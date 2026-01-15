using System.Text.Json.Serialization;

namespace Trellis.Core;

/// <summary>
/// Represents a hyperlink to a passage, including its display name, link text, and an indicator of whether the link is
/// broken.
/// </summary>
/// <param name="Name">The unique name or identifier of the target passage.</param>
/// <param name="Text">The text to display for the link.</param>
/// <param name="Broken">Indicates whether the link is considered broken. Set to <see langword="true"/> if the link does not resolve to a
/// valid passage; otherwise, <see langword="false"/>.</param>
public sealed record PassageLink
(
    string Name,
    string Text,
    bool Broken = false
);
