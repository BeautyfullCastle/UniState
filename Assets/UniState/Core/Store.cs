using System;
using System.Collections.Generic;

namespace UniState.Core
{
    public class Store<T> : IStore
    {
        private T state;
        private readonly List<Action<T>> listeners = new();

        public Store(T initialState)
        {
            state = initialState;
        }

        public void UpdateState(T newState)
        {
            state = newState;
            NotifyStateChanged();
        }

        public T GetState()
        {
            return state;
        }

        public void AddListener(Action<T> listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            listeners.Remove(listener);
        }

        protected void NotifyStateChanged()
        {
            foreach (var listener in listeners)
            {
                listener(state);
            }
        }

        public override string ToString()
        {
            return state.ToString();
        }
    }

}