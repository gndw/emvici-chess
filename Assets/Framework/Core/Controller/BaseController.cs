using System;
using System.Collections.Generic;
using System.Reflection;

namespace Agate.MVC.Core
{
    public class BaseController
    {
        public void InjectControllers(Dictionary<Type, IGlobalController> controllers)
        {
            PropertyInfo[] props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute<InjectAttribute>(false) != null)
                {
                    IGlobalController gc;
                    if (controllers.TryGetValue(prop.PropertyType, out gc))
                    {
                        prop.SetValue(this, gc);
                    }
                }
            }
        }
    }
}