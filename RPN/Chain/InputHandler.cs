namespace VV.Algorithm.RPN;

/// <summary>
/// Base class representing an input handler in a chain of responsibility.
/// It provides the structure for handling input data and linking to the next handler in the chain.
/// </summary>
internal abstract class InputHandler<T,V> : IInputHandler<T,V>
{
    internal IInputHandler? nextHandler;
    protected bool IsResetNeeded { get; init; }

    /// <summary>
    /// Initializes a new instance of the InputHandler class with an option to indicate whether a reset is needed after handling.
    /// </summary>
    /// <param name="isResetNeeded">True if a reset is needed; otherwise, false.</param>
    internal InputHandler(bool isResetNeeded)
    {
        IsResetNeeded = isResetNeeded;
    }

    /// <summary>
    /// Sets the next handler in the chain.
    /// </summary>
    /// <param name="handler">The next input handler to be linked in the chain.</param>
    /// <returns>The provided input handler, for chaining.</returns>
    public IInputHandler<K,T> SetNext<K>(IInputHandler<K,T> handler)
    {
        nextHandler = handler;
        return handler;
    }

    /// <summary>
    /// Determines whether to pass the input data to the next handler or return the input data itself.
    /// Then calls the <see cref="Reset"/> operation, breaking the chain of calls.
    /// The actual processing of the input data occurs in the Handle methods of derived classes.
    /// </summary>
    /// <param name="input">The input data to be processed.</param>
    /// <returns>The result of processing the input data, which may be passed to the next handler.</returns>
    public virtual T Handle(V input)
    {
        var result = nextHandler?.Handle(input) ?? input;

        if (IsResetNeeded)
            Reset();

        return (T)result;
    }

    /// <summary>
    /// Resets the state of the input handler, breaking the chain of responsibility by nullifying the reference to the next handler.
    /// </summary>
    public void Reset()
    {
        nextHandler = null;
    }
}
