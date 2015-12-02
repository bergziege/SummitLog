using ReactiveUI;
using SummitLog.Services.Model;

namespace SummitLog.UI.Main.ViewModels
{
    public class ItemWithNameViewModel<T> :ReactiveObject, IItemWithNameViewModel<T> where T : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert das Item
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        ///     LIefert den Namen
        /// </summary>
        public string Name { get { return Item.Name; } }

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public void LoadData(T item)
        {
            Item = item;
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public void DoUpdate()
        {
            this.RaisePropertyChanged("Name");
        }
    }
}