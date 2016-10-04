using System;

namespace Tanks
{
    class KeyboardControllerEventArgs : EventArgs
    {
        public ConsoleKeyInfo info { get; set; }
    }
}
