﻿using System;
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
                Application.Run(new Startup());
            } else
            {
                foreach(string _arg in args)
                {
                    string[] _args = _arg.Split(split.ToCharArray());
                    if (_args[0] == "--file" || _args[0] == "-f")
                    {
                        CalculateMesh.GetFile(_args[1]);
                    }
                    if (_args[0] == "--rotate" || _args[0] == "-r")
                    {
                        Game.rotation_speed_x = float.Parse(_args[1]);
                        Game.rotation_speed_y = float.Parse(_args[2]);
                        Game.rotation_speed_z = float.Parse(_args[3]);
                    }
                    if (_args[0] == "--help" || _args[0] == "-h")
                    {
                        Console.WriteLine("You can specify file with --file=[FILEPATH]");
                        Console.WriteLine("You can specify rotaition speed with --speed=[FLOAT]");
                        Console.WriteLine("You can specify framerate cap with --framerate=[INT]");
                    }
                    if (_args[0] == "--framerate" || _args[0] == "-fps")
                    {
                        Game.framerate = int.Parse(_args[1]);
                    }
                }

                using (Game game = new Game())
                {
                    game.Run(60);
                }
            }

            if (CalculateMesh.lines == null)
            {
                Console.WriteLine("Didn't set file path! Type --help for help!");
                MessageBox.Show("No file was given to load!");
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}