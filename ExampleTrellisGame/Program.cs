using System.Reflection.Metadata.Ecma335;
using Trellis.Core;
using Trellis.Engine;
using System.Threading;

string storyDataPath = "C:\\Users\\BrawnSwagger\\source\\repos\\Trellis\\ExampleTrellisGame\\gameData.json";
TrellisEngine game = new TrellisEngine(storyDataPath, "Twine");

Passage currentPassage;
PassageStep currentPassageStep;

void GameLoop()
{

    while (true)
    {
        if (game.IsFirstStep())
            Console.Clear();
        // Update game state
        currentPassage = game.GetCurrentPassage();
        currentPassageStep = game.GetCurrentStep();

        // Handle Step
        switch (currentPassageStep.StepType)
        {
            case PassageStepType.macro:
                ProcessMacro();
                break;
            //case PassageStepType.sleep:
            //    int sleepTime = int.Parse(currentPassageStep.Value.ToString());
            //    Sleep(sleepTime);
            //    break;
            //case PassageStepType.wait:
            //    Console.Write("\nPress any key to continue...");
            //    Console.ReadKey();
            //    ClearLine();
            //    break;
            //case PassageStepType.display:
            //    DisplayText();
            //    break;
            //case PassageStepType.custom:
            //    ProcessCustomMacro();
            //    break;
            default:
                DisplayText();
                break;
        }

        if (!game.Step())
        {
            Sleep(2);
            GetUserInput();
        }
    }
}

void ProcessMacro()
{
    var macro = (TrellisMacro)currentPassageStep.Value;
    switch (macro.macroType)
    {
        case "wait":
            break;
        case "sleep":
            Sleep(int.Parse(macro.macroArgs.First()));
            break;
        case "type":
            TypeText(int.Parse(macro.macroArgs.First()), (string)macro.macroArgs[1]);
            break;
        case "exit":
            Environment.Exit(0);
            break;
    }
}

void Wait()
{
    Console.Write("\nPress any key to continue...");
    Console.ReadKey();
    ClearLine();
}

void ProcessCustomMacro()
{
    string macroValue = currentPassageStep.Value.ToString();
    var macroValues = macroValue.Split("|");
    switch(macroValues[0])
    {
        case "type":
            TypeText(int.Parse(macroValues[1]), macroValues[2]);
            break;
        case "exit":
            Environment.Exit(0);
            break;
    }
}

void TypeText(int delay, string text)
{
    foreach (char letter in text)
    {
        Console.Write(letter);
        Sleep(delay);
    }
}

void ClearLine()
{
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, Console.CursorTop);
}

void Sleep(int sleepTime)
{
    System.Threading.Thread.Sleep(sleepTime);
}

void DisplayText()
{ 
    Console.Write(currentPassageStep.Value.ToString());
}

void GetUserInput()
{
    Console.WriteLine("\n\nAvailable Actions:");

    List<PassageLink> links = game.GetPassageLinks();
    for (var i = 0; i < currentPassage.Links.Count; i++)
    {
        Console.WriteLine($"{i} - {currentPassage.Links[i].Text}");
    }
    string? choiceText;
    int choiceIndex = -1;
    do
    {
        Console.Write($"\nChoice (0-{game.GetChoiceCount()-1}): ");
        choiceText = Console.ReadLine();

        if (String.IsNullOrEmpty(choiceText))
            Console.WriteLine("Empty string");

        try
        {
            choiceIndex = int.Parse(choiceText);
        }
        catch
        {
            continue;
        }
    }
    while (choiceIndex < 0 || choiceIndex >= currentPassage.Links.Count);

    Console.WriteLine($"Navigating to: {currentPassage.Links[choiceIndex].Name}");

    game.NavigateTo(currentPassage.Links[choiceIndex].Name);
}

// Enter game
GameLoop();