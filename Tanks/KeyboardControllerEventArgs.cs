using System;

namespace Tanks
{
    class KeyboardControllerEventArgs : EventArgs
    {
        public ConsoleKey Key { get; set; }
    }
}
