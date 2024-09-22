using trpo.Lab1;
using trpo.Lab2;
using Timer = trpo.Lab2.Timer;

namespace trpo.Extensions
{
    public static class LabRunner
    {
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
    }
}
