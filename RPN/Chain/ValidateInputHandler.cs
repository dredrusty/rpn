using System.Text.RegularExpressions;
using VV.Algorithm.RPN.Resources;

namespace VV.Algorithm.RPN;

/// <summary>
/// Handles validation of the input expression. This includes checking for unmatched parentheses, 
/// disallowing consecutive sequences of mathematical operators and functions, and ensuring valid characters.
/// </summary>
internal class ValidateInputHandler : InputHandler
{
    /// <summary>
    /// Gets the regular expression pattern to validate the input expression.
    /// </summary>
    /// <returns>A regular expression pattern for validation.</returns>
    internal static string GetRegexPattern()
    {
        int xOrMore = 2;

        var operators = ReversePolishNotation.Priority.Where(pair => pair.Value < 6 && pair.Key.Length < 3).Select(pair => $@"\{pair.Key}").ToList();

        operators.Add("\\.");

        var functions = ReversePolishNotation.Priority.Where(pair => pair.Value < 6 && pair.Key.Length > 2).Select(pair => $@"{pair.Key}").ToList();

        string maskOperators = "[" + string.Join("", operators) + "]";

        string maskFunctions = "(" + string.Join("|", functions) + ")";

        return new($@"{maskOperators}{{{xOrMore},}}|{maskFunctions}{{{xOrMore},}}");
    }

    /// <summary>
    /// Validates the input expression for unmatched parentheses, invalid characters, 
    /// and consecutive sequences of operators and functions.
    /// </summary>
    /// <param name="input">The input expression to validate.</param>
    /// <returns>The validated input expression.</returns>
    public override string Handle(string input)
    {
        var inputToValidate = input;

        Stack<char> stack = new();

        for (int i = 0; i < inputToValidate.Length; i++)
        {
            if (inputToValidate[i] == ')')
            {
                if (stack.Count == 0 || stack.Peek() != '(')
                    throw new ArgumentException(RPNRes.ValidateInputUnmatchedParenthesis, nameof(input));
                stack.Pop();
            }

            if (inputToValidate[i] == '(')
                stack.Push(inputToValidate[i]);
        }

        if (stack.Count != 0)
            throw new ArgumentException(RPNRes.ValidateInputUnmatchedParenthesis, nameof(input));

        Regex regex = new(GetRegexPattern());

        if (regex.IsMatch(inputToValidate))
            throw new ArgumentException(RPNRes.ValidateInputIncorrectCharacters, nameof(input));

        return nextHandler?.Handle(inputToValidate) ?? inputToValidate;
    }
}
