using System.Text;

namespace AdtMap.LinkedArray
{
    /// <summary>
    /// Представляет обобщённое отображение (ассоциативный массив), 
    /// реализованное на основе односвязного списка.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей, используемых в отображении. 
    /// Должен реализовывать интерфейс <see cref="IEquatable{TKey}"/>.</typeparam>
    /// <typeparam name="TValue">Тип значений, связанных с ключами.</typeparam>
    public class MyMap<TKey, TValue> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Голова связного списка, содержащего пары ключ-значение.
        /// </summary>
        private Node _head;

        /// <summary>
        /// Добавляет новую пару ключ-значение в отображение или 
        /// изменяет значение существующего ключа.
        /// </summary>
        /// <param name="key">Ключ элемента.</param>
        /// <param name="value">Значение, связанное с ключом.</param>
        public void Assign(char[] key, char[] value)
        {
            if (_head == null)
            {
                _head = new Node(key, value, null);
                return;
            }
            
            Node node = null;
            if (GetNodeByKey(key, ref node))
            {
                node.Value = value;
                
                return;
            }

            node.Next = new Node(key, value, null);
        }

        /// <summary>
        /// Пытается найти значение по указанному ключу.
        /// </summary>
        /// <param name="key">Ключ, по которому выполняется поиск.</param>
        /// <param name="value">
        /// Ссылка на переменную, в которую будет записано найденное значение (если найдено).
        /// </param>
        /// <returns>
        /// <c>true</c>, если элемент с указанным ключом найден; 
        /// в противном случае — <c>false</c>.
        /// </returns>
        public bool Compute(char[] key, ref char[] value)
        {
            Node node = null;
            if (!GetNodeByKey(key, ref node))
            {
                return false;
            }

            value = node.Value;
            return true;
        }

        /// <summary>
        /// Очищает отображение, удаляя все элементы.
        /// </summary>
        public void MakeNull()
        {
            _head = null;
        }

        /// <summary>
        /// Возвращает строковое представление отображения в виде {key=value, key=value, ...}.
        /// </summary>
        /// <returns>Строка, содержащая пары ключ-значение.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node node = _head;

            sb.Append('{');
            if (node != null)
            {
                sb.Append(node);
                node = node.Next;

                while (node != null)
                {
                    sb.Append(',');
                    sb.Append(node);
                    node = node.Next;
                }
            }
            sb.Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Выполняет поиск узла по заданному ключу.
        /// </summary>
        /// <param name="key">Ключ, который нужно найти.</param>
        /// <param name="node">
        /// Ссылка на переменную, в которую будет помещён найденный узел 
        /// или последний просмотренный элемент списка.
        /// </param>
        /// <returns>
        /// <c>true</c>, если элемент с указанным ключом найден; 
        /// в противном случае — <c>false</c>.
        /// </returns>
        private bool GetNodeByKey(char[] key, ref Node node)
        {
            Node current = _head;
            Node previous = null;

            while (current != null)
            {
                if (current.EqualsKey(key))
                {
                    node = current;
                    
                    return true;
                }

                previous = current;
                current = current.Next;
            }

            node = previous;
            
            return false;
        }
    }
}
