using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class BabyPigman : Pigman
    {
        public BabyPigman(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
        }

        public override string GenerateSound()
        {
            return "5 * 2^-1 = 2.5";
        }
    }
}
