
namespace VV.Algorithm.RPN;

/// <summary>
/// A utility class providing methods to evaluate binary and unary operations in Reverse Polish Notation (RPN).
/// </summary>
internal static class EvaluateReversePolishNotation
{
    /// <summary>
    /// Evaluates a binary operation with specified operands.
    /// </summary>
    /// <param name="operand">The binary operator to apply (e.g., +, -, *, /, ^).</param>
    /// <param name="first">The first operand.</param>
    /// <param name="second">The second operand.</param>
    /// <returns>The result of the binary operation.</returns>
    internal static double EvaluateBin(string operand, double first, double second)
        => operand switch
        {
            "+" => first + second,
            "-" => first - second,
            "*" => first * second,
            "/" => first / second,
            "^" => Math.Pow(first, second),
            _ => 0,
        };

    /// <summary>
    /// Evaluates a unary operation with a specified operand.
    /// </summary>
    /// <param name="operand">The unary operator to apply (e.g., ~, sin, cos).</param>
    /// <param name="first">The operand for the unary operation.</param>
    /// <returns>The result of the unary operation.</returns>
    internal static double EvaluateUn(string operand, double first)
        => operand switch
        {
            "~" => -first,
            "sin" => Math.Sin(first),
            "cos" => Math.Cos(first),
            _ => 0,
        };
}
