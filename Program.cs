using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    /// Класс, описывающий элемент связного списка.
    public class Item<T>
    {
        /// Хранимые данные.
        public T Data { get; set; }
        /// Следующий элемент связного списка.
        public Item<T> Next { get; set; }
        /// Создание нового экземпляра связного списка.
        public Item(T data)
        {
            // проверяем входящие данные
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Data = data;
        }
        /// Приведение объекта к строке.
        public override string ToString()
        {
            return Data.ToString();
        }
    }
    public class LinkedList<T> : IEnumerable<T>
    {
        /// Первый (головной) элемент списка.
        private Item<T> _head = null;
        /// Крайний (хвостовой) элемент списка. 
        private Item<T> _tail = null;
        /// Количество элементов списка.
        private int _count = 0;
        /// Количество элементов списка.
        public int Count
        {
            get => _count;
        }
        /// Добавить данные в связный список.
        public void Add(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            // Создаем новый элемент связного списка.
            var item = new Item<T>(data);

            // Если связный список пуст, то добавляем созданный элемент в начало,
            // иначе добавляем этот элемент как следующий за крайним элементом.
            if (_head == null)
            {
                _head = item;
            }
            else
            {
                _tail.Next = item;
            }

            // Устанавливаем этот элемент последним.
            _tail = item;
            // Увеличиваем счетчик количества элементов.
            _count++;
        }
        /// Удалить данные из связного списка.
        /// Выполняется удаление первого вхождения данных.
        public void Delete(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            // Текущий обозреваемый элемент списка.
            var current = _head;

            // Предыдущий элемент списка, перед обозреваемым.
            Item<T> previous = null;
            // Выполняем переход по всех элементам списка до его завершения,
            // или пока не будет найден элемент, который необходимо удалить.
            while (current != null)
            {
                // Если данные обозреваемого элемента совпадают с удаляемыми данными,
                // то выполняем удаление текущего элемента учитывая его положение в цепочке.
                if (current.Data.Equals(data))
                {
                    // Если элемент находится в середине или в конце списка,
                    // выкидываем текущий элемент из списка.
                    // Иначе это первый элемент списка,
                    // выкидываем первый элемент из списка.
                    if (previous != null)
                    {
                        // Устанавливаем у предыдущего элемента указатель на следующий элемент от текущего.
                        previous.Next = current.Next;

                        // Если это был последний элемент списка, 
                        // то изменяем указатель на крайний элемент списка.
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        // Устанавливаем головной элемент следующим.
                        _head = _head.Next;
                        // Если список оказался пустым,
                        // то обнуляем и крайний элемент.
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }

                    // Элемент был удален.
                    // Уменьшаем количество элементов и выходим из цикла.
                    // Для того, чтобы удалить все вхождения данных из списка
                    // необходимо не выходить из цикла, а продолжать до его завершения.
                    _count--;
                    break;
                }
                // Переходим к следующему элементу списка.
                previous = current;
                current = current.Next;
            }
        }
        /// Очистить список.
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }
        /// Вернуть перечислитель, выполняющий перебор всех элементов в связном списке.
        public IEnumerator<T> GetEnumerator()
        {
            // Перебираем все элементы связного списка, для представления в виде коллекции элементов.
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        /// Вернуть перечислитель, который осуществляет итерационный переход по связному списку.
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Просто возвращаем перечислитель, определенный выше.
            // Это необходимо для реализации интерфейса IEnumerable
            // чтобы была возможность перебирать элементы связного списка операцией foreach.
            return ((IEnumerable)this).GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем новый связный список.
            var list = new LinkedList<int>();
            int n;
            // Добавляем новые элементы в список.
            Console.Write($"Сколько элементов хотите внести ");
            n = Convert.ToInt16(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Элемент {i + 1} = ");
                list.Add(Convert.ToInt16(Console.ReadLine()));
            }
            // Выводим все элементы на консоль.
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.Write($"Какой элемент удалить? ");
            list.Delete(Convert.ToInt16(Console.ReadLine()));

            // Выводим все элементы еще раз.
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}