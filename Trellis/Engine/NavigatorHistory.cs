namespace Trellis.Engine
{
    internal class NavigatorHistory
    {
        private readonly Stack<string> PassageHistory = new();

        internal NavigatorHistory(string passageName)
        {
            // Ensure history has an initial value
            Push(passageName);
        }

        internal void Push(string passageName) => PassageHistory.Push(passageName);

        internal string? Pop()
        {

            // Don't pop if on only page.
            if (PassageHistory.Count <= 1)
                return null;
            
            return PassageHistory.Pop();
        }

        internal string GetCurrent() => PassageHistory.Peek();

        internal void Reset(string initialPassageName)
        {
            PassageHistory.Clear();
            PassageHistory.Push(initialPassageName);
        }
    }
}
