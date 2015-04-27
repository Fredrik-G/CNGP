using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //var analysis = new LogAnalysis();
            //analysis.ReadLogFile(false);

            //analysis.SortMostFrequentDayAndHour();
            //Console.Read();
        }
    }
}
