using System.Windows.Input;
using ReactiveUI;

namespace PolyhydraGames.SchoolTools.Core.ViewModels
{
    public sealed class CalculateInterestViewModel : ReactiveObject
    {
        public CalculateInterestViewModel()
        {
            //Model = new InterestCalculatorModel();
            //CalculateSlope = new MvxCommand(() => Model.GenerateSlope());

            //CalculateCommand = new MvxCommand(() => Model.Total);

        }


        //public InterestCalculatorModel Model { get; set; }

        public ICommand CalculateCommand { get; set; }

    }
}