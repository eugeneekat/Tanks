using System;
using System.Threading;

namespace Tanks
{
    //Абстрактный класс контроллер
    abstract class Controller
    {
        //Делегат для асинхронного метода
        protected delegate void AsyncActionDeleg();

        //Экземляр делегата для асинхронного вызова метода управления
        protected AsyncActionDeleg asyncActionDeleg = null;

        //Экземляр делегата для вызова метода зверщения асинхронных методов ввода(когда игрок нажал esc)
        protected AsyncCallback onExit = null;

        /*---События---*/
        //Движение
        public event Action<object, KeyboardControllerEventArgs> move = null;
        //Стрельба
        public event Action<object, KeyboardControllerEventArgs> shoot = null;
        //Выход
        public event Action<object, KeyboardControllerEventArgs> exit = null;

        //Аргументы нажатой клавишы(Передаются в события)
        protected KeyboardControllerEventArgs eventArgs = new KeyboardControllerEventArgs();

        //Абстрактный метод считывание нажатия кнопки(у игрока считываение нажатия, у компьютера рандом)
        protected abstract void Action();
        
        //Асинхронный метод считывания ввода
        public void AsyncAction()
        {
            if (this.asyncActionDeleg != null)
                this.asyncActionDeleg.BeginInvoke(this.onExit, null);
        }
        
        //Метод оповещения о движении
        protected void OnMove()
        {
            this.move?.Invoke(this, this.eventArgs);
        }
        
        //Метод оповещения о выстреле
        protected void OnShoot()
        {
            this.shoot?.Invoke(this, this.eventArgs);
        }


        //Метод завершения ввода
        protected void OnExit(IAsyncResult result)
        {
            //Оповещаем о завершении
            this.exit(this, this.eventArgs);
            //Удаляем всех подписчиков
            this.move = this.shoot = this.exit = null;           
        }
    }

    //Класс контроллера игрока
    class PlayerController : Controller
    {
        
        //Устанавливаем асинхронный делегат ввода определенным методом считывания
        public PlayerController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Определяем метод считывания кнопки
        protected override void Action()
        {
            ConsoleKeyInfo info = new ConsoleKeyInfo();
            //Пока игра не закончена или пока игрок не нажемт esc 
            while (!GameField.IsEndGame || info.Key != ConsoleKey.Escape)
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
            //Устанавливаем в аргументы события последнюю нажатую кнопку
            this.eventArgs.Key = info.Key;
        }
    }

    //Класс упраления компьютером
    class AIController : Controller
    {
        //Для генерации дейстаия
        Random rand = new Random();

        //Устанавливаем асинхронный делегат ввода определенным методом считывания
        public AIController()
        {
            this.asyncActionDeleg = this.Action;
        }

        //Определяем метод считывания кнопки
        protected override void Action()
        {
            //До тех пор пока игры не закончена
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
