using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    class Player : Mob, IAttack
    {
        public Player(string id, int health, string sound, double[] startLocation) : base(id,health,sound,startLocation)
        {
            
        }

        public override int Health { get; set; }

        public override string Sound { get; set; }
        public bool CanAttact { get;set; }
        public override string ID { get; set; }

        public void Attacking(string mobID)
        {
            Console.WriteLine($"I HIT {mobID}, ME NO DAMAGE DO!");
        }

        public override string GenerateSound()
        {
            return Sound; //"I AM HUMAN, I AM STRONG!!!"
        }

        public void DrinkPotion()
        {
            Console.WriteLine("I DRANK POTION, ME SMART!");
        }
        

        public override void Move()
        {
            location[0] = 2;
            location[1] /= 5;
            location[2] += 12.1;
        }
    }
}
