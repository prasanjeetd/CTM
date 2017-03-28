using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter input file path:");
            string filePath = Console.ReadLine();

            if (String.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("Invalid file path");
                Console.ReadLine();
                return;
            }

            try
            {
                InputProcessor ip = new InputProcessor(filePath);
                Conference conference = ip.GetConference();

                if (conference.TalksToSchedule.Count == 0)
                {
                    Console.WriteLine("No talks found. Please add valid talks");
                    Console.ReadLine();
                    return;
                }

                conference.Schedule();
                OutputProcessor op = new OutputProcessor(conference);
                string output = op.Process();
                Console.WriteLine(output);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An Error occurred: ", ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}
