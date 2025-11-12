namespace AdtMap.LinkedArray
{
    /// <summary>
    /// Узел.
    /// </summary>
    public class Node
    {
        public char[] Key { get; set; } = new char[20];    // ключ фиксированной длины 20
        public char[] Value { get; set; } = new char[50];  // значение фиксированной длины 50
        public Node Next { get; set; }

        public Node(char[] key, char[] value, Node next)
        {
            int i = 0;
            while (i < key.Length && i <= 20 && key[i] != '\0')
            {
                Key[i] = key[i];
                i++;
            }

            // Копируем значение до терминального элемента или до длины массива 50
            int j = 0;
            while (j < value.Length && j <= 50 && value[j] != '\0')
            {
                Value[j] = value[j];
                j++;
            }
                

            Next = next;
        }
        
        // Сравнение ключей с учётом терминальных элементов
        public bool EqualsKey(char[] otherKey)
        {
            for (int i = 0; i < 20; i++)
            {
                if (Key[i] == '\0' && (i >= otherKey.Length || otherKey[i] == '\0'))
                    break; // достигли терминального элемента
                if (i >= otherKey.Length || Key[i] != otherKey[i])
                    return false;
            }
            return true;
        }



        // Преобразование в строку для вывода
        public override string ToString()
        {
            string keyStr = new string(Key).TrimEnd('\0');
            string valueStr = new string(Value).TrimEnd('\0');
            return $"{keyStr}:{valueStr}";
        }
    }
}