using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agate.MVC.Core
{
    public interface ISceneController<out T> where T : MonoBehaviour
    {
        void InjectControllers(Dictionary<Type, IGlobalController> controllers);
        void Load();
    }
}