using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public abstract class TimeChangeObserver : MonoBehaviour
    {
        public virtual bool CanSwitchTime() => true;
        public virtual void OnTimeChanges() { }
        public virtual void OnEnterFuture() => OnTimeChanges();
        public virtual void OnEnterPast() => OnTimeChanges();
        public virtual void OnLeaveFuture() => OnTimeChanges();
        public virtual void OnLeavePast() => OnTimeChanges();
    }
}