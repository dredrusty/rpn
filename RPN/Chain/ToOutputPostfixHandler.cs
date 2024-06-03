using System.Text;

namespace VV.Algorithm.RPN;

/// <summary>
/// Handles converting an input expression into its corresponding Postfix (Reverse Polish Notation) form.
/// </summary>
internal class ToOutputPostfixHandler : InputHandler<string, string>
{
    internal ToOutputPostfixHandler(bool isResetNeeded = true) : base(isResetNeeded)
    {
    
    }

    /// <summary>
    /// Handles the input by converting it into Postfix (Reverse Polish Notation) form.
    /// </summary>
    /// <param name="input">The input expression to be converted.</param>
    /// <returns>The input expression in Postfix notation for further processing.</returns>
    public override string Handle(string input)
    {
        var preparedInput = input;

        StringBuilder temp = new();

        StringBuilder outputPostfix = new();

        Stack<string> stack = new();

        var pos = 0;

        while (pos < preparedInput!.Length)
        {
            // Process numeric values
            if (char.IsDigit(preparedInput[pos]))
            {
                while (pos < preparedInput.Length && (char.IsDigit(preparedInput[pos]) || preparedInput[pos] == '.' || preparedInput[pos] == ','))
                {
                    temp.Append(preparedInput[pos]);
                    pos++;
                }

                outputPostfix.Append(temp).Append(' ');
                temp.Clear();
            }

            // Process opening parentheses
            if (pos < preparedInput.Length && preparedInput[pos] == '(')
            {
                stack.Push(preparedInput[pos].ToString());
                pos++;
            }

            // Process closing parentheses
            if (pos < preparedInput.Length && preparedInput[pos] == ')')
            {
                while (stack.Count > 0 && stack.Peek() != "(")
                {
                    outputPostfix.Append(stack.Pop()).Append(' ');
                }

                stack.Pop();
                pos++;
            }

            // Process operators
            if (pos < preparedInput.Length && ReversePolishNotation.Priority.ContainsKey(preparedInput[pos].ToString()) && preparedInput[pos] != ')' && preparedInput[pos] != '(')
            {
                while (ReversePolishNotation.Priority.ContainsKey(preparedInput[pos].ToString()))
                {
                    while (stack.Count > 0 && ReversePolishNotation.Priority[preparedInput[pos].ToString()] <= ReversePolishNotation.Priority[stack.Peek().ToString()] && stack.Peek() != "(")
                    {
                        outputPostfix.Append(stack.Pop()).Append(' ');
                    }

                    stack.Push(preparedInput[pos].ToString());
                    pos++;
                }
            }

            // Process letters (functions)
            if (pos < preparedInput.Length && char.IsLetter(preparedInput[pos]))
            {
                while (char.IsLetter(preparedInput[pos]))
                {
                    temp.Append(preparedInput[pos]);
                    pos++;
                }

                while (ReversePolishNotation.Priority.ContainsKey(temp.ToString()) && stack.Count > 0 && ReversePolishNotation.Priority[temp.ToString()] <= ReversePolishNotation.Priority[stack.Peek().ToString()] && stack.Peek() != "(")
                {
                    outputPostfix.Append(stack.Pop()).Append(' ');
                }

                stack.Push(temp.ToString());
                temp.Clear();
            }
        }

        // Process remaining operators in the stack
        foreach (string operators in stack)
            outputPostfix.Append(operators).Append(' ');

        var result = outputPostfix.ToString().Trim();

        return base.Handle(result);
    }
}
