using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClipboardMonitor.Core;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.SchoolTools.Core.Notes;
using PolyhydraGames.SQLite;
using PolyhydraGames.SQLite.Interfaces;
using static System.String;

namespace PolyhydraGames.SchoolTools.Core.Services
{
    public class NoteService :  SqlRepositoryLite<NoteModel,INoteDto>, INoteService,IInitialize
    {
        private SchoolToolsState _myState;

        public async Task InitializeAsync()
        {
            
        }
        public IEnumerable<INoteDto> MatchingTag(string tag)
        { 
            if (tag == "")
                return Items; 
            return  Items.Where(x=>x.Tags.Split(',').Contains(tag));
        }
  

        public override string Report()
        {
            return "Items:" + Items.Count();
        }

        public override string Title { get; }

        public void Load(IEnumerable<INoteDto> items)
        {
            foreach (var item in items)
            {
                Insert(item);
            }
        }

        public IEnumerable<INoteDto> MatchingName(string nameFilter = "")
        {
            if (IsNullOrEmpty(nameFilter))
            {
                Debug.WriteLine("Null or empty");
                return Items;
            }
            Debug.WriteLine("NOTE!!!!!!!Null or empty");
            return (from item in Table where item.Name.Contains(nameFilter) select item);
            //  return  Items.ToArray().Where(i => i.Name.Contains(nameFilter));
        }
 

        private string _lastImport;
        public NoteService(ISQLiteFactory factory, IClipboardMonitor monitor) : base(factory)
        {
            monitor.OnClipboardChanged += ImportText;
        } 
        private  void ImportText(object holder, ClipboardArgs args)
        {
         Debug.WriteLine("In Import");   
            if (_lastImport == args.Text) return;
            _lastImport = args.Text;
            Import(_lastImport);
        }

        private void Import(string value)
        {  
            var items = value.Split('\r');
            var title = items[0].Replace("\n", "");
            var note = value.Replace(Environment.NewLine, " ").Replace("■", "");
            title = title.Trim();
            note = note.Trim();
            var model = new NoteModel(){Name=title,Data = note}; 
            Insert(model); 
        }



    }

    public enum SchoolToolsState
    {
        Waiting,
        AddingText
    }

}