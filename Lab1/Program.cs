using CursorList;

namespace Lab1;

/// <summary>
/// Демонстрация работы списка адресатов <see cref="List{T}"/> с типом <see cref="Recipient"/>.
/// </summary>
public class Program
{
    /// <summary>
    /// Главная точка входа программы.
    /// </summary>
    public static void Main()
    {
        MyList<Recipient> list = new MyList<Recipient>(); // создаем пустой список
        FillList(list);                                // заполняем тестовыми данными
        Console.WriteLine("Исходный список:");
        Console.WriteLine(list);                             // вывод исходного списка
        Console.WriteLine();
        
        DeleteDuplicates(list);                        // удаляем дубликаты
        Console.WriteLine("Список после удаления дубликатов:");
        Console.WriteLine(list);                              // вывод после удаления дубликатов
    }

    /// <summary>
    /// Заполняет список <paramref name="list"/> тестовыми данными с повторами.
    /// </summary>
    /// <param name="list">Список адресатов для заполнения.</param>
    private static void FillList(MyList<Recipient> list)
    {
        list.Insert(list.End(), new Recipient("Антон Смирнов", "Калуга, ул. Московская 12"));
        list.Insert(list.End(), new Recipient("Антон Смирнов", "Калуга, ул. Московская 12"));
        list.Insert(list.End(), new Recipient("Антон Смирнов", "Калуга, ул. Московская 12"));
        list.Insert(list.End(), new Recipient("Екатерина Иванова", "Сочи, ул. Виноградная 5"));
        list.Insert(list.End(), new Recipient("Петр Петров", "Тверь, пр. Ленина 7"));
        list.Insert(list.End(), new Recipient("Марина Соколова", "Владивосток, ул. Светланская 21"));
        list.Insert(list.End(), new Recipient("Игорь Кузнецов", "Тюмень, ул. Мира 10"));
        list.Insert(list.End(), new Recipient("Анна Морозова", "Краснодар, ул. Красная 20"));
        list.Insert(list.End(), new Recipient("Екатерина Иванова", "Сочи, ул. Виноградная 5")); // Повторение
        list.Insert(list.End(), new Recipient("Сергей Лебедев", "Ярославль, ул. Свободы 18"));
        list.Insert(list.End(), new Recipient("Ольга Федорова", "Ижевск, ул. Пушкина 3"));
        list.Insert(list.End(), new Recipient("Игорь Кузнецов", "Тюмень, ул. Мира 10")); // Повторение
        list.Insert(list.End(), new Recipient("Наталья Орлова", "Уфа, ул. Октябрьской революции 14"));
        list.Insert(list.End(), new Recipient("Владислав Романов", "Киров, ул. Ленина 12"));
        list.Insert(list.End(), new Recipient("Марина Соколова", "Владивосток, ул. Светланская 21")); // Повторение
        list.Insert(list.End(), new Recipient("Тимур Ахметов", "Саратов, ул. Московская 22"));
        list.Insert(list.End(), new Recipient("Александра Белова", "Ростов-на-Дону, ул. Большая Садовая 5"));
    }

    /// <summary>
    /// Удаляет все дубликаты из <paramref name="list"/>.
    /// </summary>
    /// <remarks>
    /// Алгоритм проходит по каждому элементу списка и сравнивает его со всеми последующими.
    /// Если встречается элемент с такими же данными (имя и адрес), он удаляется.
    /// В результате остаются только уникальные элементы.
    /// </remarks>
    /// <param name="list">Список адресатов, из которого нужно удалить дубликаты.</param>
    private static void DeleteDuplicates(MyList<Recipient> list)
    { 
        var position = list.First(); // получение первой позиции списка
        var end = list.End(); // получение позиции после последнего
    
        while (position != end) // пока список не кончится
        {
           var next = list.Next(position); // получаем следующую позицию за проверяемой
            while (next != end)
            {
                if (list.Retrieve(position).Equals(list.Retrieve(next))) // если объекты совпали
                {
                   var deleted = next; // запоминаем удаляемую позицию
                    next = list.Next(next); 
                    list.Delete(deleted); // удаляем дубликат
                }
                else
                {
                    next = list.Next(next); // если не удаляли дубликат, переходим к следующей позиции
                }
            }
    
            position = list.Next(position); // ищем дубликаты для следующего элемента
        }
    }
}
