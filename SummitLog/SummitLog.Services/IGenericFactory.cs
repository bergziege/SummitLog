namespace SummitLog.Services
{
    /// <summary>
    ///     Schnittstelle für Factories, die sich verschiedenster Container bedienen können um ihre AUfgabe zu lösen.
    /// </summary>
    public interface IGenericFactory
    {
        /// <summary>
        ///     Liefert ein Objekt vom Typ T oder ein Objekt, der Typ T implementiert, wenn es sich z.B. um ein Interface handelt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
    }
}