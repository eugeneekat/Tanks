using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class GameObjectStateEventArgs
    {
        public int OldStatePosX { get; set; }
        public int NewStatePosX { get; set; }
        public int OldStatePosY { get; set; }
        public int NewStatePosY { get; set; }
    }
}
