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
        Action<int, int, string> move = null;

        //Определяем делегатам методы
        public Display()
        {
            this.shoot = this.Shoot;
            this.move = this.Move;
        }

        //Асинхронный вызов перемещения танка
        public void OnMoveUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.move.BeginInvoke(args.NewStatePosX, args.NewStatePosY, args.Sprite, null, null);
        }

        //Асинхронный вызов отрисовки полета снаряда
        public void OnShootUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.shoot.BeginInvoke(args.NewStatePosX, args.NewStatePosY, null, null);
        }

        //Метод отрисовки полёта снаряда
        protected void Shoot(int x, int y)
        {
            int posX = x + 10;
            int posY = y + 2;
            Console.SetCursorPosition(posX, posY);
            while (posX < GameField.MaxWidth)
            {
                Console.Write("*");
                Console.SetCursorPosition(posX, posY);
                Thread.Sleep(80);
                Console.SetCursorPosition(posX, posY);
                Console.Write(" ");
                posX++;
            }
        }

        //Метод отрисовки перемещения танка
        protected void Move(int x, int y, string sprite)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sprite);
        }
    }
}
