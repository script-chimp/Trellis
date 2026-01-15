using System.Xml.Linq;
using Trellis.Core;

namespace Trellis.Engine;


/// <summary>
/// Provides navigation functionality for traversing passages and steps within a story, including forward and backward
/// navigation, step management, and history tracking.
/// </summary>
/// <remarks>The Navigator class is intended for internal use to manage the user's position within a story
/// structure. It maintains navigation history, supports moving between passages and steps, and allows rewinding or
/// resetting navigation state. This class is not thread-safe.</remarks>
internal class Navigator
{
    private readonly Story StoryData;
    private readonly NavigatorHistory History;
    private int _currentPassageStep = 0;

    /// <summary>
    /// Initializes a new instance of the Navigator class with the specified story data.
    /// </summary>
    /// <param name="storyData"></param>
    internal Navigator(Story storyData)
    {
        StoryData = storyData;
        History = new NavigatorHistory(storyData.InitialPassage);
    }

    /// <summary>
    /// Attempts to navigate to the specified passage by name.
    /// </summary>
    /// <param name="passageName">The name of the passage to navigate to. Must correspond to an existing passage.</param>
    /// <returns>true if navigation to the specified passage was successful; otherwise, false.</returns>
    internal bool NavigateTo(string passageName)
    {
        if (!StoryData.PassageExists(passageName))
        {
            return false;
        }

        History.Push(passageName);
        _currentPassageStep = 0;

        return true;
    }
    
    /// <summary>
    /// Navigates to the previous entry in the navigation history.
    /// </summary>
    /// <remarks>This method does not throw an exception if there is no previous entry in the history. Use the
    /// return value to determine whether navigation occurred.</remarks>
    /// <returns>true if navigation to a previous entry was successful; otherwise, false.</returns>
    internal bool NavigateBack() => History.Pop() != null;

    /// <summary>
    /// Retrieves the name of the current passage in the navigation history.
    /// </summary>
    /// <returns>A string containing the name of the current passage, or null if there is no current passage.</returns>
    internal string GetCurrentPassageName() => History.GetCurrent();

    /// <summary>
    /// Retrieves the current passage in the story based on the navigator's state.
    /// </summary>
    /// <returns>The <see cref="Passage"/> object representing the current passage.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the navigator is in an invalid state and the current passage cannot be determined.</exception>
    internal Passage GetCurrentPassage() => StoryData.GetPassage(GetCurrentPassageName())
                                  ?? throw new InvalidOperationException("Navigator entered into an invalid state.");

    /// <summary>
    /// Resets the current step to the first step in the sequence.
    /// </summary>
    internal void RewindToFirstStep() {_currentPassageStep = 0;}

    /// <summary>
    /// Gets the number of steps in the current passage.
    /// </summary>
    /// <returns>The total number of steps contained in the current passage.</returns>
    internal int StepCount() => GetCurrentPassage().Steps.Count;

    /// <summary>
    /// Gets the zero-based index of the current step in the passage sequence.
    /// </summary>
    /// <returns>An integer representing the zero-based index of the current step. Returns 0 if no steps have been advanced.</returns>
    internal int GetCurentStepIndex() => _currentPassageStep;

    /// <summary>
    /// Gets the current step within the active passage.
    /// </summary>
    /// <returns>The <see cref="PassageStep"/> representing the current step in the active passage.</returns>
    internal PassageStep GetCurrentStep() => GetCurrentPassage().Steps.ElementAt(_currentPassageStep);

    /// <summary>
    /// Determines whether the current step is the last step in the sequence.
    /// </summary>
    /// <returns>true if the current step is the last step; otherwise, false.</returns>
    internal bool IsLastStep() => GetCurentStepIndex() == StepCount() - 1;

    /// <summary>
    /// Determines whether the current step is the first step in the sequence.
    /// </summary>
    /// <returns><see langword="true"/> if the current step is the first step; otherwise, <see langword="false"/>.</returns>
    internal bool IsFirstStep() => GetCurentStepIndex() == 0;

    /// <summary>
    /// Gets the number of links in the current passage.
    /// </summary>
    /// <returns>The number of links available in the current passage.</returns>
    internal int GetLinkCount() => GetCurrentPassage().Links.Count;

    /// <summary>
    /// Advances to the next step in the sequence, if available.
    /// </summary>
    /// <returns>true if the operation successfully advanced to the next step; otherwise, false if the current step is the last
    /// one.</returns>
    internal bool NextStep()
    {
        if (IsLastStep())
            return false;
        
        _currentPassageStep++;
        return true;
    }

    /// <summary>
    /// Resets the history to the initial passage state.
    /// </summary>
    /// <remarks>Call this method to clear all previous history and restore the starting point. This is
    /// typically used to restart the story or session from the beginning.</remarks>
    internal void ResetHistory()
    {
        History.Reset(StoryData.InitialPassage);
    }
}