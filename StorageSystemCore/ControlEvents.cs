﻿using System;
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

        public class RemoveWareCreatorEventArgs
        {
            public RemoveWareCreatorEventArgs()
            {
            }
            //public WarePublisher WarePublish { get; }
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
                AmountToAdd = amountToAdd;
            }
            /// <summary>
            /// Gets and sets the ID of the ware.
            /// </summary>
            public string ID { get; }

            /// <summary>
            /// Gets and sets the amount to add.
            /// </summary>
            public int AmountToAdd { get; }
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
                AmountToRemove = amountToRemove;
            }
            /// <summary>
            /// Get and sets the ID of the ware.
            /// </summary>
            public string ID { get; }

            /// <summary>
            /// Gets and sets the amount to remove. 
            /// </summary>
            public int AmountToRemove { get; set; }
        }

        public class GetTypeEventArgs
        {
            private Dictionary<string,Type> _types = new Dictionary<string, Type>();
            public GetTypeEventArgs(string ID)
            {
                this.ID = ID;
            }
            public string ID { get; }

            public Type GetType(string ID)
            {
                return _types[ID];
            }

            public void Add(string ID, Type type)
            {
                _types.Add(ID, type);
            }

        }

        public class AlterValueEventArgs
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="newValue"></param>
            /// <param name="property">The WareSeacheableAttribute Name</param>
            public AlterValueEventArgs(string ID, object newValue, string property)
            {
                this.ID = ID;
                SingleValue = newValue;
                PropertyName = property;
            }

            public AlterValueEventArgs(string ID, object[] newValues, string property)
            {
                this.ID = ID;
                MultieValueArray = newValues;
                PropertyName = property;
            }

            public string ID { get; }
            public object SingleValue { get; }
            public object[] MultieValueArray { get; }
            public string PropertyName { get; }
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
            public ConsoleKey Key { get; }
        }


    }
}
