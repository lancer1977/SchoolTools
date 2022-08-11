using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PolyhydraGames.Core.Interfaces;
using ReactiveUI;

namespace PolyhydraGames.SchoolTools.WPF.Services
{
    public class MainThreadDispatcher : IMainThreadDispatcher
    {
        public void InvokeOnMainThread(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }
    }
}
