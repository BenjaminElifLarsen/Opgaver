using System;
using System.Collections.Generic;
using System.Text;

namespace PraticeProgram
{
    interface IAttack
    {
        public bool CanAttact { get; set; }
        void Attacking(string mobID);
    }
}
