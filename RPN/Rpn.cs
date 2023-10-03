using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RPN;

public class Rpn
{
    readonly static Dictionary<string, int> Priority = new()
    {
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 },
        { "^", 3 },
        { "sin" , 4 },
        { "cos" , 4 },
        { "~", 5 },
        { "(", 6 },
        { ")", 6 },
    };

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

    internal static double EvaluateUn(string operand, double first)
        => operand switch
        {
            "~" => -first,
            "sin" => Math.Sin(first),
            "cos" => Math.Cos(first),
            _ => 0,
        };


    public string InputInfix { get; private set; }
    public string OutputPostfix { get; private set; }

    public Rpn(string input)
    {
        InputInfix = input;
        OutputPostfix = ToOutputPostfix(InputInfix);
    }

    internal static string PrepareInput(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '-' && ((i > 0 && !char.IsDigit(input[i - 1]) && input[i - 1] != ')') || i == 0))
            {
                input = input.Remove(i, 1);
                input = input.Insert(i, "~");
            }
        }

        return input.Replace(" ", "").Replace(",", ".").Replace("{", "(").Replace("[", "(").Replace("}", ")").Replace("]", ")").Trim();
    }

    public static bool ValidateInput(string input)
    {
        string inputToValidate = PrepareInput(input);

        Stack<char> stack = new();

        for (int i = 0; i < inputToValidate.Length; i++)
        {
            if (inputToValidate[i] == ')')
            {
                if (stack.Count == 0 || stack.Peek() != '(')
                    return false;
                stack.Pop();
            }

            if (inputToValidate[i] == '(')
                stack.Push(inputToValidate[i]);
        }

        if (stack.Count != 0)
            return false;

        Regex regex = new(@"[\.\+\-\*\/\^\~]{2,}");

        if (regex.IsMatch(inputToValidate))
            return false;

        return true;
    }

    public static string ToOutputPostfix(string input)
    {
        string preparedInput = PrepareInput(input);

        if (ValidateInput(preparedInput))
        {
            StringBuilder temp = new();

            StringBuilder outputPostfix = new();

            Stack<string> stack = new();

            var pos = 0;

            while (pos < preparedInput.Length)
            {
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

                if (pos < preparedInput.Length && preparedInput[pos] == '(')
                {
                    stack.Push(preparedInput[pos].ToString());
                    pos++;
                }

                if (pos < preparedInput.Length && preparedInput[pos] == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        outputPostfix.Append(stack.Pop()).Append(' ');
                    }

                    stack.Pop();
                    pos++;
                }

                if (pos < preparedInput.Length && Priority.ContainsKey(preparedInput[pos].ToString()))
                {
                    while (Priority.ContainsKey(preparedInput[pos].ToString()))
                    {
                        //вынести в отдельный метод?
                        while (stack.Count > 0 && (Priority[preparedInput[pos].ToString()] <= Priority[stack.Peek().ToString()]) && stack.Peek() != "(")
                        {
                            outputPostfix.Append(stack.Pop()).Append(' ');
                        }

                        stack.Push(preparedInput[pos].ToString());
                        pos++;
                    }
                }

                if (pos < preparedInput.Length && char.IsLetter(preparedInput[pos]))
                {
                    while (char.IsLetter(preparedInput[pos]))
                    {
                        temp.Append(preparedInput[pos]);
                        pos++;
                    }

                    while (Priority.ContainsKey(temp.ToString()) && stack.Count > 0 && (Priority[temp.ToString()] <= Priority[stack.Peek().ToString()]) && stack.Peek() != "(")
                    {
                        outputPostfix.Append(stack.Pop()).Append(' ');
                    }

                    stack.Push(temp.ToString());
                    temp.Clear();
                }
            }

            foreach (string op in stack)
                outputPostfix.Append(op).Append(' ');

            return outputPostfix.ToString().Trim();
        }

        throw new ArgumentException("Input is not valid");
    }

    public static double CalculatePostfix(string input)
    {
        string toEvaluate = ToOutputPostfix(input);

        string[] evaluate = toEvaluate.Split(' ');

        Stack<double> stack = new();

        double first;
        double second;

        for (int i = 0; i < evaluate.Length; i++)
        {
            if (double.TryParse(evaluate[i], CultureInfo.InvariantCulture, out double temp))
                stack.Push(temp);

            if (Priority.ContainsKey(evaluate[i]))
            {
                if (IsUnaryOperator(evaluate[i]))
                {
                    first = stack.Count > 0 ? stack.Pop() : 0;
                    stack.Push(EvaluateUn(evaluate[i], first));
                }
                else
                {
                    second = stack.Count > 0 ? stack.Pop() : 0;
                    first = stack.Count > 0 ? stack.Pop() : 0;
                    stack.Push(EvaluateBin(evaluate[i], first, second));
                }
            }
        }

        return stack.Pop();
    }

    internal static bool IsUnaryOperator(string input)
    {
        HashSet<string> unaryOperators = new() { "~", "sin", "cos" };

        return unaryOperators.Contains(input);
    }
}