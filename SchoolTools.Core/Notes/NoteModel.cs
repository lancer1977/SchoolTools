using System;
using System.Collections.Generic; 
using System.Runtime.Serialization;
using PolyhydraGames.Core.Global;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SQLite;

namespace PolyhydraGames.SchoolTools.Core.Notes
{
    [DataContract]
    public sealed class NoteModel : ReactiveObject, INoteDto
    {
        public NoteModel()
        {
            this.WhenAnyValue(i => i.Name, i => i.Data, i => i.Tags).Subscribe(x =>
            {

                Dirty = true;
                this.RaisePropertyChanged(nameof(Dirty));
            });
        }
        [PrimaryKey, DataMember,AutoIncrement] public int Id { get; set; } 
        [Reactive] public bool Dirty { get; set; }

        [Indexed,DataMember,Reactive] public string Name {get;set;}

        [DataMember,Reactive]public string Data {get;set;}
 

        [Reactive,DataMember]public string Tags { get; set; }

        public void AddTag(string tag)
        {
            Tags += string.IsNullOrEmpty(Tags) ? tag : ("," + tag);
        }

        public override string ToString()
        {
            return Name;
        }
 

        public static IEnumerable<NoteModel> CreateItemsFromJson(string plainText)
        {
            List<NoteModel> p2;
            try
            {
                p2 = plainText.FromJson<List<NoteModel>>();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.ToString(), "plainText");
            }

            return p2;
        }

   

 




    }
}