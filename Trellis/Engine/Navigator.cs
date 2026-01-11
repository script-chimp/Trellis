using System.Xml.Linq;
using Trellis.Core;

namespace Trellis.Engine;

internal class Navigator
{
    private readonly Story StoryData;
    private readonly NavigatorHistory History;
    private int _currentPassageStep = 0;

    internal Navigator(Story storyData)
    {
        StoryData = storyData;
        History = new NavigatorHistory(storyData.InitialPassage);
    }

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
    
    internal bool NavigateBack() => History.Pop() != null;

    internal string GetCurrentPassageName() => History.GetCurrent();

    internal Passage GetCurrentPassage() => StoryData.GetPassage(GetCurrentPassageName())
                                         ?? throw new InvalidOperationException("Navigator entered into an invalid state.");

    internal void RewindToFirstStep() {_currentPassageStep = 0;}
    internal int StepCount() => GetCurrentPassage().Steps.Count;
    internal int GetCurentStepIndex() => _currentPassageStep;
    internal PassageStep GetCurrentStep() => GetCurrentPassage().Steps.ElementAt(_currentPassageStep);
    internal bool IsLastStep() => GetCurentStepIndex() == StepCount() - 1;
    internal bool IsFirstStep() => GetCurentStepIndex() == 0;
    internal int GetLinkCount() => GetCurrentPassage().Links.Count;
    internal bool NextStep()
    {
        if (IsLastStep())
            return false;
        
        _currentPassageStep++;
        return true;
    }

    internal void ResetHistory()
    {
        History.Reset(StoryData.InitialPassage);
    }
}