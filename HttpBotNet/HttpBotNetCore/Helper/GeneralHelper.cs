using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Helper
{
    static public class GeneralHelper
    {
        public static bool TryCreateDirectoryForThisFile(string path)
        {
            try
            {
                var pathToDirectory = Path.GetDirectoryName(path);
                if (!Directory.Exists(pathToDirectory))
                    Directory.CreateDirectory(pathToDirectory);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            
        }
    }
}
