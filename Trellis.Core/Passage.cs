namespace Trellis.Core;

/// <summary>
/// Represents a named sequence of steps and associated links within a passage-based workflow or narrative.
/// </summary>
/// <remarks>Use the Passage record to model a discrete section within a larger interactive flow, such as in
/// interactive fiction, tutorials, or guided processes. Each passage contains a set of steps and may provide links to
/// other passages, enabling branching or navigation.</remarks>
/// <param name="Name">The unique name that identifies the passage.</param>
/// <param name="Steps">The ordered collection of steps that define the content or actions within the passage.</param>
/// <param name="Links">The collection of links that connect this passage to other passages or destinations.</param>
public sealed record Passage
(
    string Name,
    IReadOnlyList<PassageStep> Steps,
    IReadOnlyList<PassageLink> Links
);
