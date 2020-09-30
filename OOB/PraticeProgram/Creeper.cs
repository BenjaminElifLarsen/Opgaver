using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class Creeper : Mob, IAttack, IAggro
    {
        public Creeper(string id, int health, string sound, double[] startLocation) : base(id, health, sound, startLocation)
        {
        }

        public override string ID { get; set; }
        public override int Health { get; set; }
        public override string Sound { get; set; }
        public bool CanAttact { get; set; }

        public void Attacking(string mobID)
        {
            Console.WriteLine($"Hey there {mobID}");
        }

        public override string GenerateSound()
        {
            return "What a nice house you got there...";
        }

        private void HitRadius()
        {
            Console.WriteLine(ID + " hit everything");
        }
    }
}
