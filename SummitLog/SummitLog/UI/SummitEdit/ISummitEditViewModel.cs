using SummitLog.UI.NameInput;

namespace SummitLog.UI.SummitEdit
{
    public interface ISummitEditViewModel : INameInputViewModel
    {
        string SummitNumber { get; set; }
        double Rating { get; set; }
    }
}