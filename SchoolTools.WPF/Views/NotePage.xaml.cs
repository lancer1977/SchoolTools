using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SchoolTools.Core.ViewModels;

namespace PolyhydraGames.SchoolTools.WPF.Views
{
    /// <summary>
    /// Interaction logic for NoteManagerView.xaml
    /// </summary>
    public partial class NotePage
    {

        public   NoteViewModel ViewModel
        {
            get => (NoteViewModel)base.DataContext; 
        }

        private const string SaveFileName = ".\\savedata.txt"; 
 

  


        public NotePage()
        {
            InitializeComponent();  
        }




        private void StopAudio(object sender, RoutedEventArgs e)
        {
            ViewModel.Speech.Stop();
        }


    

        private void Speak_OnClick(object sender, RoutedEventArgs e)
        {

            var item = ViewModel.SelectedNote;
            if (item == null)
            {
                Debug.WriteLine("Item was null");
                return;
            }
            ViewModel.Speech.Speak(item.Data);
        }





        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
             ViewModel.Speech.Rate(e.NewValue);
            VoiceSpeed.Text = "Voice Speed: " + ((int)e.NewValue);

        }

        private void SliderVoice_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Speak.Rate(e.NewValue);
            var name =  ViewModel.Speech.VoiceNames()[(int)e.NewValue];

            Voice.Text = "Voice : " + name;
            ViewModel.Speech.ChangeVoice(name);
        }



        private void InputBox(string title, string message, Action action)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                action();
            }
        }




        // private SpeechListener

        private void SpeakFrom_OnClick(object o, RoutedEventArgs e)
        {
            var index = ViewModel.Items.IndexOf(e.Source as NoteModel);
            for (var x = index; x < ViewModel.Items.Count; x++)
                 ViewModel.Speech.Speak(ViewModel.Items[x].Data);
        }
        private void MergeUp_OnClick(object o, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            if (menuItem == null) return;
            ViewModel.MergeUpCommand.Execute(menuItem.DataContext);



        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Speech.Pause();
        }

    }
}
