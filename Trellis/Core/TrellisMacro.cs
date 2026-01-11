using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trellis.Core;
public sealed record TrellisMacro
(
    string macroType,
    string[] macroArgs
);
