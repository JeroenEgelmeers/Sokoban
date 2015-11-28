using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MOD3_sokoban.Model;
using MOD3_sokoban.Model.Enums;
using MOD3_sokoban.Model.Tiles;

namespace MOD3_sokoban.Controller
{
    abstract class GameLayer
    {
        public List<List<Tile>> tileLayer;

        public GameLayer(List<string> boardInput, BoardLayers boardKind)
        {
            tileLayer = new List<List<Tile>>();
            createGrid(boardInput, boardKind);
        }

        public void createGrid(List<string> doolhof, BoardLayers boardKind)
        {
            for (int i = 0; i < doolhof.Count; i++)
            {
                List<Tile> sublist = new List<Tile>();
                foreach (char c in doolhof[i])
                {
                    sublist.Add(GetTileKind(c.ToString()));
                }
                tileLayer.Add(sublist);
            }
        }

        public Grid ShowGrid()
        {
            Grid grid = new Grid();

            for (int i = 0; i < tileLayer.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);

                for (int j = 0; j < tileLayer[i].Count; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    //col.Width = new GridLength(CellSize);
                    grid.ColumnDefinitions.Add(col);

                    //Get vak
                    List<Tile> sublist = tileLayer[i];
                    Tile temp = sublist[j];

                    //Get image
                    Image img = new Image();
                    img.Source = temp.backgroundImage;

                    //Set image
                    img.SetValue(Grid.ColumnProperty, j);
                    img.SetValue(Grid.RowProperty, i);
                    img.Stretch = Stretch.UniformToFill;
                    grid.Children.Add(img);
                }
            }

            return grid;
        }

        public virtual Tile GetTileKind(string kind)
        {
            return null;
        }
    }
}
