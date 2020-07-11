using GNMFInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PackerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // If there are no arguments passed then it acts as if help command is ran
            if (args.Count() == 0)
            {
                Program.ShowHelpText();
                return;
            }

            switch (args[0])
            {
                default:
                    {
                        if (args.Count() == 1 && Directory.Exists(args[0]))
                        {
                            // If a single existing folder is inputted, the tool will autopack that
                            // The following codes sets up that process
                        
                            string inputDir = args[0];
                            args = new string[5];
                            args[0] = "-p"; 

                            args[1] = "-i";
                            args[2] = inputDir;

                            args[3] = "-o";
                            args[4] = Path.GetDirectoryName(inputDir) + "\\output.ba2";

                            goto case "-p";
                        }
                        else
                        {
                            Program.ShowHelpText(true);
                            return;
                        }
                    }

                case "-h":
                case "-help":
                    {
                        Program.ShowHelpText();
                        return;
                    }

                case "-p":
                case "-pack":
                    {
                        string inputDir = string.Empty;
                        string outputPath = string.Empty;
                        bool isStrTableSaved = true;

                        for (uint i = 1; i < args.Count(); i++)
                        {
                            string option = args[i];

                            switch (option)
                            {
                                default:
                                    {
                                        Program.ShowHelpText(true);
                                        return;
                                    }

                                case "-nostrtbl":
                                    {
                                        isStrTableSaved = false;
                                        break;
                                    }

                                case "-i":
                                case "-indir":
                                    {
                                        i++;
                                        inputDir = args[i];
                                        break;
                                    }

                                case "-o":
                                case "-out":
                                    {
                                        i++;
                                        outputPath = args[i];
                                        break;
                                    }
                            }
                        }

                        if (string.IsNullOrEmpty(inputDir) || string.IsNullOrEmpty(outputPath))
                        {
                            Program.ShowHelpText(true);
                            return;
                        }

                        Console.WriteLine("Reading and verifying files...");
                        var gnfList = new List<GNF>();

                        foreach(string file in Directory.GetFiles(inputDir, "*", SearchOption.AllDirectories))
                        {
                            if (GNF.IsFileValid(file))
                            {
                                var gnfFile = new GNF
                                {
                                    EntryStr = file.Substring(inputDir.Length + 1),
                                    RealPath = file
                                };

                                gnfList.Add(gnfFile);
                            }
                        }

                        Console.WriteLine("Packing...");
                        GNMF.Write(outputPath, gnfList, isStrTableSaved);

                        Console.WriteLine("Done!\n");
                        break;
                    }
            }
        }

        private static void ShowHelpText(bool isInvalidUsage = false)
        {
            if (isInvalidUsage)
            {
                Console.WriteLine("Invalid usage\n");
            }

            Console.WriteLine("BethesdaArchive2 GNMF Packer Cli\nCopyright (c) 2020  SockNastre\nVersion: 1.0.0.0\n\n" +
                "Usage: \"BethesdaArchive2 GNMF Packer Cli.exe\" <Command> <Options>\n\nCommands:\n-pack (-p)\n-help (-h)\n\n" +
                "Pack Options:\n-nostrtbl\n-indir (-i)\n-out (-o)\n\nExamples:\n\n\"BethesdaArchive2 GNMF Packer Cli.exe\" -pack -i \"C:\\Data\" -o \"C:\\output.ba2\"" +
                "\n                                       -pack -nostrtbl -i \"C:\\Data\" -o \"C:\\output.ba2\"\n\n");
        }
    }
}