using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Agate.MVC.Core
{
    public class ScheduleController : GlobalController<ScheduleController>
    {
        [Inject]
        protected BaseGameController _baseGameController { get; set; }

        private Dictionary<int, Action> _updates = new Dictionary<int, Action>();
        private Dictionary<int, Coroutine> _coroutines = new Dictionary<int, Coroutine>();
        private int _counter = 0;

        public override void Init(Action onInitialized)
        {
            _baseGameController.RegisterUpdate(Update);
            onInitialized();
        }

        private void Update()
        {
            foreach (var update in _updates) update.Value();
        }

        private int GetID()
        {
            _counter++;
            return _counter;
        }

        public int RegisterUpdate(Action action)
        {
            int id = GetID();
            _updates.Add(id, action);
            return id;
        }

        public bool UnregisterUpdate(int id)
        {
            return _updates.Remove(id);
        }

        public int StartCoroutine(IEnumerator ienumerator)
        {
            int id = GetID();
            Coroutine c = _baseGameController.StartCoroutine(IEAction(ienumerator, () =>
            {
                _coroutines.Remove(id);
            }));
            _coroutines.Add(id, c);
            return id;
        }

        public bool IsCoroutinePlaying(int id)
        {
            return _coroutines.ContainsKey(id);
        }

        public bool StopCoroutine(int id)
        {
            Coroutine c;
            if (_coroutines.TryGetValue(id, out c))
            {
                _baseGameController.StopCoroutine(c);
                return _coroutines.Remove(id);
            }
            return true;
        }

        public int WaitForSeconds(float time, Action onFinish)
        {
            return StartCoroutine(IEWaitForSeconds(time, onFinish));
        }

        private IEnumerator IEAction(IEnumerator ie, Action onFinish)
        {
            yield return ie;
            onFinish();
        }

        private IEnumerator IEWaitForSeconds(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish();
        }
    }
}