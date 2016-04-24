using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;

namespace SummitLog.UI.Common
{
    /// <summary>
    /// Basis für View Commands
    /// </summary>
    public abstract class ViewCommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        protected ViewCommandBase(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper)
        {
            GenericFactory = genericFactory;
            WindowParentHelper = windowParentHelper;
        }

        /// <summary>
        /// Setzt eine <see cref="IGenericFactory"/>
        /// </summary>
        protected IGenericFactory GenericFactory { private set; get; }

        /// <summary>
        /// Setzt ein <see cref="IWindowParentHelper"/>
        /// </summary>
        protected IWindowParentHelper WindowParentHelper { private set; get; }
    }
}