using System;
using UnityEngine;

namespace Events
{
    public abstract class EventTrigger : MonoBehaviour
    {
        protected abstract bool CheckEvent();
        protected abstract void Trigger();
        protected abstract void EventInit();

        public void EventHandler()
        {
            if (CheckEvent())
                Trigger();
        }

        private void Start()
        {
            EventInit();
        }
    }
}