using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class KeyPublisher
    {
        public delegate void keyPressEventHandler(object sender, ControlEvents.KeyEventArgs args);
        public event keyPressEventHandler RaiseKeyPressEvent;

        public void PressKey(ConsoleKey key) //a threaded function should call this when a key is pressed.
        {
            OnPressingKey(new ControlEvents.KeyEventArgs(key));
        }

        protected void OnPressingKey(ControlEvents.KeyEventArgs e)
        {
            keyPressEventHandler eventHandler = RaiseKeyPressEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

    }
}
