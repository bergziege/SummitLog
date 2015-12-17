using SummitLog.Services.Model;

namespace SummitLog.UI.Common
{
    public class ItemWithNameDesignViewModel<T> : IItemWithNameViewModel<T> where T : EntityWithIdAndName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ItemWithNameDesignViewModel()
        {
            Name = "Name " + typeof(T);
        }

        /// <summary>
        ///     Liefert das Item
        /// </summary>
        public T Item { get; }

        /// <summary>
        ///     LIefert den Namen
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public void LoadData(T item)
        {
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public void DoUpdate()
        {
        }
    }
}