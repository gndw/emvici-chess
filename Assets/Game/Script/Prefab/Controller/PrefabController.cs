using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
namespace Agate.Chess.Prefab.Controller
{
    public class PrefabController : GlobalController<PrefabController>
    {
        private Dictionary<string, Object> _prefabCache = new Dictionary<string, Object>();

        public T GetObject<T>(string path, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            T obj = LoadObject<T>(path);
            if (obj != null)
            {
                T createdObj = Object.Instantiate<T>(obj, position, rotation, parent);
                return createdObj;
            }
            else
            {
                UnityEngine.Debug.LogError("Prefab Object NULL");
                return null;
            }
        }

        public T GetUIObject<T>(string path, Transform parent) where T : MonoBehaviour
        {
            T obj = LoadObject<T>(path);
            if (obj != null)
            {
                obj.gameObject.SetActive(false);
                T createdObj = Object.Instantiate<T>(obj, parent);
                return createdObj;
            }
            else
            {
                UnityEngine.Debug.LogError("Prefab Object NULL");
                return null;
            }
        }

        private T LoadObject<T>(string path) where T : Object
        {
            if (!_prefabCache.ContainsKey(path))
            {
                _prefabCache.Add(path, Resources.Load<T>(path));
            }
            return _prefabCache[path] as T;
        }
    }
}