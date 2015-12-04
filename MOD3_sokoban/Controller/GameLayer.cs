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
    public abstract class GameLayer
    {
        public List<List<Tile>> tileLayer;

        public GameLayer(List<string> boardInput, BoardLayers boardKind)
        {
            tileLayer = new List<List<Tile>>();
            CreateGrid(boardInput, boardKind);
        }

        public void CreateGrid(List<string> boardInput, BoardLayers boardKind)
        {
            foreach (string t in boardInput)
            {
                List<Tile> sublist = t.Select(c => GetTileKind(c.ToString())).ToList();
                tileLayer.Add(sublist);
            }
        }

        public Grid ShowGrid()
        {
            Grid grid = new Grid();

            for (var i = 0; i < tileLayer.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);

                for (var j = 0; j < tileLayer[i].Count; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    grid.ColumnDefinitions.Add(col);

                    List<Tile> sublist = tileLayer[i];
                    Tile temp = sublist[j];

                    Image img = new Image();
                    img.Source = temp.BackgroundImage;

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
