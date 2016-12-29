using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestHeartNpcEditor
{
    class DungeonData
    {
        public bool bigKey = false, compass = false, map = false, crystal = false, entrance = false;
        public byte dungeonPos = 0;
        public byte bossAI = 0;
        public DungeonData(byte pos)
        {
            dungeonPos = pos;
            bigKey = false; compass = false; map = false; crystal = false; entrance = false;
        }
    }
}
