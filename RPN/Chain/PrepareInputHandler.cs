using System.Text;
using VV.Algorithm.RPN.Resources;

namespace VV.Algorithm.RPN;

/// <summary>
/// Responsible for preparing input expressions by removing whitespace, replacing brackets with parentheses,
/// converting commas to decimal points, and replacing unary minus with tilde.
/// </summary>
internal class PrepareInputHandler : InputHandler
{
    internal PrepareInputHandler(bool isResetNeeded = true) : base(isResetNeeded)
    {
    
    }
    
    /// <summary>
    /// Handles the input by applying necessary formatting transformations (removing whitespace, replacing brackets with parentheses,
    /// converting commas to decimal points, and replacing unary minus with tilde).
    /// </summary>
    /// <param name="input">The input expression to be prepared.</param>
    /// <returns>The prepared expression for further processing.</returns>
    public override string Handle(string input)
    {
        if(string.IsNullOrEmpty(input))
            throw new ArgumentException(RPNRes.PrepareInputEmptyOrNull, nameof(input));

        StringBuilder sb = new(input);

        var preparedInput = sb.Replace(" ", "").
            Replace(",", ".").
            Replace("{", "(").
            Replace("}", ")").
            Replace("[", "(").
            Replace("]", ")").
            ToString();

        for (int i = 0; i < preparedInput.Length; i++)
        {
            if (preparedInput[i] == '-' && (i > 0 && !char.IsDigit(preparedInput[i - 1]) && preparedInput[i - 1] != ')' || i == 0))
            {
                preparedInput = preparedInput.Remove(i, 1);
                preparedInput = preparedInput.Insert(i, "~");
            }
        }

        var result = preparedInput.Trim();

        return base.Handle(result);
    }
}
