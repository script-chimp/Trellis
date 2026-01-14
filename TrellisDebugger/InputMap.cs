using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrellisDebugger
{
    internal class InputMap
    {
        private Dictionary<ConsoleKey, Action> keyMap;

        public InputMap()
        {
            keyMap = new Dictionary<ConsoleKey, Action>();
        }

        public void BindKey(ConsoleKey key, Action action)
        {
            keyMap[key] = action;
        }

        public void ProcessInput(ConsoleKey key)
        {
            if (keyMap.TryGetValue(key, out Action action))
            {
                action();
            }
        }

        public void UnbindKey(ConsoleKey key)
        {
            keyMap.Remove(key);
        }
    }
}
