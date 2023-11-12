using System.Runtime.CompilerServices;

namespace VV.Algorithm.RPN;

/// <summary>
/// Represents event argument data for evaluating expressions in <see cref="CalculatePostfixHandler"/>.
/// </summary>
public record EventArgsEvaluateRPN
{
    /// <summary>
    /// Gets the mathematical operator or function used in the expression.
    /// </summary>
    public string? MathOperator { get; }

    /// <summary>
    /// Gets the first operand used in the expression.
    /// </summary>
    public double FirstOperand { get; }

    /// <summary>
    /// Gets the second operand used in the expression.
    /// </summary>
    public double SecondOperand { get; }

    /// <summary>
    /// Gets the method that triggered the event.
    /// </summary>
    public string TriggerMethod { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgsEvaluateRPN"/> class with a binary mathematical operator/function, two operands, and the triggering method.
    /// </summary>
    public EventArgsEvaluateRPN(string mathOperator, double firstOperand, double secondOperand, [CallerMemberName] string triggerMethod = "")
    {
        MathOperator = mathOperator;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        TriggerMethod = triggerMethod;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgsEvaluateRPN"/> class with a unary mathematical operand/function, a single operand, and the triggering method.
    /// </summary>
    public EventArgsEvaluateRPN(string mathOperand, double firstOperand, [CallerMemberName] string triggerMethod = "")
    {
        MathOperator = mathOperand; 
        FirstOperand = firstOperand;
        TriggerMethod = triggerMethod;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgsEvaluateRPN"/> class with the triggering method.
    /// </summary>
    public EventArgsEvaluateRPN([CallerMemberName] string triggerMethod = "")
    {
        TriggerMethod = triggerMethod;
    }
}
