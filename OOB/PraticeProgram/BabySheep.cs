using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class BabySheep : Sheep, IAttack
    {
        public BabySheep(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
            location = startLocation;
            ID = id;
            Health = health;
            Sound = sound;
        }

        public bool CanAttact { get;set; }

        public void Attacking(string mobID)
        {
            Console.WriteLine("Murder! " + Sound);
        }

        public override void Eat()
        {
            Console.WriteLine(Sound + " mmm... grass... " + Sound);
        }

        public void RunAway()
        {
            location[0] += 100;
            location[1] += 100;
            location[2] *= 20.5;
        }


    }
}
