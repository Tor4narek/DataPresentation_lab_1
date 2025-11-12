using System;

namespace AdtQueue.Array
{
    /// <summary>
    /// Представляет обобщённую очередь фиксированного размера, 
    /// реализованную с помощью кольцевого массива.
    /// Работает по принципу FIFO (First In, First Out) — первым пришёл, первым ушёл.
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в очереди.</typeparam>
    public class MyQueue<T>
    {
        /// <summary>
        /// Максимальный размер очереди.
        /// </summary>
        private const int Size = 256;

        /// <summary>
        /// Внутренний массив, используемый для хранения элементов очереди.
        /// </summary>
        private readonly T[] _array;

        /// <summary>
        /// Индекс первого (переднего) элемента в очереди.
        /// </summary>
        private int _front = 0;

        /// <summary>
        /// Индекс последнего (заднего) элемента в очереди.
        /// </summary>
        private int _rear = Size - 1;

        /// <summary>
        /// Инициализирует новый экземпляр очереди фиксированного размера.
        /// </summary>
        public MyQueue()
        {
            _array = new T[Size];
        }

        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// </summary>
        /// <param name="item">Элемент, который нужно добавить.</param>
        public void Enqueue(T item)
        {
            _rear = Next(_rear);
            _array[_rear] = item;
        }

        /// <summary>
        /// Удаляет элемент из начала очереди и возвращает его.
        /// </summary>
        /// <returns>Элемент, находящийся в начале очереди.</returns>
        public T Dequeue()
        {
            T result = _array[_front];
            _front = Next(_front);
            return result;
        }

        /// <summary>
        /// Возвращает элемент, находящийся в начале очереди, не удаляя его.
        /// </summary>
        /// <returns>Элемент в начале очереди.</returns>
        public T Front()
        {
            return _array[_front];
        }

        /// <summary>
        /// Очищает очередь, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _rear = _front - 1;
        }

        /// <summary>
        /// Проверяет, заполнена ли очередь.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если очередь заполнена;
        /// в противном случае — <c>false</c>.
        /// </returns>
        public bool Full()
        {
            return Next(Next(_rear)) == _front;
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
            return Next(_rear) == _front;
        }

        /// <summary>
        /// Возвращает индекс следующей позиции в кольцевом массиве.
        /// </summary>
        /// <param name="pos">Текущий индекс.</param>
        /// <returns>Следующий индекс, учитывая циклический переход.</returns>
        private int Next(int pos)
        {
            return (pos + 1) % Size;
        }
    }
}
