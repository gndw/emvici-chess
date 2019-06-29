using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public class Sequence
    {
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
            for (int i = 1; i < sequences.Count; i++)
            {
                sequences[i - 1].FinishAction += sequences[i].Execute;
                if (i == sequences.Count - 1)
                {
                    sequences[i].FinishAction += () => OnFinishSequence?.Invoke();
                }
            }

            if (sequences.Count > 0)
            {
                sequences[0].Execute();
            }
        }
    }
}