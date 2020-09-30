using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class Sheep : Mob
    {

        public Sheep(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
            location = startLocation;
            ID = id;
            Health = health;
            Sound = sound;
        }
        public override string ID { get;set; }
        public override int Health { get; set; }
        public override string Sound { get; set; }

        public override string GenerateSound()
        {
            return Sound;
        }
        public virtual void Eat()
        {
            Console.WriteLine(Sound + " mmm... grass... ");
        }
    }
}
