using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class Publisher
    {
        private static WarePublisher warePublisher;

        public Publisher()
        {
            if (warePublisher == null) ;
            warePublisher = new WarePublisher();
        }

        public static WarePublisher PubWare { get => warePublisher; }
    }
}
