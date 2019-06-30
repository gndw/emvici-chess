using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public class Sequence
    {
        public event ProgressFunction OnProgressSequence;
        public event Function OnFinishSequence;

        private List<SequenceObject> _sequences = new List<SequenceObject>();
        private Action<Action> _separator;

        private class SequenceObject
        {
            public Action<Action> MainAction;
            public Action FinishAction;
            public void Execute()
            {
                MainAction(FinishAction);
            }
        }

        public void AddSequence(Action<Action> newSequence)
        {
            SequenceObject sobj = new SequenceObject();
            sobj.MainAction = newSequence;
            _sequences.Add(sobj);
        }

        public void SetSeparatorSequence(Action<Action> newSeparatorSequence)
        {
            _separator = newSeparatorSequence;
        }

        public void RemoveSeparatorSequence()
        {
            _separator = null;
        }

        public void Execute()
        {
            OnProgressSequence?.Invoke(0);
            for (int i = 0; i < _sequences.Count; i++)
            {
                if (i < _sequences.Count - 1)
                {
                    int index = i;
                    _sequences[i].FinishAction += () =>
                    {
                        float prog = ((float)index + 1) / _sequences.Count;
                        OnProgressSequence?.Invoke(prog);

                        if (_separator != null)
                        {
                            SequenceObject sep = new SequenceObject();
                            sep.MainAction = _separator;
                            sep.FinishAction += () => _sequences[index + 1].Execute();
                            sep.Execute();
                        }
                        else
                        {
                            _sequences[index + 1].Execute();
                        }
                    };
                }
                else
                {
                    _sequences[i].FinishAction += () =>
                    {
                        OnProgressSequence?.Invoke(1);
                        OnFinishSequence?.Invoke();
                    };
                }
            }

            if (_sequences.Count > 0)
            {
                _sequences[0].Execute();
            }
            else
            {
                OnProgressSequence?.Invoke(1);
                OnFinishSequence?.Invoke();
            }
        }
    }
}