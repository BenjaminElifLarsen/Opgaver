using System;

namespace Reaktions_spil
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }


    public class Timer
    {
        private static DateTime time = new DateTime();
        private static DateTime startTime;
        Timer()
        {

        }

        public static DateTime Time { get => time; }

        public static DateTime SetTime()
        {
            startTime = DateTime.Now;
        }

        public static double TimePassed { get => (DateTime.Now - startTime).TotalMilliseconds; }
    }

    public class Game
    {
        double countup = 0; //miliseconds
        ConsoleKey gameStart = ConsoleKey.W;
        ConsoleKey player1ReactionKey = ConsoleKey.Enter;
        private static Random rnd = new Random();

        Game()
        {
            Run();
        }

        
        private void Run()
        {
            Console.WriteLine("Press {0} to start.", gameStart);
            do
            {
                
                while (!Console.KeyAvailable) ;
                if (Console.ReadKey(true).Key == gameStart) //delegate/event this
                {
                    Timer.SetTime();
                    double playerTimer = 0;
                    float timeToCountDown = rnd.Next(0, 8) + (rnd.Next(0, 1000) / 1000f);
                    countup = 0;
                    while (countup < timeToCountDown)
                    {
                        countup = Timer.TimePassed;
                        while (Console.KeyAvailable)
                            Console.ReadKey(true); //flush the buffer
                    }
                    Console.WriteLine('O');
                    Timer.SetTime();
                    while (!Console.KeyAvailable && Console.ReadKey(true).Key != player1ReactionKey) ;
                    playerTimer = Timer.TimePassed;
                    Console.WriteLine("Time spent: {0} ms.", playerTimer);
                }


                while (Console.KeyAvailable)
                    Console.ReadKey(true); //flush the buffer

            } while (true);

        }


    }
}
