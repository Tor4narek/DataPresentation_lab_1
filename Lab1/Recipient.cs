namespace Lab1;

/// <summary>
/// Представляет адресата с именем и адресом.
/// </summary>
public class Recipient : IEquatable<Recipient>
{
    /// <summary>
    /// Имя адресата (максимум 20 символов + завершающий '\0').
    /// </summary>
    public readonly char[] Name = new char[21];

    /// <summary>
    /// Адрес адресата (максимум 50 символов + завершающий '\0').
    /// </summary>
    public readonly char[] Address = new char[51];

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="Recipient"/> с указанными именем и адресом.
    /// </summary>
    /// <param name="name">Имя адресата.</param>
    /// <param name="address">Адрес адресата.</param>
    public Recipient(string name, string address)
    {
        // Копируем имя
        int nameSize = Math.Min(name.Length, 20);
        for (int i = 0; i < nameSize; i++)
        {
            Name[i] = name[i];
        }
        Name[nameSize] = '\0'; // завершающий нуль

        // Копируем адрес
        int addressSize = Math.Min(address.Length, 50);
        for (int i = 0; i < addressSize; i++)
        {
            Address[i] = address[i];
        }
        Address[addressSize] = '\0'; // завершающий нуль
    }

    /// <summary>
    /// Сравнивает текущий экземпляр <see cref="Recipient"/> с другим объектом того же типа.
    /// </summary>
    /// <param name="other">Другой объект <see cref="Recipient"/> для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если объекты равны, иначе <c>false</c>.</returns>
    public bool Equals(Recipient? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        for (int i = 0; i < Name.Length; i++)
        {
            if (Name[i] != other.Name[i]) return false;
        }

        for (int i = 0; i < Address.Length; i++)
        {
            if (Address[i] != other.Address[i]) return false;
        }

        return true;
    }

    /// <summary>
    /// Сравнивает текущий экземпляр с указанным объектом.
    /// </summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>Возвращает <c>true</c>, если объекты равны, иначе <c>false</c>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Recipient);

    /// <summary>
    /// Возвращает хэш-код для текущего экземпляра <see cref="Recipient"/>.
    /// </summary>
    /// <returns>Хэш-код.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(
            new string(Name).TrimEnd('\0'),
            new string(Address).TrimEnd('\0')
        );
    }

    /// <summary>
    /// Возвращает строковое представление адресата в формате "Имя: Адрес".
    /// </summary>
    /// <returns>Строковое представление адресата.</returns>
    public override string ToString()
    {
        string nameStr = new string(Name).TrimEnd('\0');
        string addressStr = new string(Address).TrimEnd('\0');
        return $"{nameStr}: {addressStr}";
    }
}
