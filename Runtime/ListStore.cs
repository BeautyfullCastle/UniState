using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UniState.Core
{
    public class ListStore<T> : IStore,
        IList<T>,
        IList,
        IReadOnlyList<T>
    {
        private List<T> list;
        
        [NonSerialized]
        private object _syncRoot;
        
        private readonly List<Action<List<T>>> listeners = new();

        public ListStore(List<T> list) => this.list = list;

        private void NotifyStateChanged()
        {
            foreach (var listener in listeners)
            {
                listener(list);
            }
        }

        public bool Remove(T item)
        {
            bool isRemoved = list.Remove(item);
            if(isRemoved)
                NotifyStateChanged();

            return isRemoved;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if(array is T[] tArray)
                list.CopyTo(tArray, index);
        }

        public int Count => list.Count;
        public bool IsSynchronized => false;
        
        object ICollection.SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                    Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
                return this._syncRoot;
            }
        }

        public bool IsReadOnly => false;
        
        object IList.this[int index]
        {
            get
            {
                if (list != null) return list[index];
                return null;
            }
            set
            {
                if (value is not T value1)
                    return;
                
                this[index] = value1;
                NotifyStateChanged();
            }
        }

        public void AddListener(Action<List<T>> listener)
        {
            listeners.Add(listener);
        }
        
        public void RemoveListener(Action<List<T>> listener)
        {
            listeners.Remove(listener);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void Add(T item)
        {
            list.Add(item);
            NotifyStateChanged();
        }

        public int Add(object value)
        {
            if (value is T value1)
            {
                list.Add(value1);
                NotifyStateChanged();
                return this.list.Count - 1;
            }

            throw new Exception("Invalid value");
        }

        public void Clear()
        {
            list.Clear();
            NotifyStateChanged();
        }

        bool IList.Contains(object value)
        {
            if(value is T tValue)
                return list.Contains(tValue);
            return false;
        }

        int IList.IndexOf(object value)
        {
            if (value is T value1)
            {
                return this.IndexOf(value1);
            }

            return -1;
        }

        void IList.Insert(int index, object value)
        {
            if (value is T tValue)
            {
                list.Insert(index, tValue);
                NotifyStateChanged();
            }
        }

        void IList.Remove(object value)
        {
            if (value is T tValue)
            {
                list.Remove(tValue);
                NotifyStateChanged();
            }
        }

        void IList.RemoveAt(int index)
        {
            list.RemoveAt(index);
            NotifyStateChanged();
        }

        public bool IsFixedSize => false;

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            NotifyStateChanged();
        }

        void IList<T>.RemoveAt(int index)
        {
            list.RemoveAt(index);
            NotifyStateChanged();
        }

        public T this[int index]
        {
            get => list[index];
            set
            {
                list[index] = value;
                NotifyStateChanged();
            }
        }
    }
}