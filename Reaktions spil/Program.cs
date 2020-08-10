using System;
using System.Collections.Generic;

namespace Reaktions_spil
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }

    /// <summary>
    /// Class containing variables, properties and functions needed for time keeping for the game.
    /// </summary>
    public class Timer
    {
        private static DateTime time = new DateTime();
        private static DateTime startTime;
        Timer()
        {

        }

        public static DateTime Time { get => time; }

        public static void SetTime()
        {
            startTime = DateTime.Now;
        }

        public static double TimePassed { get => (DateTime.Now - startTime).TotalMilliseconds; }
    }

    /// <summary>
    /// Reaction game.
    /// </summary>
    public class Game
    {

        private static List<double> highscores = new List<double>();
        private double countup = 0; //miliseconds
        private const ConsoleKey gameStart = ConsoleKey.W;
        private ConsoleKey player1ReactionKey;
        private ConsoleKey player2ReactionKey;
        private static Random rnd = new Random();
        private const char sign = 'O';

        /// <summary>
        /// Basic constructor. 
        /// </summary>
        public Game()
        {
            Menu();
        }

        /// <summary>
        /// The game main menu. 
        /// </summary>
        public void Menu()
        {
            const ConsoleKey singleplayer = ConsoleKey.D1;
            const ConsoleKey multiplayer = ConsoleKey.D2;
            const ConsoleKey exit = ConsoleKey.D4;
            const ConsoleKey highscoreKey = ConsoleKey.D3;
            ConsoleKey pressedKey = ConsoleKey.D5;
            bool selected = false;
            while (true) {
                Console.Clear();
                Console.WriteLine("Press {0} for singleplayer. Press {1} for multiplayer. Press {2} for highscore. Press {3} to shutdown.", 
                    singleplayer.ToString()[singleplayer.ToString().Length-1], multiplayer.ToString()[multiplayer.ToString().Length - 1], highscoreKey.ToString()[highscoreKey.ToString().Length - 1], exit.ToString()[exit.ToString().Length - 1]);
                do
                {
                    while (!Console.KeyAvailable) ;
                    pressedKey = Console.ReadKey(true).Key;
                    if(pressedKey == singleplayer || pressedKey == multiplayer || pressedKey == exit || pressedKey == highscoreKey)
                            selected = true;

                    while (Console.KeyAvailable)
                        Console.ReadKey(true); //flush the buffer

                } while (!selected);
                selected = false;
                switch (pressedKey)
                {
                    case singleplayer:
                        SingleRun();
                        break;

                    case multiplayer:
                        MultiRun();
                        break;


                    case exit:
                        Environment.Exit(0);
                        break;

                    case highscoreKey:
                        HighscoreDisplay();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Displays the highscores. 
        /// </summary>
        private void HighscoreDisplay()
        {
            Console.Clear();
            if (highscores.Count != 0)
                foreach (double score in highscores)
                    Console.WriteLine(score);
            else
                Console.WriteLine("No highscore yet.");
            Wait();
        }

        /// <summary>
        /// Waits on a key press. 
        /// </summary>
        private void Wait()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Flushes the buffer of Console.ReadKey
        /// </summary>
        private void BufferFlush()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true); //flush the buffer
        }

        /// <summary>
        /// Multipleplayer game loop.
        /// </summary>
        private void MultiRun()
        {
            Console.Clear();
            bool pressedStartKey = false;
            player1ReactionKey = ConsoleKey.Spacebar;
            player2ReactionKey = ConsoleKey.Enter;
            bool player1Pressed = false;
            bool player2Pressed = false;

            Console.WriteLine("Press {0} to start. {1}Player 1: Pres {2} when you see {4}. {1}Player 2: Pres {3} when you see {4}."
                , gameStart, Environment.NewLine, player1ReactionKey, player2ReactionKey, sign);

            do
            {

                while (!Console.KeyAvailable) ;
                if (Console.ReadKey(true).Key == gameStart) //delegate/event this
                {
                    pressedStartKey = true;
                    Timer.SetTime();
                    double player1Timer = 0;
                    double player2Timer = 0;
                    float timeToCountDown = (rnd.Next(1, 8) + (rnd.Next(0, 1000) / 1000f)) * 1000;
                    countup = 0;
                    while (countup < timeToCountDown)
                    {
                        countup = Timer.TimePassed;
                        BufferFlush();
                    }
                    Console.WriteLine(sign);
                    Timer.SetTime();
                    do
                    {
                        while (!Console.KeyAvailable) ; 
                        ConsoleKey pressedKey = Console.ReadKey(true).Key;
                        if(pressedKey == player1ReactionKey && !player1Pressed)
                        {
                            player1Pressed = true;
                            player1Timer = Timer.TimePassed;
                        }else if(pressedKey == player2ReactionKey && !player2Pressed)
                        {
                            player2Pressed = true;
                            player2Timer = Timer.TimePassed;
                        }

                    } while (!player2Pressed || !player1Pressed);
                    Console.WriteLine("Player 1 time spent: {0} ms. {2}Player 2 time spent: {1} ms.", player1Timer, player2Timer, Environment.NewLine);
                    string winner = player1Timer < player2Timer ? "Payer 1": "Player 2";
                    Console.WriteLine("{0} won", winner);
                }


                BufferFlush();

            } while (!pressedStartKey);
            Wait();
        }

        /// <summary>
        /// Singleplayer game loop.
        /// </summary>
        private void SingleRun()
        {
            Console.Clear();
            player1ReactionKey = ConsoleKey.Enter;
            bool pressedStartKey = false;
            Console.WriteLine("Press {0} to start. {1}Pres {2} when you see {3}", gameStart, Environment.NewLine, player1ReactionKey, sign);
            do
            {
                
                while (!Console.KeyAvailable) ;
                if (Console.ReadKey(true).Key == gameStart) //delegate/event this
                {
                    pressedStartKey = true;
                    Timer.SetTime();
                    double playerTimer = 0;
                    float timeToCountDown = (rnd.Next(1, 8) + (rnd.Next(0, 1000) / 1000f))*1000;
                    countup = 0;
                    while (countup < timeToCountDown)
                    {
                        countup = Timer.TimePassed;
                        BufferFlush();
                    }
                    Console.WriteLine(sign);
                    Timer.SetTime();
                    while (!Console.KeyAvailable && Console.ReadKey(true).Key != player1ReactionKey) ;
                    playerTimer = Timer.TimePassed;
                    Console.WriteLine("Time spent: {0} ms.", playerTimer);
                    AddHighScore(playerTimer);
                }


                BufferFlush();

            } while (!pressedStartKey);
            Wait();

        }


        /// <summary>
        /// Adds <paramref name="time"/> to the highscore at the correct location. If the highscore is above ten the last value(s) is/are removed. 
        /// </summary>
        /// <param name="time"></param>
        private static void AddHighScore(double time)
        {
            if (highscores.Count == 0)
            {
                highscores.Add(time);
            }
            else
            {
                int highscoreCounter = highscores.Count;
                int pos = 0;
                while (pos <= highscoreCounter)
                {
                    if (pos < highscoreCounter && highscores[pos] > time)
                        highscores.Insert(pos, time);
                    else if (pos == highscores.Count && highscoreCounter < 10)
                        highscores.Add(time);
                    pos++;
                }

            }
            while (highscores.Count > 10)
                highscores.RemoveAt(highscores.Count - 1);

        }

    }
}
