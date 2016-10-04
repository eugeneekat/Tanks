using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    abstract class GameObject
    {
        //Позиция объекта
        protected int PositionX { get; set; }
        protected int PositionY { get; set; }

        //Границы поля
        protected static int MaxWidth { get; set; }
        protected static int MaxHeight { get; set; }

        //Инициализация границ поля
        public static void SetField(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                MaxWidth = width;
                MaxHeight = height;
            }

        }    

        //Аргументы события объекта(Старое положение и новое положение)
        protected GameObjectStateEventArgs args = new GameObjectStateEventArgs();
    }

    class Tank : GameObject
    {
        //Событие выстрела
        public event Action<object, GameObjectStateEventArgs> shoot = null;
        //Событие движения
        public event Action<object, GameObjectStateEventArgs> move = null;

        //Конструктор устанавливает позиции объекта и агрументы для события
        public Tank(int x, int y)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.args.OldStatePosX = this.args.NewStatePosX = this.PositionX;
            this.args.OldStatePosY = this.args.NewStatePosY = this.PositionY;
        }

        //Действия на кнопки передвижение
        void OnMove(object sender, KeyboardControllerEventArgs args)
        {
            switch(args.info.Key)
            {
                case ConsoleKey.UpArrow:
                    if (this.PositionY > 0)
                       this.PositionY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (this.PositionY < GameObject.MaxHeight)
                        this.PositionY++;
                    break;
            }
            this.args.NewStatePosX = this.PositionX;
            this.args.NewStatePosY = this.PositionY;
            if (this.args.NewStatePosX != this.args.OldStatePosX || this.args.NewStatePosY != this.args.OldStatePosY)
            {
                if (this.move != null)
                    this.move(this, this.args);
                this.args.OldStatePosX = this.args.NewStatePosX;
                this.args.OldStatePosY = this.args.NewStatePosY;
            }    
        }
        
        //Действия на выстрел
        void OnShoot(object sender, KeyboardControllerEventArgs args)
        {
            this.shoot(this, this.args);
        }
    }

}
