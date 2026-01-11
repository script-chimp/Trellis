namespace Trellis.Core;

public sealed record Passage
(
    string Name,
    IReadOnlyList<PassageStep> Steps,
    IReadOnlyList<PassageLink> Links
);
