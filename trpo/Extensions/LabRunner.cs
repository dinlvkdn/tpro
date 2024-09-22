using trpo.Lab1;
using trpo.Lab2;
using Timer = trpo.Lab2.Timer;

namespace trpo.Extensions
{
    public static class LabRunner
    {
        private static readonly object _lockObject = new();
        private static bool _isThread1Started = false;

        public static void RunFirst()
        {
            var guestBook = new GuestBook();

            guestBook.AddGuest(new GuestRecord("Пупкін Лупкін", DateTime.Now.AddHours(-3), DateTime.Now, 101));
            guestBook.AddGuest(new GuestRecord("Юрік Дурік", DateTime.Now.AddHours(-0.5), DateTime.Now, 102));
            guestBook.AddGuest(new GuestRecord("Машка Какашка", DateTime.Now.AddHours(-5), DateTime.Now, 103));

            guestBook.DisplayAllGuests();

            guestBook.DisplayGuestsStayedMoreThanOneHour();

            guestBook.SortByRoomNumber();
            guestBook.DisplayAllGuests();

            guestBook.SaveToFile("guests.txt");

            var newGuestBook = new GuestBook();
            newGuestBook.LoadFromFile("guests.txt");
            newGuestBook.DisplayAllGuests();
        }

        public static void RunSecond()
        {
            var timer = new Timer();

            var thread1 = new Thread(() => ComputeWithThread(1.5, timer, 100));
            var thread2 = new Thread(() => ComputeWithThread(6.5, timer, 10));

            timer.Start();

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            timer.Stop();

            Console.WriteLine($"Час виконання: {timer.ElapsedMilliseconds} мс");
        }

        static void ComputeWithThread(double x, Timer timer, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                double result = Calculator.Calculate(i);
                Console.WriteLine($"Результат обчислення для x = {i} : {result}");
            }
        }

        public static void RunThird()
        {
            var timer = new Timer();

            var thread1 = new Thread(() => ComputeWithThread1(1.5, timer, 10));
            var thread2 = new Thread(() => ComputeWithThread2(6.5, timer, 10));

            timer.Start();

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            timer.Stop();

            Console.WriteLine($"Час виконання: {timer.ElapsedMilliseconds} мс");
        }

        static void ComputeWithThread1(double x, Timer timer, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                double result = Calculator.Calculate(i);
                Console.WriteLine($"Результат обчислення для потоку 1, x = {i} : {result}");

                if (i == 0)
                {
                    lock (_lockObject)
                    {
                        _isThread1Started = true;
                        Monitor.PulseAll(_lockObject);
                    }

                    Thread.Sleep(3200);
                }

                Thread.Sleep(500);
            }
        }

        // Потік 2
        static void ComputeWithThread2(double x, Timer timer, int iterations)
        {
            lock (_lockObject)
            {
                while (!_isThread1Started)
                {
                    Monitor.Wait(_lockObject);
                }
            }

            for (int i = 0; i < iterations; i++)
            {
                double result = Calculator.Calculate(i);
                Console.WriteLine($"Результат обчислення для потоку 2, x = {i} : {result}");

                Thread.Sleep(500);
            }
        }
    }
}
