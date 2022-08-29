using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Helper
{
    /// <summary>
    /// General Helper class for necessary stuff 
    /// </summary>
    static public class GeneralHelper
    {
        /// <summary>
        /// Tries to create a directory for the file in string value.
        /// Doesn't work if a file named exactly like the directory exists.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>success/fail bool</returns>
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
