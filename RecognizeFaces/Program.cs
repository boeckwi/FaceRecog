using FaceRecog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizeFaces
{
    class Program
    {
        FileInfo in_file;
        FileInfo out_file;

        static void Main(string[] args)
        {
            new Program().Start(args);

        }

        void Start(string[] args)
        {
            ParseArgs(args);

            var a = new Algorithm(in_file.FullName);
            var result = a.Process();
            result.Save(out_file.FullName);
        }

        void ParseArgs(string[] args)
        {
            try
            {
                in_file = new FileInfo(args[0]);
                out_file = new FileInfo(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Einlesen der Parameter. {0}", ex.Message);
                Console.WriteLine("Aufrufbeispiel: RegocnizeFaces input.jpg output.jpg");
            }
        }
    }
}
