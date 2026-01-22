using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trellis.Core;

/// <summary>
/// Represents a macro within the Trellis interactive storytelling framework, including its type and associated arguments.
/// </summary>
/// <param name="macroType">Macro type</param>
/// <param name="macroArgs">Macro arguments</param>
public sealed record TrellisMacro
(
    string macroType,
    string[] macroArgs
);
