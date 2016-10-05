using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class GameObjectStateEventArgs
    {
        //Текстура
        public string Sprite { get; set; }

        //Позиция
        public int NewStatePosX { get; set; }
        public int NewStatePosY { get; set; }
    }
}
