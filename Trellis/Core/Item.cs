namespace Trellis.Core;

/// <summary>
/// Represents an immutable item with an identifier, name, and description.
/// </summary>
/// <param name="Id">The unique identifier for the item. Cannot be null.</param>
/// <param name="Name">The display name of the item. Cannot be null.</param>
/// <param name="Description">A textual description of the item. Cannot be null.</param>
public sealed record Item
    (
        string Id,
        string Name,
        string Description
    );