namespace Lab1.CursorList;

/// <summary>
/// Позиция.
/// </summary>
public class PositionIndex
{
    /// <summary>
    /// Индекс позиции.
    /// </summary>
    internal int Position;
    
    /// <summary>
    /// Инициализирует позицию.
    /// </summary>
    /// <param name="i">Индекс позиции.</param>
    internal PositionIndex(int i)
    {
        Position = i;
    }

    /// <summary>
    ///  Сравнивает позиции.
    /// </summary>
    /// <param name="position">Позиция.</param>
    /// <returns></returns>
    internal bool Equals(PositionIndex position)
    {
        return Position == position.Position;
    }
}