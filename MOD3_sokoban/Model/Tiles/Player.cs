using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOD3_sokoban.Model.Enums;

namespace MOD3_sokoban.Model.Tiles
{
    class Player : Tile
    {
        public Player()
        {
            SetBackgroundImage("player");
            PlayerDirection = PlayerDirections.Right;
        }

        public PlayerDirections PlayerDirection { get; set; }
    }
}
