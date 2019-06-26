namespace Agate.MVC.Core
{
    public class GlobalController<T> : BaseController where T : class, new()
    {
        private static T _instance;
        public static T Instance
        {
            get
            { 
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}