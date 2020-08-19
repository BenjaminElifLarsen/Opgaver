using System;
using System.Collections.Generic;

namespace Reaktions_spil
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Menu();
        }
    }

    /// <summary>
    /// Class containing variables, properties and functions needed for time keeping for the game.
    /// </summary>
    public class Timer
    {
        private DateTime startTime;

        /// <summary>
        /// Sets the time to the current system time.
        /// </summary>
        public void SetTime()
        {
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Calculates and returns the amount of miliseconds that have passed since <c>SetTime()</c> was called. If <c>SetTime()</c> was not called before it will throw a null reference exception.
        /// </summary>
        public double TimePassed { get
            {
                if (startTime.Year == 1)
                    throw new TimeNotSetException("SetTime has not been called");
                return (DateTime.Now - startTime).TotalMilliseconds;
            }
        }
    }

    /// <summary>
    /// Reaction game.
    /// </summary>
    public class Game
    {
        private List<double> highscores = new List<double>();
        private const ConsoleKey gameStart = ConsoleKey.W;
        private ConsoleKey player1ReactionKey;
        private ConsoleKey player2ReactionKey;
        private Random rnd = new Random();
        private const char sign = 'O';

        /// <summary>
        /// The game main menu. 
        /// </summary>
        public void Menu()
        {
            const ConsoleKey singleplayerKey = ConsoleKey.D1;
            const ConsoleKey multiplayerKey = ConsoleKey.D2;
            const ConsoleKey exitKey = ConsoleKey.D4;
            const ConsoleKey highscoreKey = ConsoleKey.D3;

            while (true) {
                Display();
                ConsoleKey pressedKey = MenuKeyPressed();
                switch (pressedKey)
                {
                    case singleplayerKey:
                        SinglePlayerRun();
                        break;

                    case multiplayerKey:
                        MultiPlayerRun();
                        break;

                    case exitKey:
                        Environment.Exit(0);
                        break;

                    case highscoreKey:
                        HighscoreDisplay();
                        break;
                }
            }

            void Display()
            {
                char singleplayerControlChar = singleplayerKey.ToString()[singleplayerKey.ToString().Length - 1];
                char multiplayerControlChar = multiplayerKey.ToString()[multiplayerKey.ToString().Length - 1];
                char exitControlChar = highscoreKey.ToString()[highscoreKey.ToString().Length - 1];
                char highscoreControlChar = exitKey.ToString()[exitKey.ToString().Length - 1];

                Console.Clear();
                PrintOutMessage(String.Format("Press {0} for singleplayer. Press {1} for multiplayer. Press {2} for highscore. Press {3} to shutdown.",
                    singleplayerControlChar, multiplayerControlChar, exitControlChar, highscoreControlChar));
            }

            ConsoleKey MenuKeyPressed()
            {
                do
                {
                    while (!Console.KeyAvailable) ;
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == singleplayerKey || key == multiplayerKey || key == exitKey || key == highscoreKey)
                        return key;
                    BufferFlush();
                } while (true);
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
                    PrintOutMessage(score.ToString());
            else
                PrintOutMessage("No highscore yet.");
            Wait();
        }

        /// <summary>
        /// Waits on a key press. 
        /// </summary>
        private void Wait()
        {
            PrintOutMessage("Press any key to continue.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Flushes the buffer of Console.ReadKey
        /// </summary>
        private void BufferFlush()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true); 
        }

        /// <summary>
        /// Runs the multiplayer.
        /// </summary>
        private void MultiPlayerRun()
        {
            Console.Clear();
            SetMultiPlayerKeys();
            Console.WriteLine("Press {0} to start. {1}Player 1: Pres {2} when you see {4}. {1}Player 2: Pres {3} when you see {4}.", 
                gameStart, Environment.NewLine, player1ReactionKey, player2ReactionKey, sign);
            MultipleGameLoop();
            UnsetMultiPlayerKeys();
            Wait();
        }

        /// <summary>
        /// Sets the player keys for multiplayer. 
        /// </summary>
        private void SetMultiPlayerKeys()
        {
            player1ReactionKey = ConsoleKey.Spacebar;
            player2ReactionKey = ConsoleKey.Enter;
        }

        /// <summary>
        /// Unsets the player keys for multiplayer
        /// </summary>
        private void UnsetMultiPlayerKeys()
        {
            player1ReactionKey = 0;
            player2ReactionKey = 0;
        }

        /// <summary>
        /// Contains the game loop for the multiplayer part.
        /// </summary>
        private void MultipleGameLoop()
        {

            bool pressedStartKey = false;
            do
            {
                while (!Console.KeyAvailable) ;
                if (Console.ReadKey(true).Key == gameStart)
                {
                    bool player1Pressed = false;
                    bool player2Pressed = false;
                    double player1Timer = 0;
                    double player2Timer = 0;
                    pressedStartKey = true;

                    PrintOutMessage("Game Started");
                    CountUp();
                    PrintOutMessage(sign.ToString(), ConsoleColor.Green);

                    Timer timer = new Timer();
                    timer.SetTime();
                    do
                    {
                        ConsoleKey pressedKey = KeyGet();
                        if (pressedKey == player1ReactionKey && !player1Pressed)
                        {
                            player1Pressed = true;
                            player1Timer = timer.TimePassed;
                        }
                        else if (pressedKey == player2ReactionKey && !player2Pressed)
                        {
                            player2Pressed = true;
                            player2Timer = timer.TimePassed;
                        }
                    } while (!player2Pressed || !player1Pressed);
                    PrintOutMessage(String.Format("Player 1 time spent: {0} ms. {2}Player 2 time spent: {1} ms.", player1Timer, player2Timer, Environment.NewLine));
                    PrintOutMessage(String.Format("{0} won", player1Timer < player2Timer ? "Payer 1" : "Player 2"), ConsoleColor.Red);
                    AddHighScore(player1Timer);
                    AddHighScore(player2Timer);
                }
                BufferFlush();
            } while (!pressedStartKey);
        }

        /// <summary>
        /// Prints out <paramref name="message"/> in the <paramref name="colour"/>.
        /// </summary>
        /// <param name="message">Message to print out</param>
        /// <param name="colour">The colour to print the <paramref name="message"/> in.</param>
        private void PrintOutMessage(string message, ConsoleColor colour = ConsoleColor.White)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Waits on a key to be pressed and then returns the key without displaying it.
        /// </summary>
        /// <returns>Returns the pressed key without displaying it.</returns>
        private ConsoleKey KeyGet()
        {
            while (!Console.KeyAvailable) ;
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Counts up to a random time period from 1000 and up to 7999 miliseconds.  
        /// </summary>
        private void CountUp()
        {
            Timer timer = new Timer();
            float timeToCountDown = (rnd.Next(1, 7)*1000 + (rnd.Next(0, 1999)));
            double countup = 0;
            timer.SetTime();
            while (countup < timeToCountDown)
            {
                countup = timer.TimePassed;
                BufferFlush();
            }
        }

        /// <summary>
        /// Runs the singleplayer.
        /// </summary>
        private void SinglePlayerRun()
        {
            Console.Clear();
            SetSinglePlayerKey();
            PrintOutMessage(String.Format("Press {0} to start. {1}Pres {2} when you see {3}", gameStart, Environment.NewLine, player1ReactionKey, sign));
            SinglePlayerGameLoop();
            UnsetSinglePlayerKey();
            Wait();
        }

        /// <summary>
        /// Sets the single player key.
        /// </summary>
        private void SetSinglePlayerKey()
        {
            player1ReactionKey = ConsoleKey.Enter;
        }

        /// <summary>
        /// Unsets the single player key.
        /// </summary>
        private void UnsetSinglePlayerKey()
        {
            player1ReactionKey = 0;
        }

        /// <summary>
        /// Contains the gameloop for the singleplayer part. 
        /// </summary>
        private void SinglePlayerGameLoop()
        {
            bool pressedStartKey = false;
            do
            {
                while (!Console.KeyAvailable) ;
                if (Console.ReadKey(true).Key == gameStart)
                {
                    pressedStartKey = true;
                    PrintOutMessage("Game Started");
                    CountUp();
                    PrintOutMessage(sign.ToString(), ConsoleColor.Green);
                    Timer timer = new Timer();
                    timer.SetTime();
                    while (!Console.KeyAvailable && Console.ReadKey(true).Key != player1ReactionKey) ;
                    double playerTimer = timer.TimePassed;
                    PrintOutMessage(String.Format("Time spent: {0} ms.", playerTimer), ConsoleColor.Red);
                    AddHighScore(playerTimer);
                }
                BufferFlush();
            } while (!pressedStartKey);
        }

        /// <summary>
        /// Adds <paramref name="time"/> to the highscore at the correct location. If the highscore is above ten the last value(s) is/are removed. 
        /// </summary>
        /// <param name="time">The time to add to the highscore</param>
        private void AddHighScore(double time)
        {
            if (highscores.Count == 0)
                highscores.Add(time);
            else
            {
                int highscoreCounter = highscores.Count;
                int pos = 0;
                while (pos <= highscoreCounter)
                {
                    if (pos < highscoreCounter && highscores[pos] > time)
                    {
                        highscores.Insert(pos, time);
                        break;
                    }
                    else if (pos == highscoreCounter && highscoreCounter < 10)
                    {
                        highscores.Add(time);
                        break;
                    }
                    pos++;
                }

            }
            while (highscores.Count > 10)
                highscores.RemoveAt(highscores.Count - 1);

        }

    }

    /// <summary>
    /// Custom exception used for the class <c>Timer</c>.
    /// </summary>
    class TimeNotSetException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeNotSetException()
        {

        }

        /// <summary>
        /// Constructor that takes <paramref name="message"/>,
        /// </summary>
        /// <param name="message">Message of the error,</param>
        public TimeNotSetException(string message) : base(message)
        {

        }


    }

}
