using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using PolyhydraGames.Extensions;


namespace PolyhydraGames.Code
{
    public partial class MainWindow
    {
        private const string SaveFileName = ".\\savedata.txt";
        private string _lastName;
        public TextToSpeech.Speech Speak;
        private readonly NoteModelManager _notes;
        protected SchoolToolsState _myState;

        private NoteModel _currentModel = new NoteModel { Data = "EmptyData", Name = "EmptyName" };
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.EnableClipboard(((CheckBox)sender).IsChecked == true);


        }
        public MainWindow()
        {
            InitializeComponent();
            _notes = new NoteModelManager();
            Speak = new TextToSpeech.Speech();
            Speak.Synthesizer.StateChanged += Synthesizer_StateChanged;
            DataContext = _currentModel;
            _notes.Changed += _notes_Changed;
            base.MonitorClipboard += ImportText;
            FileManipulation.LoadFile(SaveFileName, _notes);
            EnableClipboard(true);
            VoiceSlider.Maximum = Speak.VoiceNames().Count() - 1;

        }

        private void _notes_Changed(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Synthesizer_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case SynthesizerState.Speaking:
                    PauseButton.Content = "Pause";
                    PauseButton.IsEnabled = true;
                    break;
                case SynthesizerState.Paused:
                    PauseButton.Content = "Resume";
                    PauseButton.IsEnabled = true;
                    break;
                case SynthesizerState.Ready:
                    PauseButton.IsEnabled = false;
                    break;
            }
        }

        protected void AddData(NoteModel code)
        {

            if (code == null || code.Name == _lastName)
            {
                _myState = SchoolToolsState.Waiting;
                return;
            }
            //Console.Beep();
            _notes.Add(code);

            Speak.Speak(code.Name + " " + code.Data);
            _lastName = code.Name;
            _myState = SchoolToolsState.Waiting;
        }

        private void StopAudio(object sender, RoutedEventArgs e)
        {
            Speak.Stop();
        }



        protected virtual void ImportText(object holder, ClipboardMonitorClass.ClipboardArgs args)
        {
            if (_myState != SchoolToolsState.Waiting) return;
            var data = args.Text;
            var items = data.Split('\r');

            var title = items[0].Replace("\n", "");
            var note = data.Replace(Environment.NewLine, " ").Replace("■", "");
            title = title.Trim();
            note = note.Trim();
            var model = new NoteModel(title, note, ClassTitle.Text, Chapter.Text);

            _myState = SchoolToolsState.TextAvailible;
            AddData(model);

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_notes.ListToString());
        }



        private void Redraw()
        {
            ListViewNotes.Items.Clear();
            foreach (var item in _notes.List)
                ListViewNotes.Items.Add(item);
        }

        private void ListViewNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SelectedNote == null) return;
            _currentModel = SelectedNote;

            NoteTitle.Text = _currentModel.Name;
            Description.Text = _currentModel.Data;
            ClassTitle.Text = _currentModel.ClassTitle;
            Chapter.Text = _currentModel.Chapter;
        }

        private NoteModel SelectedNote
        {
            get
            {
                var box = ListViewNotes;
                if (box.SelectedItem == null) return null;
                return (NoteModel)ListViewNotes.SelectedItem;
            }


        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Redraw();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileManipulation.SaveFile(SaveFileName, _notes);

        }

        private void ClassTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentModel.ClassTitle = ((TextBox)sender).Text;
        }

        private void Chapter_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentModel.Chapter = ((TextBox)sender).Text;
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentModel.Name = ((TextBox)sender).Text;
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentModel.Data = ((TextBox)sender).Text;
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedNote == null) return;
            _notes.Remove(SelectedNote);

        }

        private void Speak_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedNote == null) return;
            Speak.Speak(SelectedNote.Data);
        }




        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Speak.Rate(e.NewValue);
            VoiceSpeed.Text = "Voice Speed: " + ((int)e.NewValue);

        }

        private void SliderVoice_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Speak.Rate(e.NewValue);
            var name = Speak.VoiceNames()[(int)e.NewValue];

            Voice.Text = "Voice : " + name;
            Speak.ChangeVoice(name);
        }

        private void NewChapter(object sender, RoutedEventArgs e)
        {
            InputBox("New Class", "Start a new class", () => _notes.Clear());
        }

        private void InputBox(string title, string message, Action action)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                action();
            }
        }



        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            FileManipulation.SaveFile(_notes);
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            FileManipulation.LoadFile(_notes);
            Redraw();
        }

        // private SpeechListener

        private void SpeakFrom_OnClick(object o, RoutedEventArgs e)
        {
            if (SelectedNote == null) return;
            var index = _notes.IndexOf(SelectedNote);
            for (var x = index; x < _notes.Count; x++)
                Speak.Speak(_notes[x].Data);
        }
        private void MergeUp_OnClick(object o, RoutedEventArgs e)
        {
            _notes.MergeUp(SelectedNote);


        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Speak.Pause();
        }


        private void MakeXML(object sender, RoutedEventArgs e)
        {

        }

        private void MakeSpecial_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _notes.List)
            {
                Output.Text += item.GenerateXML + Environment.NewLine;
            }
        }


        private void Open_Robot_Chat(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
            var robotChat = new RobotChatWindow();
            robotChat.Show();
            robotChat.Activate();
        }

        protected virtual void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var items = new List<string>();


        }

        private void btnImport(object sender, RoutedEventArgs e)
        {
            var item = new SpiritWindow();
            item.Show();
        }
    }

    public class SpiritWindow : MainWindow
    {
        protected override void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var items = new List<string>();
            var spiritXml = @"<Spirit Name=""{0}"" Description=""{1}"" />";

        }
        protected override void ImportText(object holder, ClipboardMonitorClass.ClipboardArgs args)
        {
            if (_myState != SchoolToolsState.Waiting) return;

            var data = args.Text;
            var items = data.Split('\r');

            var outItems = "";
            foreach (var item in items)
            {
                if (item.Contains(":"))
                    outItems = outItems + Environment.NewLine + item;
                else
                {
                    outItems = outItems + item.Replace("\n", ""); ;
                }
            }
            // var note = items[1].Replace(Environment.NewLine, " ").Replace("■", ""); 
            var model = new NoteModel("Spirit", outItems, ClassTitle.Text, Chapter.Text);

            _myState = SchoolToolsState.TextAvailible;
            AddData(model);

        }

    }
    public enum SchoolToolsState
    {
        Waiting,
        TextAvailible
    }


}
