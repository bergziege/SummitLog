namespace SummitLog.UI.Common
{
    public interface IGenericFactory
    {
        T Resolve<T>();
    }
}