using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class TimeSwitcher : MonoBehaviour
    {
        [SerializeField] private bool inFutureInit;
        [SerializeField] private List<TimeChangeObserver> timeObservers;

        public bool InFutureNow { get; private set; }

        private void Start()
        {
            InFutureNow = inFutureInit;
            foreach (var observer in timeObservers)
            {
                if (InFutureNow)
                    observer.OnLeavePast();
                else
                    observer.OnLeaveFuture();
            }
        }

        public void SwitchTime()
        {
            if (!timeObservers.All(x => x.CanSwitchTime()))
                return;

            foreach (var observer in timeObservers)
            {
                observer.OnTimeChanges();
            }

            foreach (var observer in timeObservers)
            {
                if (InFutureNow)
                {
                    observer.OnLeaveFuture();
                    observer.OnEnterPast();
                }
                else
                {
                    observer.OnLeavePast();
                    observer.OnEnterFuture();
                }
            }
            InFutureNow = !InFutureNow;
        }
    }
}