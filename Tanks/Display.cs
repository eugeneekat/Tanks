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
        
        string tank =
            "        \n" +
            "------  \n" +
            "|   ----\n" +
            "|   ----\n" +
            "------  \n" +
            "        \n";

        public void OnMoveUpdate(object sender, GameObjectStateEventArgs args)
        {
            Console.SetCursorPosition(args.OldStatePosX, args.OldStatePosY);
            Console.SetCursorPosition(args.NewStatePosX, args.NewStatePosY);
            Console.Write(this.tank);
        }

        public void OnShootUpdate(object sender, GameObjectStateEventArgs args)
        {
            Action<int, int> action = (x, y) =>
            {
                int posX = args.NewStatePosX + 10;
                int posY = args.NewStatePosY + 2;
                Console.SetCursorPosition(posX, posY);
                while (posX < 25)
                {
                    Console.Write("*");
                    Console.SetCursorPosition(posX, posY);
                    Thread.Sleep(80);
                    Console.SetCursorPosition(posX, posY);
                    Console.Write(" ");
                    posX++;
                }
            };
            action.BeginInvoke(args.NewStatePosX, args.NewStatePosY, null, null);
        }
    }
}
