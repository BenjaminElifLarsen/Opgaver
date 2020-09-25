using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Publisher class that all objects, that needs to react to either a keypress or interact with a ware, should be subscribed too.
    /// </summary>
    public class Publisher
    {
        private static WarePublisher warePublisher = new WarePublisher();
        private static KeyPublisher keyPublisher = new KeyPublisher();

        /// <summary>
        /// The default constructor.
        /// </summary>
        public Publisher()
        {
            //if (warePublisher == null)
            //warePublisher = new WarePublisher();
        }

        /// <summary>
        /// Gets the ware publisher class instant. 
        /// </summary>
        public static WarePublisher PubWare { get => warePublisher; }

        /// <summary>
        /// Gets the key publisher class instant 
        /// </summary>
        public static KeyPublisher PubKey { get => keyPublisher; }
    }
}
