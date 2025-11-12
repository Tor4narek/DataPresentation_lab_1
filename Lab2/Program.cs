using AdtMap.LinkedArray;
using AdtQueue.ADTList;
using AdtStack.ADTList;
using DoubleLinked;

namespace Lab2
{
    public class Program
    {
        private const string MyWord = "Hello world";
        public static void Main()
        {
            RunMap();
            RunQueue();
            RunStack();
        }

        /// <summary>
        /// Демонстрирует работу с отображением (Map).
        /// </summary>
        private static void RunMap()
        {
            MyMap<char, int> myMap = new();

            var keys = new char[10][];
            var values = new char[10][];
            keys[0] = "Кречетов".ToCharArray();
            keys[1] = "Федерова".ToCharArray();
            values[0] = "Кронверский".ToCharArray();
            values[1] = "Ломоносова".ToCharArray();
            myMap.Assign(keys[0],values[0]);
            myMap.Assign(keys[1],values[0]);
            myMap.Assign(keys[0],values[1]);
            


            Console.WriteLine("Результат работы отображения:");
            Console.WriteLine(myMap);
            Console.WriteLine();
            Console.WriteLine("Проверка compute: Есть ли ключ Кречетов");
            Console.WriteLine(myMap.Compute(keys[0], ref values[0]));
            Console.WriteLine(values[0]);
        }

        /// <summary>
        /// Демонстрирует работу с очередью (Queue).
        /// </summary>
        private static void RunQueue()
        {
            MyQueue<char> myQueue = new();
            
            foreach (var i in MyWord)
            {
                myQueue.Enqueue(i);
            }

            Console.WriteLine("Результат работы очереди:");
            while (!myQueue.Empty())
            {
                Console.Write(myQueue.Dequeue());
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Демонстрирует работу со стеком (Stack).
        /// </summary>
        private static void RunStack()
        {
            MyStack<char> myStack = new();
            
            foreach (var i in MyWord)
            {
                myStack.Push(i);
            }

            Console.WriteLine("Результат работы стека:");
            while (!myStack.Empty())
            {
                Console.Write(myStack.Pop());
            }

            Console.WriteLine();
        }
    }
}