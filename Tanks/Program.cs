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
            string[] leftTankSpr = {"        \n",
                                    "------  \n",
                                    "|   ----\n",
                                    "|   ----\n",
                                    "------  \n",
                                    "        \n"};

            string[] rightTankSpr = {"        \n",
                                    "  ------\n",
                                    "----   |\n",
                                    "----   |\n",
                                    "  ------\n",
                                    "        \n"};

            KeyboardController contr = new KeyboardController();
           
            GameField.SetField(50, 20);

            Tank tank = new Tank(0, 0, leftTankSpr);
            Tank tank2 = new Tank(50, 0, rightTankSpr);
            contr.move += tank2.OnMove;
            contr.shoot += tank2.OnShoot;

            contr.move += tank.OnMove;
            contr.shoot += tank.OnShoot;
            Display disp = new Display();
            tank.move += disp.OnMoveUpdate;
            tank.shoot += disp.OnShootUpdate;
            tank2.move += disp.OnMoveUpdate;
            tank2.shoot += disp.OnShootUpdate;
            while (true)
            {
                contr.Action(Console.ReadKey());
            }

        }
    }
}
