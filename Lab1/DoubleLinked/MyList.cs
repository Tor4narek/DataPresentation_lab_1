namespace Lab1.DoubleLinked
{
    /// <summary>
    /// Реализация двусвязного списка.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyList<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Первый элемент списка.
        /// </summary>
        private Node<T> _head;
        
        /// <summary>
        /// Последний элемент списка.
        /// </summary>
        private Node<T> _tail;
        
        /// <summary>
        /// Конец списка.
        /// </summary>
        private readonly PositionIndex<T> _end = new PositionIndex<T>(null);

        /// <summary>
        /// Условный конец списка.
        /// </summary>
        /// <returns>Position<T>.</returns>
        public PositionIndex<T> End()
        {
            return _end;
        }

        /// <summary>
        /// Вставлят <paramref name="value"/> в <paramref name="positionIndex"/>.
        /// </summary>
        /// <remarks>
        /// 1) Если список пустой:
        ///     Создает новый узел.
        ///     Новый узел становится головой и хвостом.
        /// 2) Если вставка в конец списка (_end):
        ///     Создаем новый узел, передаем предыдущий как _tail, следующий null.
        ///     Связываем текущий _tail и новый узел.
        ///     Обновляем _tail на новый узел.
        /// 3) Проверяем что позиция существует
        /// 4) Вставка в определенную позицию (перед существующим узлом):
        ///     Берем переданную позиуию, как текущую, перед ней будем вставлять.
        ///     Создаем новый узел, помещаем туда данные текущего узла и обновляем ссылки:
        ///     Новый узел с данными текущего ссылается на слелующий после текущего.
        ///     А текущий ссылкается на новый.
        ///     Обновляем данные текущего листа
        ///     Таким образом мы вставляем данные в текущую позицию,
        ///     но старые данные становятся в позиции перед ней.
        ///     Если вставка была перед хвостом,
        ///     то нужно обновить хвост на новый узел, который хранит данные текущего.
        /// </remarks>
        /// <param name="positionIndex">Позиция.</param>
        /// <param name="value">Значение.</param>
        /// <exception cref="PositionException">Выбрасывается, если <paramref name="positionIndex"/> не существует.</exception>
        public void Insert(PositionIndex<T> positionIndex, T value)
        {
            if (positionIndex == _end)
            {
                if (_head == null)
                {
                    Node<T> newNode = new Node<T>(value); // создаём новый узел
                    _head = newNode;                       // этот узел становится головой
                    _tail = newNode;                       // и хвостом одновременно
                
                    return;                                // вставка завершена
                }
                
                Node<T> newNodeAtEnd = new Node<T>(value, _tail, null); // новый узел после хвоста
                UpdateNodes(_tail, newNodeAtEnd);                       // связываем с текущим хвостом
                _tail = newNodeAtEnd;                                   // обновляем хвост
                
                return;                                                 // вставка завершена
            }

            // 3) Проверка корректности позиции
            ValidatePosition(positionIndex);
            
            // 4) Вставка перед существующим узлом
            Node<T> currentNode = positionIndex.Node!;                    // узел, перед которым вставляем
            Node<T> newNodeWithOldData = new Node<T>(currentNode.Data);// создаём новый узел и переносим старые данные
            UpdateNodes(newNodeWithOldData, currentNode.Next);      // корректируем ссылки следующего узла
            UpdateNodes(currentNode, newNodeWithOldData);           // текущий узел ссылается на новый
            currentNode.Data = value;                               // в текущий узел записываем новое значение

            // Если вставка была перед хвостом, обновляем хвост списка
            if (_tail == currentNode)
            {
                _tail = newNodeWithOldData;
            }
        }
        
        /// <summary>
        /// Удаляет элемент в позиции <paramref name="positionIndex"/>.
        /// </summary>
        /// <remarks>
        /// 
        /// 1) Если удаляем первый элемент:
        ///     Если список из одного элемента, делаем голову и хвост null.
        ///     Если в списке есть элементы, переносим head на следующую позицию,
        ///     если следующая позиция не пустая, то в новой голове ставим ссылку предыдущего null.
        /// 2) Если удаляем хвост:
        ///     Переносим хвост на предыдущий,
        ///     Ссылка на следующий в новом хвосте null.
        /// Проверяем существование позиции.
        /// 3) Переносим ссылки предыдущего на следующий, тем самым удаляя текущий. 
        /// </remarks>
        /// <param name="positionIndex">Позиция.</param>
        public void Delete(PositionIndex<T> positionIndex)
        {
            if (positionIndex.Node == _head)
            {
                if (_head == _tail)
                {
                    MakeNull();
                    
                    return;
                }
                
                _head = _head!.Next;
                if (_head != null)
                {
                    _head.Previous = null;
                }

                return;
            }

            if (positionIndex.Node == _tail)
            {
                _tail = _tail!.Previous;
                _tail!.Next = null;

                return;
            }
            ValidatePosition(positionIndex);
            UpdateNodes(positionIndex.Node!.Previous!, positionIndex.Node!.Next!);
        }
        
        /// <summary>
        /// Ищет первое вхождение <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Проходимся по списку пока <paramref name="value"/> не совпадет с данными в списке.
        /// Возращаем _end, если вхождений нет.
        /// </remarks>
        /// <param name="value">Элемент списка.</param>
        /// <returns>Position<T>.</returns>
        public PositionIndex<T> Locate(T value)
        {
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    return new PositionIndex<T>(current);
                }
                current = current.Next;
            }
            return _end;
        }
        
        /// <summary>
        /// Получает данные по позиции.
        /// </summary>
        /// <remarks>
        /// Проверяем существует ли позиция
        /// Возвращаем данные.
        /// </remarks>
        /// <param name="positionIndex">Позиция</param>
        /// <returns>Данные в узле в данной позиции.</returns>
        public T Retrieve(PositionIndex<T> positionIndex)
        {
            ValidatePosition(positionIndex);
            
            return positionIndex.Node!.Data;
        }
        
        /// <summary>
        /// Получает <see cref="PositionIndex{T}"/> следующего за <paramref name="positionIndex"/>.
        /// </summary>
        /// <remarks>
        /// Проверяем существует ли позиция.
        /// Если позиция равна хвосту, то возращаем _end, иначе следующую позицию.
        /// </remarks>
        /// <param name="positionIndex">Позиция.</param>
        /// <returns><see cref="PositionIndex{T}<T>"/>.</returns>
        public PositionIndex<T> Next(PositionIndex<T> positionIndex)
        {
            ValidatePosition(positionIndex);
    
            return positionIndex.Node == _tail 
                ? _end 
                : new PositionIndex<T>(positionIndex.Node!.Next);
        }

        /// <summary>
        /// Получает позицию предыдущего элемента.
        /// </summary>
        /// <remarks>
        /// Проверяем что позиция сущесвует и не _head.
        /// Возращаем предыдущую позицию.
        /// </remarks>
        /// <param name="positionIndex">Позиция.</param>
        /// <returns><see cref="PositionIndex{T}<T>"/>.</returns>
        /// <exception cref="PositionException">Выбрасывается, если <paramref name="positionIndex"/> не имеет предыдущего.</exception>
        public PositionIndex<T> Previous(PositionIndex<T> positionIndex)
        {
            ValidatePosition(positionIndex);
            if (positionIndex.Node == _head)
            {
                throw new PositionException("Данная позиция не имеет предыдущего");
            }

            return new PositionIndex<T>(positionIndex.Node!.Previous);
        }

       /// <summary>
       /// Получает первый элемент списка.
       /// </summary>
       /// <remarks>
       ///  Если список пустой, вернем _end, иначе позицию головы.
       /// </remarks>
       /// <returns>Position<T>.</returns>
        public PositionIndex<T> First()
        {
            return _head == null 
                ? _end 
                : new PositionIndex<T>(_head);
        }

        /// <summary>
        /// Отчищает список.
        /// </summary>
        public void MakeNull()
        {
            _head = _tail = null;
        }
        
        /// <summary>
        /// Формирует строковое представление списка.
        /// </summary>
        /// <returns>Список в виде строки.</returns>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            Node<T>? current = _head;
            if (current != null)
            {
                sb.Append(current.Data); // первый элемент
                current = current.Next;

                while (current != null) // остальные элементы
                {
                    sb.Append('\n');
                    sb.Append(current.Data);
                    current = current.Next;
                }
            }
            
            return sb.ToString();
        }
        
        /// <summary>
        /// Проверяет сущестовование позиции.
        /// </summary>
        /// <param name="positionIndex">Позиуия</param>
        /// <returns>Если позиция существует, true, если нет false.</returns>
        private bool CheckPosition(PositionIndex<T> positionIndex)
        {
            if (positionIndex == _end)
            {
                return false;
            }
            
            Node<T> current = _head;

            while (current != null)
            {
                if (current == positionIndex.Node)
                {
                    return true;
                }
                
                current = current.Next;
            }
            
            return false;
        }

        /// <summary>
        /// Обновляет ссылки двух узлов.
        /// </summary>
        /// <remarks>
        /// Для <paramref name="first"/> следующий будет <paramref name="second"/>.
        /// Для <paramref name="second"/> предыдущий будет <paramref name="first"/>.
        /// </remarks>
        /// <param name="first">Первый узел.</param>
        /// <param name="second">Второй узел.</param>
        private void UpdateNodes(Node<T> first, Node<T> second)
        {
            if (first != null)
            {
                first.Next = second;
            }

            if (second != null)
            {
                second.Previous = first;
            }
        }
        
        /// <summary>
        /// Валидирует позицию.
        /// </summary>
        /// <param name="positionIndex">Позиция.</param>
        /// <exception cref="PositionException">Если позиция не существует в списке</exception>
        private void ValidatePosition(PositionIndex<T> positionIndex)
        {
            if (!CheckPosition(positionIndex))
            {
                throw new PositionException("Данная позиция не существует в списке!"); 
            }
        }
    }
}