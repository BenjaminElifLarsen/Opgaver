using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class Squid : Mob
    {
        public Squid(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
        }

        public override string ID { get; set; }
        public override int Health { get; set; }
        public override string Sound { get; set; }

        public override string GenerateSound()
        {
            return "FEAR ME MORTAL!!!";
        }

        public int GenerateTerror()
        {
            return int.MaxValue;
        }

        public void Swim()
        {

        }

    }
}
