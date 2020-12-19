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
                yield return new WaitForSeconds(1);
                if (!EventEndCondition() && CheckEvent())
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