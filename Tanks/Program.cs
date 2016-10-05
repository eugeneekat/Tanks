using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Program
    {
        static void Main(string[] args)
        { 

            KeyboardController contr = new KeyboardController();
            
            GameObject.SetField(15, 15);

            Tank tank = new Tank(0, 0);

            contr.move += tank.OnMove;
            contr.shoot += tank.OnShoot;
            Display disp = new Display();
            tank.move += disp.OnMoveUpdate;
            tank.shoot += disp.OnShootUpdate;
            while(true)
            {
                contr.Action(Console.ReadKey());
            }

        }
    }
}
