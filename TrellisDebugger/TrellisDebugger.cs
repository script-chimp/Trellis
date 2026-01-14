using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellis.Engine;
using Trellis.Core;
using System.Drawing;
using System.Data;

namespace TrellisDebugger;

public class TrellisDebugger
{
    InputManager input;
    InputMap inputMap;
    public TrellisEngine engine;
    public Passage currentPassage;
    public PassageStep currentPassageStep;
    public TrellisMacro currentMacro;

    private ConsoleColor fg = Console.ForegroundColor;
    private ConsoleColor bg = Console.BackgroundColor;
    private int width = Console.WindowWidth;

    

    public TrellisDebugger(TrellisEngine trellisEngine)
    {
        engine = trellisEngine;
        inputMap = new InputMap();
        inputMap.BindKey(ConsoleKey.UpArrow, DisplayEngineInfo);
        Main();
    }

    public void Main()
    {
        DisplayEngineInfo();

        while(true)
        {
            var key = InputManager.GetInput();

            inputMap.ProcessInput(key);
            //currentPassage = engine.GetCurrentPassage();
            //currentPassageStep = engine.GetCurrentStep();
            //currentMacro = currentPassageStep.StepType == PassageStepType.macro
            //    ? (TrellisMacro)currentPassageStep.Value
            //    : null;
            //// Placeholder for debugger functionality
            //DisplayPassageInfo();
            //Update();
        }

    }

    void Update()
    {
        ProcessInput();   
    }

    void ProcessInput()
    {
        // Get user input
        ConsoleKeyInfo input = Console.ReadKey();

        ConsoleKey key = input.Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                Console.WriteLine("Up");
                break;
            case ConsoleKey.DownArrow:
                Console.WriteLine("Down");
                break;
            case ConsoleKey.Enter:
                Console.WriteLine("Enter");
                break;
            case ConsoleKey.G:
                Console.WriteLine("Goto");
                break;
            case ConsoleKey.Q:
                Console.WriteLine("Exit");
                break;
        }
    }

    private void DisplayEngineInfo()
    {
        WriteHeader("Engine");
        Console.WriteLine($"Current Passage: {engine.GetCurrentPassage().Name}".PadRight(width));
        Console.WriteLine($"Is First Step: {engine.IsFirstStep()}");
        Console.WriteLine($"Is Last Step: {engine.IsLastStep()}");
        Console.WriteLine($"Available Choices: {engine.GetChoiceCount()}");
        Console.WriteLine("");
    }

    private void WriteHeader(string headerText, bool center=true)
    {
        int startPos = 0;
        if (center)
            startPos = width/2 - headerText.Length/2;
        Console.ForegroundColor = bg;
        Console.BackgroundColor = fg;
        Console.WriteLine(headerText.PadLeft(startPos).PadRight(width));
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
    }

    private void DisplayMacroInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[Macro] Type: {currentMacro.macroType}");
        Console.WriteLine($"[Macro] Values: {string.Join(", ", currentMacro.macroArgs)}");
        Console.ForegroundColor = fg;
    }

    private void DisplayPassageInfo()
    {
        WriteHeader("Passage");
        Console.WriteLine($"Name: {currentPassage.Name}");
        Console.WriteLine($"Steps: {currentPassage.Steps.Count}");
        Console.WriteLine($"Links: {currentPassage.Links.Count}");
        Console.WriteLine("");
    }

    private void DisplayPassageStepInfo()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n[Passage Step] Text: {currentPassageStep.Value}");
        Console.ForegroundColor = fg;
    }
}
