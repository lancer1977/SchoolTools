using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ClipboardMonitor.Core;
using DynamicData;
using PolyhydraGames.Core.ReactiveUI;
using PolyhydraGames.Extensions.Dice; 
using PolyhydraGames.SchoolTools.Core.Math;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SchoolTools.Core.Services;
using PolyhydraGames.SchoolTools.Core.Spelling;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ITextToSpeech = PolyhydraGames.TextToSpeech.Core.ITextToSpeech;

namespace PolyhydraGames.SchoolTools.Core.ViewModels
{
    public sealed class NoteViewModel : ReactiveObject
    {
        //private readonly IClipboardMonitor _clipboard;
        public ITextToSpeech Speech { get; }
        private readonly INoteService _service; 

        public NoteViewModel(INoteService service, IClipboardMonitor clipboardHandler, ITextToSpeech speech,
             ISimpleNavigator naivgator)
        {
            _service = service; 
            _service.CollectionChanged += CollectionChanged;
            Speech = speech;
            var baseItems = _cache.Connect();
            baseItems
                //.Filter(filterObservable)
                //.Sort(SortExpressionComparer<INoteDto>
                //    .Descending(i => i.IsOwned)
                //    .ThenBy(i => i.MedicationType, new PrescriberPendingOrderTypeComparer())
                //    .ThenBy(i => i.DrugName))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .DisposeMany()
                .Subscribe();
            baseItems.WhenPropertyChanged(x => x.Dirty).Where(x => x.Value).Select(x => x.Sender).Subscribe(SaveItem);
            NameFilter = "";
            MergeUpCommand = ReactiveCommand.Create<INoteDto>(MergeUp);

            //RemoveCommand = new MvxCommand<NoteModel>(Remove);
            SpeakRandomNote = ReactiveCommand.Create(ExecuteRandomNote);

            RobotChatCommand = ReactiveCommand.Create(naivgator.NavigateTo<RobotChatViewModel>);
            SpellingViewCommand = ReactiveCommand.Create(naivgator.NavigateTo<SpellingGameViewModel>);
            MathViewCommand = ReactiveCommand.Create(naivgator.NavigateTo<MathViewModel>);
            InterestCommand = ReactiveCommand.Create(naivgator.NavigateTo<CalculateInterestViewModel>);
            RemoveCommand = ReactiveCommand.Create<INoteDto>(_service.Delete).OnException();

            this.WhenAnyValue(x => x.NameFilter).Subscribe(x => RefreshItems());
            this.WhenAnyValue(x => x.TagFilter).Subscribe(x => RefreshItems());
            //this.WhenAnyValue(x => x.SelectedNote).Subscribe(  x =>   Save());
            this.WhenAnyValue(x => x.ToggleClipboardEnabled).Subscribe(x =>
            {
                clipboardHandler.MonitorClipboard = x;
            });
            // SpeakFromCommand = ReactiveCommand.Create<INoteDto>(Speak);
        }

        public void SaveItem(INoteDto dto)
        {
            _service.Update(dto);
        }


        private readonly SourceList<INoteDto> _cache = new SourceList<INoteDto>();
        public IList<INoteDto> Items => _items;
        private readonly ReadOnlyObservableCollection<INoteDto> _items;
        [Reactive] public NoteModel SelectedNote { get; set; }
        [Reactive] public string NameFilter { get; set; }
        [Reactive] public string TagFilter { get; set; }
        [Reactive] public bool ToggleClipboardEnabled { get; set; }

        public ICommand MergeUpCommand { get; private set; }
        public ICommand SpeakFromCommand { get; private set; }
        public ICommand RemoveCommand { get; }
        public ICommand RobotChatCommand { get; }
        public ICommand SpeakRandomNote { get; }
        public ICommand SpellingViewCommand { get; }
        public ICommand MathViewCommand { get; private set; }
        public ICommand InterestCommand { get; private set; }

        private void Save()
        {
            Debug.WriteLine("saving!");
            if (Items == null) return;
            foreach (var item in Items.Where(i => i.Dirty))
            {
                var local = item;
                local.Dirty = false;
                _service.Update(local);
            }

        }

        private void MergeUp(INoteDto note)
        {
            if (note == null) return;
            int index = Items.IndexOf(note);
            Debug.WriteLine("Index:" + index);
            if (index <= 0) return;
            var target = Items[index - 1];
            target.Data += note.Data;
            Debug.WriteLine(target.Name + "" + " absorbed " + note.Name);

            RemoveCommand.Execute(note);
        }

        private void UpdateCache(IEnumerable<INoteDto> meds)
        {

            _cache.Edit(innerCache =>
            {
                innerCache.Clear();
                innerCache.AddRange(meds);
            });
        }

        private void RefreshItems()
        { 
            UpdateCache(_service.Items);
        }


        private void ExecuteRandomNote()
        {
            int length = Items.Count - 1;
            int index = DiceRoll.RollRandom(0, length);
            Speech.Speak(Items[index].Data);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine(e.Action);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RefreshItems();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (INoteDto item in e.OldItems)
                    {
                        _cache.Remove(item);
                    }

                    break;
            }
        }

    }
}