using System.Text.Json.Serialization;

namespace Trellis.Core;

public sealed record PassageLink
(
    string Name,
    string Text,
    bool Broken = false
);
