using System.Linq;
using System.Windows.Input;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Services;
using PolyhydraGames.TextToSpeech.Core;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyhydraGames.SchoolTools.Core.Spelling
{
    public class SpellingGameViewModel : ReactiveObject
    {
        private readonly INoteService _noteService;
        public ICommand SubmitGuess { get; }
        public ICommand RespeakCommand { get; }
        public SpellingGameModel Model { get; }


        public SpellingGameViewModel(INoteService noteService)
        {
            _noteService = noteService;

            Model = new SpellingGameModel(_noteService.Items.ToList(),IOC.Get<ITextToSpeech>());

            SubmitGuess = ReactiveCommand.Create(() => Model.GamePump());
            RespeakCommand =  ReactiveCommand.Create(() => Model.SpeakAnswer());

        }


    }

    public enum GameState
    {
        Start,
        End

    }
}
