using System.Runtime.CompilerServices;

namespace VV.Algorithm.RPN;

/// <summary>
/// Represents information about a mathematical operation or function and operands used in an expression. 
/// Used in <see cref="EventArgsInCalculate"/>.
/// </summary>
internal class OperationInfo
{
    /// <summary>
    /// Gets the mathematical operator or function used in the expression.
    /// </summary>
    internal string? MathOperator { get; }

    /// <summary>
    /// Gets the first operand used in the expression.
    /// </summary>
    internal double FirstOperand { get; }

    /// <summary>
    /// Gets the second operand used in the expression.
    /// </summary>
    internal double SecondOperand { get; }

    /// <summary>
    /// Gets the method that triggered the event.
    /// </summary>
    internal string TriggerMethod { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class for a binary operation.
    /// </summary>
    /// <param name="mathOperator">A binary math operation or function.</param>
    /// <param name="firstOperand">The first operand.</param>
    /// <param name="secondOperand">The second operand.</param>
    /// <param name="triggerMethod">A method that triggered the event.</param>
    internal OperationInfo(string? mathOperator, double firstOperand, double secondOperand, string triggerMethod)
    {
        MathOperator = mathOperator;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        TriggerMethod = triggerMethod;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class for an unary operation.
    /// </summary>
    /// <param name="mathOperator">An unary math operation or function.</param>
    /// <param name="firstOperand">The first operand.</param>
    /// <param name="triggerMethod">The second operand.</param>
    internal OperationInfo(string mathOperator, double firstOperand, [CallerMemberName] string triggerMethod = "")
    {
        MathOperator = mathOperator;
        FirstOperand = firstOperand;
        TriggerMethod = triggerMethod;
    }
}
