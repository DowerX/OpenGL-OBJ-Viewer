using System;
using System.Windows.Forms;
using OpenTKTest.Forms;

namespace OpenTKTest
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            string split = "=";
            if(args.Length == 0)
            {
                Console.WriteLine("Starting with defaults. Write --help for help!");
            } else
            {
                foreach(string _arg in args)
                {
                    string[] _args = _arg.Split(split.ToCharArray());
                    if (_args[0] == "--file")
                    {
                        CalculateMesh.GetFile(_args[1]);
                    }
                    if (_args[0] == "--speed")
                    {
                        Game.rotation_speed = float.Parse(_args[1]);
                    }
                    if (_args[0] == "--help")
                    {
                        Console.WriteLine("You can specify file with --file=[FILEPATH]");
                        Console.WriteLine("You can specify rotaition speed with --speed=[FLOAT]");
                        Console.WriteLine("You can specify rotaition framerate cap with --framerate=[INT]");
                    }
                    if (_args[0] == "--framerate")
                    {
                        Game.framerate = int.Parse(_args[1]);
                    }
                }
            }

            Application.Run(new Startup());

            if (CalculateMesh.lines == null)
            {
                Console.WriteLine("Didn't set file path! Type --help for help!");
                //Application.Run(new NoFile());
                MessageBox.Show("No file was given to load!");
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}