using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class Pigman : Mob, IAttack, IAggro
    {
        public override string ID { get;set; }
        public override int Health { get; set; }
        public override string Sound { get; set; }
        public bool CanAttact { get; set; }

        public Pigman(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
        }

        public void Attacking(string mobID)
        {
            Console.WriteLine("F*ck you, " + mobID);
        }

        public override string GenerateSound()
        {
            return "Pigs > humans";
        }

        public void GoPassive()
        {

        }
    }
}
