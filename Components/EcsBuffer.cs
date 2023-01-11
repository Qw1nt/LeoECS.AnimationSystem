using System.Collections.Generic;

namespace Ecs.Animation.Components
{
    public class EcsBuffer<T> where T : class
    {
        private readonly int _capacity;
        
        public EcsBuffer(int capacity)
        {
            _capacity = capacity;
            StoredObjects = new T[_capacity];
        }

        private T[] StoredObjects { get; set; }
        
        public void Fill(T[] data)
        {
            StoredObjects = data;
        }

        public void Fill(int index, T data)
        {
            StoredObjects[index] = data;
        }

        public T Get(int index)
        {
            return StoredObjects[index];
        }
        
        public void Remove(int index)
        {
            StoredObjects[index] = null;
        }
        
        public void ClearAll(int capacity = -1)
        {
            if (capacity == -1)
                capacity = _capacity;
            
            StoredObjects = new T[capacity];
        }

        public int GetLength()
        {
            return _capacity;
        }

        public IReadOnlyList<T> GetCollection()
        {
            return StoredObjects;
        }
    }
}