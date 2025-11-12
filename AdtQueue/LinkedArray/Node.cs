namespace AdtQueue.LinkedArray;

/// <summary>
/// Узел.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Node<T>
{
    /// <summary>
    /// Данные узла.
    /// </summary>
    public T Data {get; set;}
    
    /// <summary>
    /// Ссылка на следующий узел.
    /// </summary>
    public Node<T> Next { get; set; }

    /// <summary>
    /// Инициализация узла.
    /// </summary>
    /// <param name="data"> Данные узла. </param>
    /// <param name="next"> Ссылка на следующий узел. </param>
    public Node(T data, Node<T> next)
    {
        Data = data;
        Next = next;
    }
}