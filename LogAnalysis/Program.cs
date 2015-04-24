using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var analysis = new LogAnalysis();
            analysis.ReadLogFile();

            analysis.CalculateMostFrequentDay();
            Console.Read();
        }
    }
}
