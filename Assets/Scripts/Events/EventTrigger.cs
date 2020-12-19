using System.Collections;
using UnityEngine;

namespace Events
{
    public abstract class EventTrigger : MonoBehaviour
    {
        protected abstract bool CheckEvent();
        protected abstract void Trigger();
        protected abstract bool EventEndCondition();
        protected abstract void EventInit();

        private IEnumerator EventRoutine()
        {
            while (!EventEndCondition())
            {
                yield return new WaitUntil(() => CheckEvent());
                Trigger();
            }
        }

        private void Start()
        {
            EventInit();
            StartCoroutine(EventRoutine());
        }
    }
}