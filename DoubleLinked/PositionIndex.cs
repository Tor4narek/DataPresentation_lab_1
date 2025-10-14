namespace DoubleLinked;

/// <summary>
/// Позиция.
/// </summary>
/// <typeparam name="T">Любой IEquatable.</typeparam>
public class PositionIndex<T>
{
    /// <summary>
    /// Узел.
    /// </summary>
    internal Node<T>? Node  { get; init; }
    
    /// <summary>
    /// Создает новый Position c <paramref name="node"/>.
    /// </summary>
    /// <param name="node">Узел.</param>
    internal PositionIndex(Node<T>? node)
    {
        Node = node;
    }
}