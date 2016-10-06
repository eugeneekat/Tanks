﻿using System;
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
            GameField.SetField(50, 20);


            KeyboardController contr = new KeyboardController();
            Tank tank = new Tank(0, 0, leftTankSpr);
            contr.move += tank.OnMove;
            contr.shoot += tank.OnShoot;

            AIController ai = new AIController();
            Tank tank2 = new Tank(50, 0, rightTankSpr);
            ai.move += tank2.OnMove;
            ai.shoot += tank2.OnShoot;


            Display disp = new Display();
            tank.move += disp.OnMoveUpdate;
            tank.shoot += disp.OnShootUpdate;
            tank2.move += disp.OnMoveUpdate;
            tank2.shoot += disp.OnShootUpdate;
            ai.AsyncAction();
            while (true)
            {
                contr.Action(Console.ReadKey());
            }

        }
    }
}
