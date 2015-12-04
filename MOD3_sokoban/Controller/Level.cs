using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOD3_sokoban.Controller
{
    class Level
    {
        public List<string> levelInput;

        public Level(string textName)
        {
            // Read the file as one string.
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            path = path + "\\Model\\Levels\\" + textName + ".txt";
            levelInput = new List<string>();

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    levelInput.Add(line);
                }
            }
        }
    }
}
