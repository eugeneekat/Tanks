using System;
using System.Threading;

namespace Tanks
{
    //Абстрактный класс контроллер
    abstract class Controller
    {
        //Экземляр делегата для асинхронного вызова
        protected AsyncActionDeleg asyncActionDeleg = null;
        //События движения и стрельбы
        public event Action<object, KeyboardControllerEventArgs> move = null;
        public event Action<object, KeyboardControllerEventArgs> shoot = null;
        public event Action<object, KeyboardControllerEventArgs> exit = null;
        //Аргументы нажатой клавишы
        protected KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();
        protected AsyncCallback onExit = null;
        //Абстрактный метод считывание нажатия кнопки
        protected abstract void Action();
        //Асинхронный метод считывания
        public void AsyncAction()
        {
            if (this.asyncActionDeleg != null)
                this.asyncActionDeleg.BeginInvoke(this.onExit, null);
        }
        
        //Делегат для асинхронного метода
        protected delegate void AsyncActionDeleg();

        protected void OnMove()
        {
            this.move?.Invoke(this, this.eventArgs);
        }

        protected void OnShoot()
        {
            this.shoot?.Invoke(this, this.eventArgs);
        }

        protected void OnExit(IAsyncResult result)
        {
            if (result.IsCompleted)
                this.exit(this, this.eventArgs);
        }
    }

    //Класс контроллера игрока
    class PlayerController : Controller
    {
        

        public PlayerController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Метод считывания кнопки
        protected override void Action()
        {
            ConsoleKeyInfo info = new ConsoleKeyInfo();
            while (info.Key != ConsoleKey.Escape)
            {
                info = Console.ReadKey();
                switch (info.Key)
                {
                    //При движении срабатывает move
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        this.eventArgs.Key = info.Key;
                        this.OnMove();
                        break;
                    //При выстреле shoot
                    case ConsoleKey.Spacebar:
                        this.eventArgs.Key = info.Key;
                        this.OnShoot();
                        break;
                }
            }
            this.eventArgs.Key = info.Key;
        }
    }

    //Класс упраления компьютером
    class AIController : Controller
    {
        Random rand = new Random();

        public AIController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Случайное действие компьютера
        protected override void Action()
        {
            while (!GameField.IsEndGame)
            {
                int i = rand.Next(1, 8);
                switch (i)
                {
                    case 1:
                    case 2:
                    case 3:
                        this.eventArgs.Key = ConsoleKey.UpArrow;
                        this.OnMove();
                        break;
                    case 4:
                    case 5:
                    case 6:
                        this.eventArgs.Key = ConsoleKey.DownArrow;
                        this.OnMove();
                        break;
                    case 7:
                        this.eventArgs.Key = ConsoleKey.Spacebar;
                        this.OnShoot();
                        break;
                }
                Thread.Sleep(500);
            }
        }      
    }


}
