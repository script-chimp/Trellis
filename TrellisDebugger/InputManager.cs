using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrellisDebugger
{
    internal class InputManager
    {
        internal ConsoleKey CurrentKey;

        public static ConsoleKey GetInput()
        {
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            return keyInfo.Key;
        }

    }
}
