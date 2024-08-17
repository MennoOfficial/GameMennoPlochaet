using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMennoPlochaet.Entities
{
    internal class Health
    {
        public int lives { get; set; }
        public Health(int lives)
        {
            this.lives = lives;
        }
    }
}
