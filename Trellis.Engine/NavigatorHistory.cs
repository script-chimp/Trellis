namespace Trellis.Engine;
/// <summary>
/// Maintains a navigation history stack for passages, allowing navigation to previous and current passages within an
/// application.
/// </summary>
/// <remarks>NavigatorHistory is intended for internal use to track the sequence of visited passages. It ensures
/// that there is always at least one passage in the history, representing the current location. The class provides
/// methods to add, remove, retrieve, and reset the current passage in the navigation history.</remarks>
internal class NavigatorHistory
{
    private readonly Stack<string> PassageHistory = new();

    /// <summary>
    /// Initializes a new instance of the NavigatorHistory class with the specified initial passage name.
    /// </summary>
    /// <param name="passageName">The name of the initial passage to add to the navigation history. Cannot be null or empty.</param>
    internal NavigatorHistory(string passageName)
    {
        // Ensure history has an initial value
        Push(passageName);
    }

    /// <summary>
    /// Adds the specified passage name to the top of the passage history stack.
    /// </summary>
    /// <param name="passageName">The name of the passage to add to the history. Cannot be null or empty.</param>
    internal void Push(string passageName) => PassageHistory.Push(passageName);

    /// <summary>
    /// Removes and returns the most recent entry from the passage history, if more than one entry exists.
    /// </summary>
    /// <returns>The most recently added passage if the history contains more than one entry; otherwise, null.</returns>
    internal string? Pop()
    {

        // Don't pop if on only page.
        if (PassageHistory.Count <= 1)
            return null;
            
        return PassageHistory.Pop();
    }

    /// <summary>
    /// Retrieves the current passage from the history without removing it.
    /// </summary>
    /// <returns>A string representing the current passage at the top of the history stack.</returns>
    internal string GetCurrent() => PassageHistory.Peek();

    /// <summary>
    /// Resets the passage history to contain only the specified initial passage.
    /// </summary>
    /// <param name="initialPassageName">The name of the passage to set as the sole entry in the history. Cannot be null.</param>
    internal void Reset(string initialPassageName)
    {
        PassageHistory.Clear();
        PassageHistory.Push(initialPassageName);
    }
}
