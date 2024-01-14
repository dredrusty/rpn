using System.Runtime.CompilerServices;

namespace VV.Algorithm.RPN;

/// <summary>
/// Represents event arguments data for calculating every step of expressions in <see cref="CalculatePostfixHandler"/>.
/// </summary>
public record EventArgsInCalculate
{
    /// <summary>
    /// Gets the instance of OperationInfo class. 
    /// </summary>
    public OperationInfo OperationInfo { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgsInCalculate"/> class with a binary mathematical operator/function, two operands, and the triggering method.
    /// </summary>
    internal EventArgsInCalculate(string mathOperator, double firstOperand, double secondOperand, double stepResult, [CallerMemberName] string triggerMethod = "")
    {
        OperationInfo = new OperationInfo(mathOperator, firstOperand, secondOperand, stepResult, triggerMethod);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgsInCalculate"/> class with a unary mathematical operand/function, a single operand, and the triggering method.
    /// </summary>
    internal EventArgsInCalculate(string mathOperator, double firstOperand, double stepResult, [CallerMemberName] string triggerMethod = "")
    {
        OperationInfo = new OperationInfo(mathOperator, firstOperand, stepResult, triggerMethod);
    }
}
