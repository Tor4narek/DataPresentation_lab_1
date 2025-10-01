using System.Text;

namespace Lab1.CursorList;

/// <summary>
/// Список на курсорах.
/// </summary>
/// <typeparam name="T"></typeparam>
public class MyList<T> where T : IEquatable<T>
{
    /// <summary>
    /// Массив узлов.
    /// </summary>
    private static Node<T>[] _nodes;
    
    /// <summary>
    /// Индекс первого элемента.
    /// </summary>
    private int _start = -1;      
    
    /// <summary>
    /// Индекс первого свободного элемента.
    /// </summary>
    private static int _space = 0;  
    
    /// <summary>
    /// Условный конец списка.
    /// </summary>
    private readonly PositionIndex _end = new PositionIndex(-1);
    
    /// <summary>
    /// Максимальная длина списка.
    /// </summary>
    private const int MaxLen = 100;
    
    /// <summary>
    /// Инициализирует массив узлов и строит список свободных.
    /// </summary>
    /// <remarks>
    ///  Создаем массив узлов, максимальной длины,
    ///  каждая ячейка указывает на следующую свободну.
    /// </remarks>
    static MyList()
    {
        _nodes = new Node<T>[MaxLen];
        const int Size = MaxLen - 1;
        for (int i = 0; i < Size; i++)
        {
            _nodes[i] = new Node<T>
            {
                Next = i+1,
            };
        }
        // Последний свободный указывает на -1.
        _nodes[Size] = new Node<T>
        {
            Next = -1
        };
    }
    
    /// <summary>
    /// Возвращает "фиктивную" позицию конца списка (всегда -1).
    /// </summary>
    /// <remarks>
    /// Используется как маркер: например, вставка в End означает вставку в конец.
    /// </remarks>
    /// <returns></returns>
    public PositionIndex End()
    {
        return _end;
    }
    
    /// <summary>
    /// Возвращает первую позицию в списке.
    /// </summary>
    /// <remarks>
    /// Если список пуст, возращает End().
    /// </remarks>
    /// <returns>PositionIndex.</returns>
    public PositionIndex First() => IsEmpty()
        ? End() 
        : new PositionIndex(_start);
    
    /// <summary>
    /// Вставляет <paramref name="value"/> в <paramref name="position"/>.
    /// </summary>
    /// <remarks>
    /// Проверяем не переполнен ли список
    /// 1) Вставка в конец:
    ///     Если список пустой, то начало списка это первый свободный,
    ///     Начало свободных теперь на следущем узле.
    ///     Записываем данные в начало списка
    ///     Ссылка на следующий будет -1.
    ///
    ///     Если список не пустой:
    ///     Находим последний индекс списка.
    ///     Берём свободный узел из массива.
    ///     Заполняем новый узел данными и делаем его последним.
    ///     Присоединяем новый узел к концу списка.
    /// 2) Вставка перед произвольной позицией внутри списка:
    ///     Проверяем, что позиция существует.
    ///     Берём свободный узел и записываем туда старые данные текущей позиции.
    ///     В текущий узел записываем новое значение.
    ///     Обновляем ссылки:
    ///     Новый узел с предыдущими данными указывает на следующий элемент текущей позиции.
    ///     Текущая позиция указывает на новый узел.
    /// 
    ///     Таким образом мы вставляем новый элемент перед указанной позицией, 
    ///     а старые данные сдвигаются на новый узел.
    /// </remarks>
    /// <param name="value">Значение.</param>
    /// <param name="position">Позиция.</param>
    /// <exception cref="InvalidOperationException">Если список переполнен.</exception>
    /// <exception cref="PositionException">Если позиция не существует.</exception>
    public void Insert(PositionIndex position, T value)
    {
        if (position.Position == -1) // вставка в конец
        {
            if (IsEmpty()) // список пуст
            {
                _start = _space;
                _space = _nodes[_space].Next;
                _nodes[_start].Data = value;
                _nodes[_start].Next = -1;
                return;
            }
            
            int last = FindLast();
            int newNode = _space;
            _space = _nodes[_space].Next;

            _nodes[newNode].Data = value;
            _nodes[newNode].Next = -1;
            _nodes[last].Next = newNode;
            
            return;

        }

        CheckPosition(position.Position);
            
        int tmp = _space;
        _space = _nodes[_space].Next;

        // Перенос старых данных текущей позиции в новый узел
        _nodes[tmp].Data = _nodes[position.Position].Data;
        _nodes[tmp].Next = _nodes[position.Position].Next;

        // В текущий узел записываем новое значение
        _nodes[position.Position].Data = value;
        _nodes[position.Position].Next = tmp;
    }

