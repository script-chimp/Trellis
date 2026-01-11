namespace Trellis.Core
{
    public enum PassageStepType
    {
        display,
        macro
    }

    public sealed record PassageStep
    (
        PassageStepType StepType,
        object? Value
    );
}
