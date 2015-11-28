using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MOD3_sokoban.Controller;
using MOD3_sokoban.Model;
using MOD3_sokoban.Model.Enums;

namespace MOD3_sokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Level level = new Level("level1");

            GameLayer boardLayer = new Board(level.doolhof, BoardLayers.GameLayer);
            GameLayer objectLayer = new GameObjects(level.doolhof, BoardLayers.ObjectLayer);

            this.boardLayer.Children.Add(boardLayer.ShowGrid());
            this.objectLayer.Children.Add(objectLayer.ShowGrid());

            Console.WriteLine("Test");

        }
    }
}
