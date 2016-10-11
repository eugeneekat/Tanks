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
        //Примитив синхронизации отрисовки
        protected object sync = new object();

        //Делегаты для асинхронного вызова
        Action<object, GameObjectStateEventArgs> shoot = null;

        Dictionary<object, GameObjectStateEventArgs> dic = new Dictionary<object, GameObjectStateEventArgs>();

        //Определяем делегатам методы
        public Display()
        {
            this.shoot = this.Shoot;
        }

        //Перемещения танка - синхронная отрисовка
        public void OnMoveUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.dic[sender] = args;
            //Синхронизация отрисовки
            lock (this.sync)
            {
                for (int i = 0; i < args.Sprite.Length; i++)
                {
                    Console.SetCursorPosition(args.NewStatePosX, args.NewStatePosY + i);
                    Console.Write(args.Sprite[i]);
                }
            }
        }

        //Асинхронный вызов отрисовки полета снаряда
        public void OnShootUpdate(object sender, GameObjectStateEventArgs args)
        {
            this.shoot.BeginInvoke(sender, args, null, null);
        }

        //Метод отрисовки полёта снаряда
        protected void Shoot(object sender, GameObjectStateEventArgs args)
        {
            bool left = args.NewStatePosX == 0 ? true : false;
            int posX = (left) ? args.NewStatePosX + 10 : args.NewStatePosX - 1;
            int posY = args.NewStatePosY + 2;
            while (posX < GameField.MaxWidth && posX > 0 && !GameField.IsEndGame)
            {
                //Примитивы синхронизации отрисовки что бы не смешивалось
                lock (this.sync)
                {
                    Console.SetCursorPosition(posX, posY);
                    Console.Write("*");
                }
                Thread.Sleep(80);
                lock (this.sync)
                {
                    Console.SetCursorPosition(posX, posY);
                    Console.Write(" ");
                }
                if (left)
                    posX++;
                else
                    posX--;
            }
            //Определяет попадание
            foreach (var i in this.dic)
            {
                if (i.Key != sender)
                {
                    if (i.Value.NewStatePosX == posX && i.Value.NewStatePosY == posY)
                    {
                        i.Value.IsAlive = false;
                        GameField.IsEndGame = true;
                    }
                    break;
                }
            }
           
        }

        public void Exit(object sender, GameObjectStateEventArgs args)
        {
            if (!args.IsAlive)
            {
                Console.Clear();
                Console.WriteLine("LLooser:\n");
                foreach (string s in args.Sprite)
                {
                    Console.Write(s);
                }
            }
        }
    }
}
