using System;

namespace PolyhydraGames.Code
{
    public interface IBaseAdapter<T>
    {
        T GetItem(int position);
        long GetItemId(int position);
        int Count { get; }
        event EventHandler Changed;
        void CallMenu(int position);
        void CallMenu();
        T this[int index]  { get; }
       // void NotifyDataSetChanged();
    }
}
