using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trellis.Loader;

public sealed record TwinePassageDto
(
    string name,
    string text,
    List<TwineLinkDto> links,
    string pid,
    Dictionary<string, string> position
);

