using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices; 
using PolyhydraGames.Extensions;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SchoolTools.Core.Properties; 
using ITextToSpeech = PolyhydraGames.TextToSpeech.Core.ITextToSpeech;

namespace PolyhydraGames.SchoolTools.Core.Spelling
{
    public class SpellingGameModel : INotifyPropertyChanged
    {
        private readonly ITextToSpeech _speech;

        public SpellingGameModel(IList<INoteDto> models, ITextToSpeech speech)
        {
            _speech = speech;
            Items = models;
            Restart(true);
        }
        private string _guess;
        private string _answer;
        private int _failedTries;
        private int _score;
        public IList<INoteDto> Items { get; set; }

        public int Score
        {
            get { return _score; }
            set
            {
                if (value == _score) return;
                _score = value;
                OnPropertyChanged();
            }
        }

        public string Guess
        {
            get { return _guess; }
            set
            {
                if (value == _guess) return;
                _guess = value;
                OnPropertyChanged();
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                if (value == _answer) return;
                _answer = value;

                OnPropertyChanged();
            }
        }

        public int FailedTries
        {
            get { return _failedTries; }
            set
            {
                if (value == _failedTries) return;
                _failedTries = value;
                OnPropertyChanged();
            }
        }

        public GameState State { get; set; }

        public void GamePump()
        {
            if (State == GameState.End)
            {
                GamePumpEnd();
            }
            else
            {
                GamePumpContinue();
            }


        }

        private async void GamePumpContinue()
        {
            ProcessResult();
        }

        private void ProcessResult()
        {
            var guess = Guess.ToLower();
            Debug.WriteLine("Guess:" + guess + " Answer:" + Answer);
            if (!String.IsNullOrEmpty(guess))
            {
         
                if (guess.ToLower().Contains(Answer))
                {
                    ProcessMatch();
                }
                else
                {
                    ProcessFail();
                }
                Guess = "";
            }

            SpeakAnswer();
        }

        private void ProcessFail()
        {
            Score -= 1;
            _speech.Speak("Thats not right!");
        }
        private void ProcessMatch()
        {
            _speech.Speak("Thats correct!");
            Score += 1;
            Answer = Items.RandomItem().Name;
        }

        private void GamePumpEnd()
        {
            Prompt($"Final Score {Score}. Try again?", Restart);
        }

        private void Restart(bool restart)
        {
            if (restart)
            {
                Score = 0;
                Guess = String.Empty;
                Answer = String.Empty;
                FailedTries = 0;
                State = GameState.Start;
                Answer = Items.RandomItem().Name;
                SpeakAnswer();
            }
        }

        public void SpeakAnswer()
        {
            _speech.Speak(Answer);
            Debug.WriteLine(Answer);
        }
        private void Prompt(string format, Action<bool> restart)
        {
           // Mvx.GetSingleton<IUserInteraction>().Confirm(format, restart);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}