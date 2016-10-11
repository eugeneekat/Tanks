using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    static class GameField
    {
        //Границы поля
        public static int MaxWidth { get; set; }
        public static int MaxHeight { get; set; }

        //Инициализация границ поля
        public static void SetField(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                MaxWidth = width;
                MaxHeight = height;
            }
        }

        //Флаг компьютеру что игра окончена
        public static bool IsEndGame { get; set; } = false;        
    }

    abstract class GameObject
    {
        //Изображение объекта
        protected string [] sprite = null;

        //Отслеживание изменение состояния объекта (агрументы передаются отрисовщику)
        protected GameObjectStateEventArgs args = new GameObjectStateEventArgs();

        //Позиция объекта
        protected int PositionX { get; set; }
        protected int PositionY { get; set; }
    }

    class Tank : GameObject
    {

        //Событие выстрела
        public event Action<object, GameObjectStateEventArgs> shoot = null;
        //Событие движения
        public event Action<object, GameObjectStateEventArgs> move = null;
        //Событие завершения игры
        public event Action<object, GameObjectStateEventArgs> exit = null;

        //Конструктор устанавливает позиции объекта и агрументы для события
        public Tank(int x, int y, string [] sprite)
        {
            this.PositionX  = this.args.NewStatePosX    = x;
            this.PositionY  = this.args.NewStatePosY    = y;
            this.sprite     = this.args.Sprite          = sprite;
        }

        //Обработка события передвижения у контроллера
        public void OnMove(object sender, KeyboardControllerEventArgs args)
        {
            switch (args.Key)
            {
                //Проверка границ и передвижение координат
                case ConsoleKey.UpArrow:
                    if (this.PositionY > 0)
                        this.args.NewStatePosY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (this.PositionY < GameField.MaxHeight)
                        this.args.NewStatePosY++;
                    break;
            }
            //Если границы допускают передвижение то меняем положение оригинала и сообщаем отрисовщику
            if (this.args.NewStatePosY != this.PositionY)
            {
                this.PositionX = this.args.NewStatePosX;
                this.PositionY = this.args.NewStatePosY;
                if (this.move != null)
                    this.move(this, this.args);             
            }
        }

        //Действия на выстрел
        public void OnShoot(object sender, KeyboardControllerEventArgs args)
        {
            this.shoot(this, this.args);
        }

        //Действия на выход
        public void OnExit (object sender, KeyboardControllerEventArgs args)
        {
            //Если был нажат esc то игрок вышел
            if(args.Key == ConsoleKey.Escape)
                //Устаналиваем аргумент жизни в false
                this.args.IsAlive = false;
            //Оповещаем подписчиков на событии выход игрока
            this.exit?.Invoke(this, this.args);
            //Устанавливаем флаг завершения игры
            GameField.IsEndGame = true;
            //Удаляем всех подписчиков
            this.exit = this.move = this.shoot = null;
        }
    }

}
