using CursorList;

namespace AdtQueue.ADTList
{
    /// <summary>
    /// Представляет обобщённую очередь, реализованную на основе списка (<see cref="MyList{T}"/>).
    /// Работает по принципу FIFO (First In, First Out) — первым пришёл, первым ушёл.
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в очереди. 
    /// Должен реализовывать интерфейс <see cref="IEquatable{T}"/>.</typeparam>
    public class MyQueue<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Список, используемый для хранения элементов очереди.
        /// </summary>
        private MyList<T> _list;

        /// <summary>
        /// Инициализирует новый экземпляр очереди.
        /// </summary>
        public MyQueue()
        {
            _list = new MyList<T>();
        }

        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// </summary>
        /// <param name="item">Элемент, который нужно добавить.</param>
        public void Enqueue(T item)
        {
            _list.Insert(_list.End(), item);
        }

        /// <summary>
        /// Удаляет элемент из начала очереди и возвращает его.
        /// </summary>
        /// <returns>Элемент, находящийся в начале очереди.</returns>
        public T Dequeue()
        {
            T result = _list.Retrieve(_list.First());
            _list.Delete(_list.First());
            return result;
        }

        /// <summary>
        /// Возвращает элемент, находящийся в начале очереди, не удаляя его.
        /// </summary>
        /// <returns>Элемент в начале очереди.</returns>
        public T Front()
        {
            return _list.Retrieve(_list.First());
        }

        /// <summary>
        /// Проверяет, заполнена ли очередь.
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
        /// <c>true</c>, если очередь пуста; 
        /// в противном случае — <c>false</c>.
        /// </returns>
        public bool Empty()
        {
            return _list.First() == _list.End();
        }

        /// <summary>
        /// Очищает очередь, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _list.MakeNull();
        }
    }
}
