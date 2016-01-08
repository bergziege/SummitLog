using ReactiveUI;
using SummitLog.UI.NameInput.ViewModels;

namespace SummitLog.UI.SummitEdit.ViewModels
{
    public class SummitEditViewModel: NameInputViewModel, ISummitEditViewModel
    {
        private string _summitNumber;

        public string SummitNumber
        {
            get { return _summitNumber; }
            set { this.RaiseAndSetIfChanged(ref _summitNumber, value); }
        }
    }
}