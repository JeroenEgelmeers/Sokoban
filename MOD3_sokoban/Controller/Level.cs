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
        public List<string> doolhof;

        public Level(string textName)
        {
            // Read the file as one string.
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            path = path + "\\Model\\Levels\\" + textName + ".txt";
            doolhof = new List<string>();

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    doolhof.Add(line);
                }
            }

            // For testing..
            foreach (string d in doolhof)
            {
               Console.WriteLine(d); 
            }
        }
    }
}
