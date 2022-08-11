using System.Windows.Input;
using ReactiveUI;

namespace PolyhydraGames.SchoolTools.Core.Math
{
    public sealed class MathViewModel : ReactiveObject
    {
        public MathViewModel()
        {
       
        }
        public ICommand ResetCommand { get; private set; }

        //public LinearEquationModel Model { get; set; }

        public ICommand CalculateSlope { get; set; }

        public object CalculateYIntercept { get; set; }

        public object CalculateXIntercept { get; set; }
    }
}