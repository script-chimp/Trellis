using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trellis.Loader;

public sealed record TwineLinkDto
(
    string name,
    string text,
    bool broken = false
);