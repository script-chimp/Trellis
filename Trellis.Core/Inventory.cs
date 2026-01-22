namespace Trellis.Core;

/// <summary>
/// Represents a collection of items and provides methods to manage them.
/// </summary>
/// <remarks>The Inventory class allows adding, removing, and querying items by reference or by item identifier.
/// The collection is publicly managed and exposed as a read-only list to prevent external modification.</remarks>
public class Inventory
{
    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items =>_items.AsReadOnly();

    /// <summary>
    /// Adds the specified item to the collection.
    /// </summary>
    /// <param name="item">The item to add to the collection. Cannot be null.</param>
    public void Add(Item item) => _items.Add(item);

    /// <summary>
    /// Removes the specified item from the collection.
    /// </summary>
    /// <param name="item">The item to remove from the collection. If the item does not exist, no action is taken.</param>
    public void Remove (Item item) => _items.Remove(item);

    /// <summary>
    /// Removes the item with the specified identifier from the collection.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to remove. Cannot be null or empty.</param>
    public void Remove(string itemId)
    {
        Item? item = GetItem(itemId);
        if (item != null)
            Remove(item);
    }

    /// <summary>
    /// Determines whether the collection contains an item with the specified identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to locate in the collection. Cannot be null.</param>
    /// <returns>true if an item with the specified identifier exists in the collection; otherwise, false.</returns>
    public bool Contains(string itemId) => _items.Any(i => i.Id == itemId);

    /// <summary>
    /// Retrieves the item with the specified identifier, if it exists.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to retrieve. Cannot be null.</param>
    /// <returns>The item with the specified identifier, or null if no such item exists.</returns>
    public Item? GetItem(string itemId) => _items.FirstOrDefault(i => i.Id == itemId);
}