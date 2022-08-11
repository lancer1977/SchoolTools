using System;
using System.Diagnostics;
using ClipboardMonitor.Core;
using ClipboardMonitor.WPF;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SchoolTools.Core.Services;
using PolyhydraGames.SchoolTools.WPF.Views;
using PolyhydraGames.TextToSpeech.Core;

namespace PolyhydraGames.SchoolTools.WPF.Services
{
    public class ClipboardHandlerService : IClipboardHandlerService
    {
        private INoteService _service;
        public SchoolToolsState MyState { get; private set; }
        private string Lastdata { get; set; }
        private bool _enabled;
        public bool Enable
        {
            get { return _enabled; }
            set
            {
                if (Window != null)
                {
                    Window.EnableClipboard(value);
                    _enabled = value;
                }
            }
        }

        private ITextToSpeech _speech;

        private ITextToSpeech Speech
        {
            get { return _speech ?? (_speech = IOC.Get<ITextToSpeech>()); }
        }

        private ClipboardWindow Window { get; set; }
        public ClipboardHandlerService(INoteService service)
        {
            _service = service;
        }
        public string ClassName { get; set; }
        public string Chapter { get; set; }

        public void SetWindow(ClipboardWindow window)
        {
            Window = window;
        }
   

        public void ImportText(object holder, ClipboardArgs args)
        {
            Debug.WriteLine("ImportText:Args");
          
            if (MyState != SchoolToolsState.Waiting || args.Text == Lastdata ) return;
            Lastdata = args.Text;
            MyState = SchoolToolsState.TextAvailible;
            var data = args.Text;
            var items = data.Split('\n');

            var title = items[0].Replace("\n", "");
            var note = data.Replace(Environment.NewLine, " ").Replace("■", "");
            title = title.Trim();
            note = note.Trim();

            var model = new NoteModel { Data = note, Name = title, Chapter = Chapter, ClassTitle = ClassName };
            _service.Insert(model);
            Speech.Speak(title + " " + data);
            MyState = SchoolToolsState.Waiting;

        }



    }
}
