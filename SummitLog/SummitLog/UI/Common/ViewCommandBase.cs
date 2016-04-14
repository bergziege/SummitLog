using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;

namespace SummitLog.UI.Common
{
    public class ViewCommandBase
    {
        /// <summary>
        /// Setzt eine <see cref="IGenericFactory"/>
        /// </summary>
        [Dependency]
        public IGenericFactory GenericFactory { set; protected get; }

        /// <summary>
        /// Setzt ein <see cref="IWindowParentHelper"/>
        /// </summary>
        [Dependency]
        public IWindowParentHelper WindowParentHelper { set; protected get; }
    }
}