using SummitLog.UI.NameInput.DesignViewModels;

namespace SummitLog.UI.SummitEdit.DesignViewModels
{
    public class SummitEditDesignViewModel: NameInputDesignViewModel, ISummitEditViewModel
    {
        public SummitEditDesignViewModel()
        {
            Name = "Gipfelname";
            SummitNumber = "Gipfelnummer z.B. 42";
            Rating = 3.5;
        }

        public string SummitNumber { get; set; }
        public double Rating { get; set; }
    }
}