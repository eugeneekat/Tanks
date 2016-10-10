using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class GameObjectStateEventArgs
    {
        //Текстура для отрисовки
        public string [] Sprite { get; set; }

        //Позиция объекта
        public int NewStatePosX { get; set; }
        public int NewStatePosY { get; set; }

        //Жив ли объект
        public bool IsAlive { get; set; } = true;
    }
}
