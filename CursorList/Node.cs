namespace CursorList;

/// <summary>
/// Узел.
/// </summary>
/// <typeparam name="T">Любой IEquatable.</typeparam>
internal class Node<T>
{
    /// <summary>
    /// Значение.
    /// </summary>
    internal T Data { get; set; }
    
    /// <summary>
    /// Индекс следующего.
    /// </summary>
    internal int Next { get; set; }
    
    /// <summary>
    /// Создает объект Node<T>.
    /// </summary>
    internal Node() {}
    
    /// <summary>
    /// Создает объект Node<T> c <paramref name="data"/> и <paramref name="next"/>.
    /// </summary>
    /// <param name="data">Значение.</param>
    /// <param name="next">Индекс следующего.</param>
    internal Node(T data, int next)
    {
        Data = data;
        Next = next;
    }
}