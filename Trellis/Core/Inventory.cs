namespace Trellis.Core;

internal class Inventory
{
    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items =>_items.AsReadOnly();

    public void Add(Item item) => _items.Add(item);

    public void Remove (Item item) => _items.Remove(item);
    public void Remove(string itemId)
    {
        Item? item = GetItem(itemId);
        if (item != null)
            Remove(item);
    }

    public bool Contains(string itemId) => _items.Any(i => i.Id == itemId);

    public Item? GetItem(string itemId) => _items.FirstOrDefault(i => i.Id == itemId);
}