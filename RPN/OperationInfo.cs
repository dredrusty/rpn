using System.Runtime.CompilerServices;

namespace VV.Algorithm.RPN;

/// <summary>
/// Represents information about a mathematical operation or function and operands used in an expression. 
/// Used in <see cref="EventArgsInCalculate"/>.
/// </summary>
public sealed class OperationInfo
{
    /// <summary>
    /// Defining flag, unary and binary operation. 
    /// </summary>
    public bool isUnary;
    
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
    /// Gets the result of one operation.
    /// </summary>
    public double StepResult { get;  }

    /// <summary>
    /// Gets the method that triggered the event.
    /// </summary>
    public string TriggerMethod { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class for a binary operation.
    /// </summary>
    /// <param name="mathOperator">A binary math operation or function.</param>
    /// <param name="firstOperand">The first operand.</param>
    /// <param name="secondOperand">The second operand.</param>
    /// <param name="stepResult">The result of one opertion.</param>
    /// <param name="triggerMethod">A method that triggered the event.</param>
    internal OperationInfo(string? mathOperator, double firstOperand, double secondOperand, double stepResult, string triggerMethod)
    {
        MathOperator = mathOperator;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        TriggerMethod = triggerMethod;
        StepResult = stepResult;
        isUnary = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class for an unary operation.
    /// </summary>
    /// <param name="mathOperator">An unary math operation or function.</param>
    /// <param name="firstOperand">The first operand.</param>
    /// <param name="stepResult">The result of one opertion.</param>
    /// <param name="triggerMethod">The second operand.</param>
    internal OperationInfo(string mathOperator, double firstOperand, double stepResult, [CallerMemberName] string triggerMethod = "")
    {
        MathOperator = mathOperator;
        FirstOperand = firstOperand;
        TriggerMethod = triggerMethod;
        StepResult = stepResult;
        isUnary = true;
    }

    /// <summary>
    /// Gets a string representation of the operation, including operand(s) and operator.
    /// </summary>
    /// <returns>A formatted string representing the operation.</returns>
    public string GetOperationInfo()
    {
        if (!isUnary)
        {
            return $"{FirstOperand}{MathOperator}{SecondOperand} = ";
        }
        else
        {
            return $"{MathOperator}{FirstOperand} = ";
        }
    }
    
    /// <summary>
    /// Returns a string representation of the result of calculating one operation in an expression.
    /// </summary>
    /// <returns>A string representation of the result of calculating one operation in an expression.</returns>
    public string GetResultInfo()
        => $"{StepResult}";
}
