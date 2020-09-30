using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    abstract class Mob
    {

        protected double[] location = new double[3];

        public Mob(string id, int health, string sound, double[] startLocation)
        {
            location = startLocation;
            ID = id;
            Health = health;
            Sound = sound;
        }

        public abstract string ID { get; set; }

        public abstract int Health { get; set; }

        public abstract string Sound { get; set; } 

        public abstract string GenerateSound();

        public virtual void Move() 
        {
            location[0] += 2;
            location[2] *= 0.1;
        }

    }
}
