using System;

namespace Tanks
{
    class KeyboardController
    {
        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;

        KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();

        public void Action(ConsoleKeyInfo info)
        {
            switch(info.Key)
            {
                //При движении срабатывает move
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    this.eventArgs.info = info;
                    if(this.move != null)
                        this.move(this, this.eventArgs);
                    break;
                //При выстреле shoot
                case ConsoleKey.Spacebar:
                    this.eventArgs.info = info;
                    if(this.shoot != null)
                        this.shoot(this, this.eventArgs);
                    break;
            }
        }
    }
}
