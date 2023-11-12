
namespace VV.Algorithm.RPN;

/// <summary>
/// Represents an interface for handling input data and creating a chain of responsibility pattern.
/// Implementing classes should provide handling capabilities and a way to set the next handler in the chain.
/// </summary>
internal interface IInputHandler
{
    /// <summary>
    /// Sets the next handler in the chain of responsibility.
    /// </summary>
    /// <param name="handler">The next handler to set in the chain.</param>
    /// <returns>The next handler in the chain.</returns>
    internal IInputHandler SetNext(IInputHandler handler);

    /// <summary>
    /// Handles the input data, processing it based on the implementation's logic.
    /// </summary>
    /// <param name="input">The input data to be processed.</param>
    /// <returns>The result of the processing, typically transformed, validated or calculated data.</returns>
    internal string Handle(string input);

    /// <summary>
    /// Resets the state of the input handler.
    /// </summary>
    internal void Reset();
}