namespace Trellis.Core
{
    /// <summary>
    /// Specifies the type of step within a passage, such as displaying content or executing a macro.
    /// </summary>
    public enum PassageStepType
    {
        display,
        macro
    }

    /// <summary>
    /// Represents a single step in a passage, including its type and associated value.
    /// </summary>
    /// <param name="StepType">The type of the passage step, indicating the action or category of this step.</param>
    /// <param name="Value">The value associated with the step. The expected type and meaning depend on the specified step type. May be null
    /// if the step does not require an associated value.</param>
    public sealed record PassageStep
    (
        PassageStepType StepType,
        object? Value
    );
}
