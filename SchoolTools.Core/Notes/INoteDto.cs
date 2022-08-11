using System.Collections.Generic;
using System.ComponentModel;
using PolyhydraGames.Core.Interfaces;

namespace PolyhydraGames.SchoolTools.Core.Notes
{
    public interface INoteDto : IDto,INotifyPropertyChanged
    {
        string Name { get; set; } 
        string Data{ get; set; }
        string Tags { get; set; }
        bool Dirty { get; set; }
         
    }
}