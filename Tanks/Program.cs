using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            int width = 50;
            int height = 20;
            GameField.SetField(width, height);

            Display disp = new Display();
            PlayerController playerContoller = new PlayerController();
            Tank playerTank = new Tank(0, 0, leftTankSpr);
            playerContoller.move    += playerTank.OnMove;
            playerContoller.shoot   += playerTank.OnShoot;
            playerContoller.exit    += playerTank.OnExit;
            playerTank.move         += disp.OnMoveUpdate;
            playerTank.shoot        += disp.OnShootUpdate;
            playerTank.exit         += disp.Exit;

            AIController aiController = new AIController();
            Tank aiTank = new Tank(GameField.MaxWidth, 0, rightTankSpr);
            aiController.move   += aiTank.OnMove;
            aiController.shoot  += aiTank.OnShoot;
            aiController.exit   += aiTank.OnExit;
            aiTank.move         += disp.OnMoveUpdate;          
            aiTank.shoot        += disp.OnShootUpdate;
            aiTank.exit         += disp.Exit;

            aiController.AsyncAction();
            playerContoller.AsyncAction();
            
            while(!GameField.IsEndGame)
            {
                
            }
            Console.ReadKey();
            
        }
    }
}
