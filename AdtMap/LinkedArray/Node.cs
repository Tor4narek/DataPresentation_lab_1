namespace AdtMap.LinkedArray
{
    public class Node
    {
        public char[] Key { get; set; } = new char[20];    // ключ фиксированной длины 20
        public char[] Value { get; set; } = new char[50];  // значение фиксированной длины 50
        public Node Next { get; set; }

        public Node(char[] key, char[] value, Node next)
        {
            // Копируем ключ и дополняем '\0', если меньше 20
            for (int i = 0; i < 20; i++)
                Key[i] = i < key.Length ? key[i] : '\0';

            // Копируем значение и дополняем '\0', если меньше 50
            for (int i = 0; i < 50; i++)
                Value[i] = i < value.Length ? value[i] : '\0';

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