using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tanks
{
    
    class Display
    {
        //Делегаты для асинхронного вызова
        Action<int, int> shoot = null;
        Action<int, int, string[]> move = null;

        //Определяем делегатам методы
        public Display()
        {
            this.shoot = this.Shoot;
            this.move = this.Move;
        }

        //Асинхронный вызов перемещения танка
        public void OnMoveUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.move.Invoke(args.NewStatePosX, args.NewStatePosY, args.Sprite);
        }

        //Асинхронный вызов отрисовки полета снаряда
        public void OnShootUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.shoot.BeginInvoke(args.NewStatePosX, args.NewStatePosY, null, null);
        }

        //Метод отрисовки полёта снаряда
        protected void Shoot(int x, int y)
        {
            bool left = x == 0 ? true : false;
            int posX = (left) ? x + 10 : x - 1;
            int posY = y + 2;
            while (posX < GameField.MaxWidth && posX > 0)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write("*");
                Thread.Sleep(80);
                Console.SetCursorPosition(posX, posY);
                Console.Write(" ");
                if (left)
                    posX++;
                else
                    posX--;
            }
        }

        //Метод отрисовки перемещения танка
        protected void Move(int x, int y, string [] sprite)
        {
            for(int i = 0; i < sprite.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(sprite[i]);
            }          
        }
    }
}
