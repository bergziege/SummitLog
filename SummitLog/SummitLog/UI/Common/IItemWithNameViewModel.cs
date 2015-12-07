using SummitLog.Services.Model;

namespace SummitLog.UI.Common
{
    /// <summary>
    ///     Schnittstelle für Items mit Namen
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IItemWithNameViewModel<T> where T : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert das Item
        /// </summary>
        T Item { get; }

        /// <summary>
        ///     LIefert den Namen
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        void LoadData(T item);

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        void DoUpdate();
    }
}