    /// <summary>
    /// Удалаяет элемент из списка по <paramref name="position"/>.
    /// </summary>
    /// <remarks>
    /// 1) Проверка позиции:
    ///     Если позиция некорректная или отсутствует в списке, выбрасывается исключение <see cref="PositionException"/>.
    ///     
    /// 2) Удаление первого элемента:
    ///     Сдвигаем начало списка на следующий элемент.
    ///     Освобожденный узел возвращается в список свободных узлов, чтобы его можно было использовать для будущих вставок.
    ///     
    /// 3) Удаление элемента из середины или конца:
    ///     Находим предыдущий элемент относительно удаляемого.
    ///     Переправляем ссылку предыдущего элемента на следующий за удаляемым.
    ///     Освобожденный элемент возвращается в список свободных узлов.
    ///
    /// Таким образом удаляемый элемент исключается из основного списка, 
    /// а его место становится доступным для будущих вставок.
    /// </remarks>
    /// <param name="position">Позиуия.</param>
    /// <exception cref="PositionException">Если позиция отсутсвует в списке.</exception>
    public void Delete(PositionIndex position)
    {
        int tmp;

        if (position.Position == _start)
        {
            tmp = _space;
            _space = _start;
            _start = _nodes[_start].Next;
            _nodes[_space].Next = tmp;

            return;
        }

        int previous = CheckPosition(position.Position);
        
        int current = _nodes[previous].Next;
        _nodes[previous].Next = _nodes[current].Next;
        
        tmp = _space;
        _space = current;

        _nodes[_space].Next = tmp;
    }

    /// <summary>
    /// Возращает первое вхождение <paramref name="value"/> в списке. 
    /// </summary>
    /// <remarks>
    /// Идем от старта списка до конца, если находится <paramref name="value"/> возращаем позицию.
    /// Иначе End().
    /// </remarks>
    /// <param name="value">Зачение.</param>
    /// <returns>PositionIndex.</returns>
    public PositionIndex Locate(T value)
    {
        int current = _start;
        
        while (current != -1)
        {
            if (_nodes[current].Data!.Equals(value))
                return new PositionIndex(current);

            current = _nodes[current].Next;
        }
        
        return End();
    }
    
    /// <summary>
    /// Возращает значение элемента по <paramref name="position"/>
    /// </summary>
    /// <param name="position">Позиция.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public T Retrieve(PositionIndex position)
    {
        CheckPosition(position.Position);

       return _nodes[position.Position].Data!;
    }
    
    /// <summary>
    /// Возращает следующую позицию после <paramref name="position"/>.
    /// </summary>
    /// <param name="position">Позиция.</param>
    /// <returns>PositionIndex.</returns>
    public PositionIndex Next(PositionIndex position)
    {
        CheckPosition(position.Position);
        
        return _nodes[position.Position].Next == -1 
            ? _end 
            : new PositionIndex(_nodes[position.Position].Next);
    }
    
    /// <summary>
    /// Возращает предыдущую позицию после <paramref name="position"/>.
    /// </summary>
    /// <param name="position">Позиция.</param>
    /// <returns>PositionIndex.</returns>
    public PositionIndex Previous(PositionIndex position)
    {
        int previous = CheckPosition(position.Position);
        
        return new PositionIndex(previous);
    }

    /// <summary>
    /// Делает список пустым.
    /// </summary>
    /// <remarks>
    /// Возращаем все элементы в список свободных.
    /// </remarks>
    public void MakeNull()
    {
        _start = -1;

        for (int i = 0; i < MaxLen - 1; i++)
        {
            _nodes[i].Next = i + 1;
        }
        
        _nodes[MaxLen - 1].Next = -1;
        _space = 0;
    }

    /// <summary>
    /// Формирует строкоевое представление списка.
    /// </summary>
    /// <returns>Значения списка в виде строки.</returns>
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        int current = _start;
        
        while (current != -1)
        {
            result.Append(_nodes[current].Data);
            result.Append('\n');
            current = _nodes[current].Next;
        }
        
        return result.ToString();
    }
    
    /// <summary>
    /// Находит индекс предыдущего узла для <paramref name="index"/>.
    /// </summary>
    /// <param name="index">Индекс узла.</param>
    /// <returns><see cref="int"/>.</returns>
    /// <exception cref="PositionException">Если, позиция не существует в списке.</exception>
    private int Before(int index)
    {
        int current = _start;
        int previous = -1;
        
        while (current != -1)
        {
            if (current == index) return previous;
            previous = current;
            current = _nodes[current].Next;
        }

        throw new PositionException("Данная позиция не существует.");
    }
    
    /// <summary>
    /// Находит индекс последнего элемента списка.
    /// </summary>
    /// <returns><see cref="int"/>.</returns>
    private int FindLast()
    {
        if (IsEmpty())
        {
            return -1;
        }
        
        int current = _start;
        int previous = -1;
        while (current != -1)
        {
            previous = current;
            current = _nodes[current].Next;
        }
        return previous;
    }
    
    /// <summary>
    /// Проверяет пустой ли список.
    /// </summary>
    /// <returns></returns>
    private bool IsEmpty()
    {
        return _start == -1;
    }

    private int CheckPosition(int position)
    {
        var previous = Before(position);
        return previous;
    }
}


