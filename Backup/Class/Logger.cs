using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
   public class Logger
    {
        public static void WriteToLog(string message)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{message} - {DateTime.Now}");
            }
        }
    }
}
