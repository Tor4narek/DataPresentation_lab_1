namespace Lab1.DoubleLinked;

/// <summary>
/// Узел.
/// </summary>
/// <param name="data">Значение.</param>
/// <param name="prev">Ссылка на предыдущий узел.</param>
/// <param name="next">Ссылка на следующий узел.</param>
/// <typeparam name="T">Любой IEquatable.</typeparam>
internal class Node<T>
{
    /// <summary>
    /// Значение.
    /// </summary>
    internal T Data { get; set; }  
    
    /// <summary>
    /// Ссылка на предыдущий узел.
    /// </summary>
    internal Node<T>? Previous { get; set; }
    
    /// <summary>
    /// Ссылка на следующий узел.
    /// </summary>
    internal Node<T>? Next { get; set; }

    /// <summary>
    /// Иниуиализирует поля Node.
    /// </summary>
    /// <param name="data">Значение.</param>
    /// <param name="previous">Ссылка на предыдущий узел.</param>
    /// <param name="next">Ссылка на следующий узел.</param>
    internal Node(T data, Node<T>? previous = null, Node<T>? next = null) 
    {
        Data = data;
        Previous = previous;
        Next = next;
    }
}