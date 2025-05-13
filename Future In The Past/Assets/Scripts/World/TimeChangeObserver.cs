using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public abstract class TimeChangeObserver : MonoBehaviour
    {
        public virtual bool CanSwitchTime() => true;
        public virtual void OnTimeChanges() { }
        public virtual void OnEnterFuture() { }
        public virtual void OnEnterPast() { }
        public virtual void OnLeaveFuture() { }
        public virtual void OnLeavePast() { }
    }
}