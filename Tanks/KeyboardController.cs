using System;
using System.Threading;

namespace Tanks
{
    //Абстрактный класс контроллер
    abstract class Controller
    {
        //Аргументы нажатой клавишы
        protected KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();
        //Абстрактный метод считывание нажатия кнопки
        protected abstract void Action();
        //Асинхронный метод считывания
        public abstract void AsyncAction();
        //Делегат для асинхронного метода
        protected delegate void AsyncActionDeleg();
    }

    //Класс контроллера игрока
    class PlayerController : Controller
    {
        //События движения и стрельбы
        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;
        //Делагат асинхронного метода считывания
        AsyncActionDeleg asyncActionDeleg = null;

        public PlayerController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Метод считывания кнопки
        protected override void Action()
        {
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                switch (info.Key)
                {
                    //При движении срабатывает move
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        this.eventArgs.Key = info.Key;
                        if (this.move != null)
                            this.move(this, this.eventArgs);
                        break;
                    //При выстреле shoot
                    case ConsoleKey.Spacebar:
                        this.eventArgs.Key = info.Key;
                        if (this.shoot != null)
                            this.shoot(this, this.eventArgs);
                        break;
                }
            }
        }

        //Асинхронный метод считывание нажатия кнопки
        public override void AsyncAction()
        {
            this.asyncActionDeleg.BeginInvoke(null,null);
        }

    }

    //Класс упраления компьютером
    class AIController : Controller
    {
        //События
        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;

        AsyncActionDeleg asyncActionDeleg = null;
        Random rand = new Random();

        public AIController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Случайное действие компьютера
        protected override void Action()
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
            
        public override void AsyncAction()
        {
            this.asyncActionDeleg.BeginInvoke(null, null);
        }

        
    }


}
