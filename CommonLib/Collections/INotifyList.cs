using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CommonLib.Collections
{
    public class NotifyList<T> : List<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// Добавить объект в коллекцию
        /// </summary>
        /// <param name="item"></param>
        public new void Add(T item)
        {
            base.Add(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        }

        /// <summary>
        /// Добавить объекты в коллекцию
        /// </summary>
        /// <param name="items"></param>
        public new void AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, items);
        }

        /// <summary>
        /// Удалить объект из коллекции
        /// </summary>
        /// <param name="item"></param>
        public new Boolean Remove(T item)
        {
            var result = base.Remove(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);

            return result;
        }

        public new void Clear()
        {
            base.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public new void Insert(Int32 i, T item)
        {
            base.Insert(i,item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, i);
        }

        public new void RemoveAll(Predicate<T> p)
        {
            base.RemoveAll(p);
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public new void RemoveAt(Int32 i)
        {
            base.RemoveAt(i);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, i);
        }
        public new void RemoveRange(Int32 i, Int32 count)
        {
            base.RemoveRange(i, count);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, i);
        }

        public new void InsertRange(Int32 i, IEnumerable<T> items)
        {
            base.InsertRange(i, items);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, items, i);
        }

        /// <summary>
        /// Отсортировать
        /// </summary>
        public new void Sort()
        {
            base.Sort();
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        private void OnPropertyChanged(String propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
        private void OnCollectionChanged(NotifyCollectionChangedAction action, Object item = null, Int32? i = null)
        {
            if (i.HasValue)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, i.Value));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
        }
    }
}
