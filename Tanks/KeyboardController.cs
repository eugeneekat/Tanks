using System;
using System.Threading;

namespace Tanks
{

    class KeyboardController
    {
        protected KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();

        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;

        public void Action(ConsoleKeyInfo info)
        {
            switch(info.Key)
            {
                //При движении срабатывает move
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    this.eventArgs.Key = info.Key;
                    if(this.move != null)
                        this.move(this, this.eventArgs);
                    break;
                //При выстреле shoot
                case ConsoleKey.Spacebar:
                    this.eventArgs.Key = info.Key;
                    if(this.shoot != null)
                        this.shoot(this, this.eventArgs);
                    break;
            }
        }
    }

    class AIController
    {
        delegate void ActionDeleg();
        ActionDeleg a = null;
        Random rand = new Random();

        protected KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();

        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;

        public AIController()
        {
            this.a = this.Action;
        }

        public void AsyncAction()
        {
            this.a.BeginInvoke(null, null);
        }

        //Случайное действие компьютера
        public void Action()
        {
            while (true)
            {
                int i = rand.Next(1, 8);
                switch (i)
                {
                    case 1:
                    case 2:
                    case 3:
                        this.eventArgs.Key = ConsoleKey.UpArrow;
                        this.move(this, this.eventArgs);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        this.eventArgs.Key = ConsoleKey.DownArrow;
                        this.move(this, this.eventArgs);
                        break;
                    case 7:
                        this.eventArgs.Key = ConsoleKey.Spacebar;
                        this.shoot(this, this.eventArgs);
                        break;
                }
                Thread.Sleep(500);
            }
        }
    }


}
