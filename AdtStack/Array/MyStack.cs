using System;
using System.Text;

namespace AdtStack.Array
{
    /// <summary>
    /// Представляет обобщённый стек фиксированного размера, работающий по принципу LIFO (Last In, First Out).
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в стеке.</typeparam>
    public class MyStack<T>
    {
        private const int MaxSize = 100;
        private readonly T[] _data = new T[MaxSize];
        private int _top = -1;
        
        /// <summary>
        /// Делает стек <paramref name="s"/> пустым.
        /// </summary>
        public void MakeNull()
        {
            _top = -1;
        }

        /// <summary>
        /// Добавляет элемент <paramref name="x"/> в вершину стека <paramref name="s"/>.
        /// </summary>
        /// <param name="x">Элемент, добавляемый в стек.</param>
        /// <exception cref="InvalidOperationException">Выбрасывается, если стек полон.</exception>
        public void Push( T x)
        {
            _top++;
            _data[_top] = x;
        }

        /// <summary>
        /// Удаляет элемент из вершины стека <paramref name="s"/> и возвращает его.
        /// </summary>
        /// <returns>Элемент, удалённый с вершины стека.</returns>
        public T Pop()
        {
            T value = _data[_top];
            _top--;
            
            return value;
        }

        /// <summary>
        /// Возвращает элемент (копию) с вершины стека <paramref name="s"/>, не удаляя его.
        /// </summary>
        /// <returns>Элемент, находящийся на вершине стека.</returns>
        public T Top()
        {
            return _data[_top];
        }

        /// <summary>
        /// Определяет, пуст ли стек <paramref name="s"/>.
        /// </summary>
        /// <param name="s">Стек для проверки.</param>
        /// <returns><see langword="true"/>, если стек пуст; иначе <see langword="false"/>.</returns>
        public bool Empty()
        {
            return _top == -1;
        }

        /// <summary>
        /// Определяет, заполнен ли стек <paramref name="s"/>.
        /// </summary>
        /// <returns><see langword="true"/>, если стек полон; иначе <see langword="false"/>.</returns>
        public bool Full()
        {
            return _top == MaxSize - 1;
        }
    }
}
