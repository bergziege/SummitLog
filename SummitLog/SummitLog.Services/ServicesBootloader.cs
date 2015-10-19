using DryIoc;

namespace SummitLog.Services
{
    /// <summary>
    ///     Bootloader für den Servicebereich
    /// </summary>
    public static class ServicesBootloader
    {
        /// <summary>
        ///     Initialisiert den Bootloader
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Container Init(Container container)
        {
            return container;
        }
    }
}