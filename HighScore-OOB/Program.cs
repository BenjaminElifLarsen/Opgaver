using System;
using System.Collections.Generic;
using System.IO;

namespace HighScore_OOB
{
    class Program
    {
        //static FileReadWrite fileRW;
        //static HighScores highScore;
        //static UI ui;

        static void Main(string[] args)
        {
            //fileRW = new FileReadWrite();
            //highScore = new HighScores();
            //ui = new UI()
            Menu menu = new Menu();
            menu.Run();
        }


        

    }

}
