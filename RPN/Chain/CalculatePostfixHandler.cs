using System.Globalization;
using VV.Algorithm.RPN.Resources;

namespace VV.Algorithm.RPN;

/// <summary>
/// An implementation of the <see cref="InputHandler"/> that calculates expressions in Reverse Polish Notation (RPN).
/// It handles the evaluation of RPN expressions, supporting binary and unary operators, including standard mathematical functions.
/// Also rises even <see cref="OnCalculateStep"/> after evaluation every step.
/// </summary>
internal class CalculatePostfixHandler : InputHandler
{
    /// <summary>
    /// Represents a delegate for handling events related to evaluating expressions in Reverse Polish Notation (RPN).
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An instance of <see cref="EventArgsEvaluateRPN"/> containing event data.</param>
    internal delegate void CalculateStepHandler(object sender, EventArgsEvaluateRPN e);

    /// <summary>
    /// Occurs when a calculation step is performed during the evaluation of an RPN expression.
    /// </summary>
    internal event CalculateStepHandler? OnCalculateStep;

    /// <summary>
    /// A set containing unary operators.
    /// </summary>
    internal readonly static HashSet<string> unaryOperators = new() { "~", "sin", "cos" };

    /// <summary>
    /// Handles the calculation of an RPN expression.
    /// </summary>
    /// <param name="input">The input expression in RPN format.</param>
    /// <returns>The result of the RPN expression calculation.</returns>
    public override string Handle(string input)
    {
        var toEvaluate = input;

        string[] evaluate = toEvaluate.Split(' ');

        Stack<double> stack = new();

        double first;
        double second;

        for (int i = 0; i < evaluate.Length; i++)
        {
            if (double.TryParse(evaluate[i], CultureInfo.InvariantCulture, out double temp))
                stack.Push(temp);

            if (ReversePolishNotation.Priority.ContainsKey(evaluate[i]))
            {
                if (unaryOperators.Contains(evaluate[i]))
                {
                    first = stack.Count > 0 ? stack.Pop() : 0;

                    stack.Push(EvaluateReversePolishNotation.EvaluateUn(evaluate[i], first));

                    OnCalculateStep?.Invoke(this, new EventArgsEvaluateRPN(evaluate[i], first));
                }
                else
                {
                    second = stack.Count > 0 ? stack.Pop() : 0;

                    if (second == 0 && evaluate[i] == "/")
                        throw new ArgumentException(RPNRes.CalculatePostfixHandlerSecondIsZero);

                    first = stack.Count > 0 ? stack.Pop() : 0;

                    stack.Push(EvaluateReversePolishNotation.EvaluateBin(evaluate[i], first, second));

                    OnCalculateStep?.Invoke(this, new EventArgsEvaluateRPN(evaluate[i], first, second));
                }
            }
        }

        return nextHandler?.Handle(stack.Pop().ToString()) ?? stack.Pop().ToString();
    }
}
