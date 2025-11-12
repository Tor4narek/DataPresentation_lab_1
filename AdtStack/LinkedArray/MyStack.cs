namespace AdtStack.LinkedArray
{
    /// <summary>
    /// Представляет обобщённый стек на связных узлах, 
    /// работающий по принципу LIFO (Last In, First Out).
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в стеке.</typeparam>
    public class MyStack<T>
    {
        /// <summary>
        /// Голова связного списка — вершина стека.
        /// </summary>
        private Node<T> _head;
        
        /// <summary>
        /// Добавляет элемент на вершину стека.
        /// </summary>
        /// <param name="x">Элемент, добавляемый в стек.</param>
        public void Push(T x)
        {
            Node<T> previousHead = _head;
            _head = new Node<T>(x, previousHead);
        }

        /// <summary>
        /// Удаляет элемент с вершины стека и возвращает его.
        /// </summary>
        /// <returns>Элемент, находившийся на вершине стека.</returns>
        public T Pop()
        {
            T result = _head.Data;
            _head = _head.Next;
            return result;
        }

        /// <summary>
        /// Возвращает элемент, находящийся на вершине стека, не удаляя его.
        /// </summary>
        /// <returns>Элемент на вершине стека.</returns>
        public T Top()
        {
            return _head.Data;
        }

        /// <summary>
        /// Проверяет, пуст ли стек.
        /// </summary>
        /// <returns>
        /// true, если стек не содержит элементов; 
        /// в противном случае — false.
        /// </returns>
        public bool Empty()
        {
            return _head == null;
        }

        /// <summary>
        /// Проверяет, заполнен ли стек.
        /// Для данной реализации стек не имеет ограничений по размеру.
        /// </summary>
        /// <returns> Всегда возвращает false. </returns>
        public bool Full()
        {
            return false;
        }
        
        /// <summary>
        /// Очищает стек, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _head = null;
        }
    }
}
