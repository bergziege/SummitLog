namespace SummitLog.UI.DbSettings
{
    public interface IDbSettingsViewCommand
    {
        /// <summary>
        ///     Verbindet die View mit dem View Model und zeigt diese an.
        /// </summary>
        void Execute();
    }
}