using Trellis.Core;
using Trellis.Loader;

namespace Trellis.Engine
{
    public class TrellisEngine
    {
        private readonly Story storyData;
        private readonly Navigator Nav;
        private readonly Inventory PlayerInventory;

        public TrellisEngine(string storyDataPath, string storyType)
        {
            // TODO: Load story data using loader.
            storyData = StoryLoader.LoadStoryData(storyDataPath, storyType) ?? throw new ArgumentException("Invalid story data type supplied");
            Nav = new Navigator(storyData);
            PlayerInventory = new Inventory();

            //TODO: Implement future systems
        }

        public void Reset()
        {
            Nav.ResetHistory();
            //PlayerInventory.Reset();
            // TODO: Implement any initial state conditions here.
        }

        public Passage GetCurrentPassage() => Nav.GetCurrentPassage();

        
        public PassageStep GetCurrentStep() => Nav.GetCurrentStep();
        public bool Step() => Nav.NextStep();
        public bool IsFirstStep() => Nav.IsFirstStep();
        public bool IsLastStep() => Nav.IsLastStep();
        public int GetChoiceCount() => Nav.GetLinkCount();

        public bool NavigateTo(string passageName) => Nav.NavigateTo(passageName);

        public List<PassageLink> GetPassageLinks() => GetCurrentPassage().Links.ToList();

    }
}
