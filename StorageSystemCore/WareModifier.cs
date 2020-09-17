using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    /// <summary>
    /// Contains functions to modify, add wares and remove wares
    /// </summary>
    public class WareModifier //consider using delegates/events to access this one.
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="amount"></param>
        public static void AddToWare(string ID, int amount)
        {
            Publisher.PubWare.AddToWare(ID, amount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="amount"></param>
        public static void RemoveFromWare(string ID, int amount)
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
            if(Support.Confirmation())
                return WareInformation.RemoveWare(ID);
            return false;
        }

    }
}
