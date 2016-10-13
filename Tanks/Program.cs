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
            //Изображение игрока и компьютера
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
            //Устанавливает размеры поля
            int width = 50;
            int height = 20;
            GameField.SetField(width, height);

            //Создаем класс отрисовки
            Display disp = new Display();
            //Класс управление игрока
            PlayerController playerContoller = new PlayerController();
            //Класс танка игрока
            Tank playerTank = new Tank(0, 0, leftTankSpr);
            //Подписка танка на события контроллера
            playerContoller.move    += playerTank.OnMove;
            playerContoller.shoot   += playerTank.OnShoot;
            playerContoller.exit    += playerTank.OnExit;
            //Подписка отрисовщика на события танка
            playerTank.move         += disp.OnMoveUpdate;
            playerTank.shoot        += disp.OnShootUpdate;
            playerTank.exit         += disp.Exit;

            //Тоже самое что у игрока
            AIController aiController = new AIController();
            Tank aiTank = new Tank(GameField.MaxWidth, 0, rightTankSpr);
            aiController.move   += aiTank.OnMove;
            aiController.shoot  += aiTank.OnShoot;
            aiController.exit   += aiTank.OnExit;
            aiTank.move         += disp.OnMoveUpdate;          
            aiTank.shoot        += disp.OnShootUpdate;
            aiTank.exit         += disp.Exit;

            //Запуск асинхронных действий компьютера и игрока
            aiController.AsyncAction();
            playerContoller.AsyncAction();

            //Бесконечный цикл пока игра не закончена
            while (!GameField.IsEndGame) { }

            //Считать нажатие так как поток игрока в режиме ожидания нажатия клавишы
            Console.ReadKey();
            
        }
    }
}
