using System.Collections.Generic;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.SchoolTools.Core.Notes;

namespace PolyhydraGames.SchoolTools.Core.Services
{
    public interface INoteService : IRepositoryLite<INoteDto>
    {
        void Load(IEnumerable<INoteDto> toObservableCollection);
        IEnumerable<INoteDto> MatchingName(string name );
        IEnumerable<INoteDto> MatchingTag(string tag );
    }
}