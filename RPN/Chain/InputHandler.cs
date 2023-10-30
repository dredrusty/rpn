namespace VV.Algorithm.RPN;

/// <summary>
/// Base class representing an input handler in a chain of responsibility.
/// It provides the structure for handling input data and linking to the next handler in the chain.
/// </summary>
internal abstract class InputHandler : IInputHandler
{
    internal IInputHandler? nextHandler;

    /// <summary>
    /// Sets the next handler in the chain.
    /// </summary>
    /// <param name="handler">The next input handler to be linked in the chain.</param>
    /// <returns>The provided input handler, for chaining.</returns>
    public IInputHandler SetNext(IInputHandler handler)
    {
        nextHandler = handler;
        return handler;
    }

    /// <summary>
    /// Handles the input data according to the specific implementation of the handler.
    /// </summary>
    /// <param name="input">The input data to be processed.</param>
    /// <returns>The result of processing the input data, which may be passed to the next handler.</returns>
    public abstract string Handle(string input);
}
