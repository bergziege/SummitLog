using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.NameInput.DesignViewModels
{
    public class NameInputDesignViewModel: ReactiveObject, INameInputViewModel
    {
        public NameInputDesignViewModel()
        {
            Name = "Testeingabe";
        }

        public string Name { get; set; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }
        public event EventHandler RequestCloseAfterOk;
        public event EventHandler RequestCloseAfterCancel;
    }
}