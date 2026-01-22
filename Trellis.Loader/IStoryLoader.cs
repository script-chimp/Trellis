using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellis.Core;

namespace Trellis.Loader
{
    internal interface IStoryLoader
    {
        Story LoadStoryData(string fileText);

    }
}
