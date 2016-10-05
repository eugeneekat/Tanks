using System;

namespace Tanks
{
    class KeyboardControllerEventArgs : EventArgs
    {
        public ConsoleKeyInfo Info { get; set; }
    }
}
