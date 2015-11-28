using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOD3_sokoban.Model;
using MOD3_sokoban.Model.Enums;
using MOD3_sokoban.Model.Tiles;

namespace MOD3_sokoban.Controller
{
    class Board : GameLayer
    {

        public Board(List<string> boardInput, BoardLayers boardKind) : base(boardInput, boardKind)
        {
        }

        public override Tile GetTileKind(string kind)
        {
            switch (kind)
            {
                case "#": // Wall
                    return new Wall();
                case "x": // Player
                    return new Destination();
                default:
                    return new Floor();
            }
        }
    }
}
