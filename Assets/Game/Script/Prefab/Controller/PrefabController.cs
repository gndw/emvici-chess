using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
namespace Agate.Chess.Prefab.Controller
{
    public class PrefabController : GlobalController<PrefabController>
    {
        private Dictionary<string, Object> _prefabCache = new Dictionary<string, Object>();
        public override void Init() { }
        public T GetObject<T>(string path, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            if (!_prefabCache.ContainsKey(path))
            {
                _prefabCache.Add(path, Resources.Load<T>(path));
            }
            T obj = _prefabCache[path] as T;
            if (obj == null) return null;
            T createdObj = Object.Instantiate<T>(obj, position, rotation, parent);
            return createdObj;
        }
    }
}