using Trellis.Core;
using Trellis.Loader;

namespace Trellis.Engine;

/// <summary>
/// <c>TrellisEngine</c> is the main class responsible for managing the state and progression of an interactive story.
/// </summary>
public class TrellisEngine
{
    private readonly Story storyData;
    private readonly Navigator Nav;
    private readonly Inventory PlayerInventory;

    /// <summary>
    /// Initializes a new instance of the TrellisEngine class using the specified story data path and story type.
    /// </summary>
    /// <param name="storyDataPath">The file path to the story data to be loaded. This should point to a valid story data file compatible with the
    /// specified story type.</param>
    /// <param name="storyType">The type of story data to load. This determines how the story data is interpreted and processed.</param>
    /// <exception cref="ArgumentException">Thrown if the story data cannot be loaded for the specified story type, or if the provided story data path or
    /// type is invalid.</exception>
    public TrellisEngine(string storyDataPath, string storyType)
    {
        // TODO: Load story data using loader.
        storyData = StoryLoader.LoadStoryData(storyDataPath, storyType) ?? throw new ArgumentException("Invalid story data type supplied");
        Nav = new Navigator(storyData);
        PlayerInventory = new Inventory();

        //TODO: Implement future systems
    }

    /// <summary>
    /// Resets the state of the object to its initial conditions.
    /// </summary>
    /// <remarks>Call this method to clear any navigation history and prepare the object for reuse. This is
    /// typically used to reinitialize the object without creating a new instance.</remarks>
    public void Reset()
    {
        Nav.ResetHistory();
        //PlayerInventory.Reset();
        // TODO: Implement any initial state conditions here.
    }

    /// <summary>
    /// Gets the current passage in the navigation sequence.
    /// </summary>
    /// <returns>The current <see cref="Passage"/> instance representing the active passage. Returns <see langword="null"/> if
    /// there is no current passage.</returns>
    public Passage GetCurrentPassage() => Nav.GetCurrentPassage();

    /// <summary>
    /// Gets the current step in the navigation sequence.
    /// </summary>
    /// <returns>The current <see cref="PassageStep"/> representing the user's position in the navigation. Returns <see
    /// langword="null"/> if there is no current step.</returns>
    public PassageStep GetCurrentStep() => Nav.GetCurrentStep();

    /// <summary>
    /// Advances the navigation to the next step in the sequence.
    /// </summary>
    /// <returns>true if the navigation successfully advanced to the next step; otherwise, false.</returns>
    public bool Step() => Nav.NextStep();

    /// <summary>
    /// Determines whether the current navigation position is at the first step.
    /// </summary>
    /// <returns><see langword="true"/> if the current step is the first in the navigation sequence; otherwise, <see
    /// langword="false"/>.</returns>
    public bool IsFirstStep() => Nav.IsFirstStep();

    /// <summary>
    /// Determines whether the current step is the last step in the navigation sequence.
    /// </summary>
    /// <returns>true if the current step is the last step; otherwise, false.</returns>
    public bool IsLastStep() => Nav.IsLastStep();

    /// <summary>
    /// Gets the number of available navigation choices.
    /// </summary>
    /// <returns>The total number of navigation choices currently available. Returns 0 if no choices are present.</returns>
    public int GetChoiceCount() => Nav.GetLinkCount();

    /// <summary>
    /// Attempts to navigate to the passage with the specified name.
    /// </summary>
    /// <param name="passageName">The name of the passage to navigate to. Cannot be null or empty.</param>
    /// <returns>true if navigation to the specified passage was successful; otherwise, false.</returns>
    public bool NavigateTo(string passageName) => Nav.NavigateTo(passageName);

    /// <summary>
    /// Retrieves a list of links associated with the current passage.
    /// </summary>
    /// <returns>A list of <see cref="PassageLink"/> objects representing the available links in the current passage. The list
    /// will be empty if the passage contains no links.</returns>
    public List<PassageLink> GetPassageLinks() => GetCurrentPassage().Links.ToList();

}