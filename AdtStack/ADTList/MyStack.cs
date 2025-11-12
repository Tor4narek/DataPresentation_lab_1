using System;
using CursorList;

namespace AdtStack.ADTList
{
    /// <summary>
    /// Представляет обобщённый стек, реализованный на основе списка (<see cref="MyList{T}"/>).
    /// Стек работает по принципу LIFO (Last In, First Out) — последним пришёл, первым ушёл.
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в стеке. 
    /// Должен реализовывать интерфейс <see cref="IEquatable{T}"/>.</typeparam>
    public class MyStack<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Список, используемый для хранения элементов стека.
        /// </summary>
        private MyList<T> _list;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MyStack{T}"/>.
        /// </summary>
        public MyStack()
        {
            _list = new MyList<T>();
        }

        /// <summary>
        /// Добавляет элемент на вершину стека.
        /// </summary>
        /// <param name="item">Элемент, который нужно добавить в стек.</param>
        public void Push(T item)
        {
            // Добавляем элемент перед первым — становится новой вершиной стека
            _list.Insert(_list.First(), item);
        }

        /// <summary>
        /// Удаляет элемент с вершины стека и возвращает его.
        /// </summary>
        /// <returns>Элемент, находившийся на вершине стека.</returns>
        public T Pop()
        {
            T result = _list.Retrieve(_list.First());
            _list.Delete(_list.First());
            
            return result;
        }

        /// <summary>
        /// Возвращает элемент, находящийся на вершине стека, не удаляя его.
        /// </summary>
        /// <returns>Элемент, находящийся на вершине стека.</returns>
        public T Top()
        {
            return _list.Retrieve(_list.First());
        }

        /// <summary>
        /// Проверяет, пуст ли стек.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если стек не содержит элементов; 
        /// в противном случае — <c>false</c>.
        /// </returns>
        public bool Empty()
        {
            return _list.First() == _list.End();
        }

        /// <summary>
        /// Проверяет, заполнен ли стек.
        /// Для данной реализации стек не имеет ограничений по размеру.
        /// </summary>
        /// <returns>Всегда возвращает <c>false</c>.</returns>
        public bool Full()
        {
            return false;
        }

        /// <summary>
        /// Очищает стек, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _list.MakeNull();
        }
    }
}
