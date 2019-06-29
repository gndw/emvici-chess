using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public class Sequence
    {
        public event ProgressFunction OnProgressSequence;
        public event Function OnFinishSequence;

        private List<SequenceObject> sequences = new List<SequenceObject>();
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
            sequences.Add(sobj);
        }

        public void Execute()
        {
            OnProgressSequence?.Invoke(0);
            for (int i = 0; i < sequences.Count; i++)
            {
                if (i < sequences.Count - 1)
                {
                    int index = i;
                    sequences[i].FinishAction += () =>
                    {
                        float prog = ((float)index + 1) / sequences.Count;
                        OnProgressSequence?.Invoke(prog);
                        sequences[index + 1].Execute();
                    };
                }
                else
                {
                    sequences[i].FinishAction += () =>
                    {
                        OnProgressSequence?.Invoke(1);
                        OnFinishSequence?.Invoke();
                    };
                }
            }

            if (sequences.Count > 0)
            {
                sequences[0].Execute();
            }
            else
            {
                OnProgressSequence?.Invoke(1);
                OnFinishSequence?.Invoke();
            }
        }
    }
}