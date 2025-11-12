using System;

namespace AdtQueue.LinkedArray
{
    /// <summary>
    /// Представляет обобщённую очередь, реализованную на основе кольцевого связного списка.
    /// Работает по принципу FIFO (First In, First Out) — первым пришёл, первым ушёл.
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в очереди.</typeparam>
    public class MyQueue<T>
    {
        /// <summary>
        /// Хвост очереди (указатель на последний узел кольцевого списка).
        /// </summary>
        private Node<T> _tail;

        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// </summary>
        /// <param name="value">Элемент, который нужно добавить.</param>
        public void Enqueue(T value)
        {
            // Если очередь пуста — создаём первый элемент
            if (_tail == null)
            {
                _tail = new Node<T>(value, null);
                _tail.Next = _tail; // Кольцевая связь на самого себя
                return;
            }

            // Новый элемент указывает на голову (первый элемент)
            Node<T> current = new Node<T>(value, _tail.Next);

            // Старый хвост теперь указывает на новый элемент
            _tail.Next = current;

            // Новый элемент становится хвостом
            _tail = current;
        }

        /// <summary>
        /// Удаляет элемент из начала очереди и возвращает его.
        /// </summary>
        /// <returns>Элемент, находящийся в начале очереди.</returns>

        public T Dequeue()
        {
            // Голова — элемент, следующий за хвостом
            T result = _tail.Next.Data;

            // Если остался один элемент
            if (_tail.Next == _tail)
            {
                _tail = null;
            }
            else
            {
                // Перемещаем ссылку головы на следующий элемент
                _tail.Next = _tail.Next.Next;
            }

            return result;
        }

        /// <summary>
        /// Возвращает элемент, находящийся в начале очереди, не удаляя его.
        /// </summary>
        /// <returns>Элемент в начале очереди.</returns>
        public T Front()
        {
            return _tail.Next.Data;
        }

        /// <summary>
        /// Проверяет, заполнена ли очередь.
        /// Для данной реализации очередь не имеет фиксированного ограничения по размеру.
        /// </summary>
        /// <returns>Всегда возвращает <c>false</c>.</returns>
        public bool Full()
        {
            return false;
        }

        /// <summary>
        /// Проверяет, пуста ли очередь.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если очередь не содержит элементов;
        /// в противном случае — <c>false</c>.
        /// </returns>
        public bool Empty()
        {
            return _tail == null;
        }

        /// <summary>
        /// Очищает очередь, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _tail = null;
        }
    }
}
