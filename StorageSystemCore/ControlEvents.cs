using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Event class with events related to the control of the program.
    /// </summary>
    public class ControlEvents : EventArgs
    {
        /// <summary>
        /// Class that holds event data of the ware creation system.
        /// </summary>
        public class CreateWareEventArgs
        {
            public CreateWareEventArgs()
            {
            }
        }

        /// <summary>
        /// Class that holds event data of the ware amount adding system.
        /// </summary>
        public class AddEventArgs
        {
            /// <summary>
            /// Base constructor for the add event data.
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="amountToAdd"></param>
            public AddEventArgs(string ID, int amountToAdd)
            {
                this.ID = ID;
                this.AmountToAdd = amountToAdd;
            }
            /// <summary>
            /// Gets and sets the ID of the ware.
            /// </summary>
            public string ID { get; set; }

            /// <summary>
            /// Gets and sets the amount to add.
            /// </summary>
            public int AmountToAdd { get; set; }
        }

        /// <summary>
        /// Class that holds event data of the ware amount removal system.
        /// </summary>
        public class RemoveEventArgs
        {
            /// <summary>
            /// Base constructor for the remove event data.
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="amountToRemove"></param>
            public RemoveEventArgs(string ID, int amountToRemove)
            {
                this.ID = ID;
                this.AmountToRemove = amountToRemove;
            }
            /// <summary>
            /// Get and sets the ID of the ware.
            /// </summary>
            public string ID { get; set; }

            /// <summary>
            /// Gets and sets the amount to remove. 
            /// </summary>
            public int AmountToRemove { get; set; }
        }

        /// <summary>
        /// Class that holds event data of the input control system.
        /// </summary>
        public class KeyEventArgs
        {
            /// <summary>
            /// Base constructor for the consoleKey event data.
            /// </summary>
            /// <param name="key">The ConsoleKeyInfo to be transmitted.</param>
            public KeyEventArgs(ConsoleKey key)
            {
                Key = key;
            }
            /// <summary>
            /// Gets and sets the consoleKeyInfo key. 
            /// </summary>
            public ConsoleKey Key { get; set; }
        }


    }
}
