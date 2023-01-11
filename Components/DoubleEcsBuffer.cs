using System;

namespace Ecs.Animation.Components
{
    public class DoubleEcsBuffer<T>
    {
        private readonly int _capacity;
        private readonly Func<T, T, bool> _equalsFunction;

        public DoubleEcsBuffer(int capacity, Func<T, T, bool> equalsFunction)
        {
            CurrentData = new T[capacity];
            CacheData = new T[capacity];
        }

        private T[] CurrentData { get; set; }
        
        private T[] CacheData { get; set; }

        public void Fill(T[] data)
        {
            CurrentData = data;
        }
        
        
    }
}