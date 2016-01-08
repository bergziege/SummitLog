using SummitLog.UI.NameInput.DesignViewModels;

namespace SummitLog.UI.SummitEdit.DesignViewModels
{
    public class SummitEditDesignViewModel: NameInputDesignViewModel, ISummitEditViewModel
    {
        public SummitEditDesignViewModel()
        {
            Name = "Gipfelname";
            SummitNumber = "Gipfelnummer z.B. 42";
        }

        public string SummitNumber { get; set; }
    }
}