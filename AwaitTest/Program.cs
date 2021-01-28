using System;
using System.Threading.Tasks;

namespace AwaitTest
{
    class Program
    {
        /// <summary>
        /// Вариант 1. Без async
        /// </summary>
        /// <returns></returns>
        private static Task FailedNoAsync()
        {
            throw new Exception("ex no async-await"); // исключение для тестирования поведения
            return Task.CompletedTask;
        }

        /// <summary>
        /// Вариант 2. Пишем async
        /// </summary>
        /// <returns></returns>
        private static async Task FailedWithAsync()
        {
            throw new Exception("ex async-await"); // исключение для тестирования поведения
            await Task.CompletedTask;
        }

        static void Main(string[] args)
        {
            TestNoAsync();
            Console.WriteLine();
            TestWithAsync();
            Console.ReadLine();
        }

        public static void TestNoAsync()
        {
            try
            {
                Console.WriteLine("Тест без async");
                var tasks = new[] 
                {
                    FailedNoAsync(), // <-- тут будет исключение
                    WorkAsync() // выполнение WorkAsync зависит от порядка элементов
                };
                Task.WaitAll(tasks); // <-- сюда не дойдём
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void TestWithAsync()
        {
            try
            {
                Console.WriteLine("Тест с async");
                var tasks = new[] 
                { 
                    FailedWithAsync(), 
                    WorkAsync() // выполнится независимо от порядка элементов
                };
                Task.WaitAll(tasks); // <-- тут будет исключение
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async Task WorkAsync()
        {
            Console.WriteLine("WorkAsync completed");
            await Task.CompletedTask;
        }
    }
}
