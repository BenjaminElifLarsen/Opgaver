﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareModifier
    {

        public static void AddToWare(string ID, uint amount)
        {
            Publisher.PubWare.AddToWare(ID, amount);
        }

        public static void RemoveFromWare(string ID, uint amount)
        {
            Publisher.PubWare.RemoveFromWare(ID, amount);
        }

        /// <summary>
        /// ... Returns true if the item was found and removed else false
        /// </summary>
        /// <param name="ID">The ID of the ware to remove</param>
        /// <returns>Returns true if the item was found and removed else false</returns>
        public static bool RemoveWare(string ID)
        {
            //Have a confirmation
            return WareInformation.RemoveWare(ID);
        }

    }
}
