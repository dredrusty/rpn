
namespace VV.Algorithm.RPN;

/// <summary>
/// Represents a class for Reverse Polish Notation calculations.
/// Provides methods for working with the input expression: <see cref="PrepareExpression(string)"/>, <see cref="ValidateExpression(string)"/>, <see cref="ExpressionToPostfix(string)"/>, <see cref="CalculateExpression(string)"/>.
/// Methods call handlers that implement the Chain of Responsibility pattern.
/// </summary>
public sealed class ReversePolishNotation
{
    /// <summary>
    /// Represents the delegate for handling events related to evaluating expressions in Reverse Polish Notation (RPN).
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An instance of <see cref="EventArgsInCalculate"/> containing event data.</param>
    public delegate void ReversePolishNotationEventHandler(object sender, EventArgsInCalculate e);

    /// <summary>
    /// Occurs when a calculation step is performed in <see cref="CalculatePostfixHandler"/> during the evaluation of an expression in Reverse Polish Notation (RPN).
    /// </summary>
    public event ReversePolishNotationEventHandler? OnCalculateIteration;

    /// <summary>
    /// A dictionary that associates operators and functions with their precedence levels, used for parsing expressions.
    /// </summary>
    internal readonly static Dictionary<string, int> Priority = new()
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

    private readonly object lockProceed = new (); 

    // Instances of handler classes, that implements interface IInputHandler, for various stages of expression processing.
    internal readonly PrepareInputHandler prepare = new(isResetNeeded : true);
    internal readonly ValidateInputHandler validate = new(isResetNeeded: true);
    internal readonly ToOutputPostfixHandler postfix = new(isResetNeeded: true);
    internal readonly CalculatePostfixHandler calc = new(isResetNeeded: true);

    /// <summary>
    /// Initializes a new instance of the ReversePolishNotation class.
    /// </summary>
    public ReversePolishNotation()
    {

    }

    /// <summary>
    /// Calls <see cref="PrepareInputHandler.Handle(string)"/> that takes an expression as input and returns a string with the expression prepared for further processing.
    /// In this stage, whitespaces are removed, square and curly brackets are replaced with parentheses, and commas are replaced with dots as the decimal separator. 
    /// The unary minus is replaced with a tilde.
    /// </summary>
    /// <param name="expression">Inputed expression as string that should be prepared for further processing.</param>
    /// <returns>Prepared expression as string for further processing.</returns>
    public string PrepareExpression(string expression)
    {
        lock (lockProceed)
        { 
            return prepare.Handle(expression); 
        }     
    }

    /// <summary>
    /// Constructs a chain from <see cref="PrepareInputHandler"/> and <see cref="ValidateInputHandler"/>, where the input expression goes through preparation and, after it, validation.
    /// At this stage, it disallows sequences of two or more consecutive mathematical operators, functions, and decimal separators.
    /// It also checks whether the parentheses match.
    /// </summary>
    /// <param name="expression">Input expression as a string that should be prepared and validated for further processing.</param>
    /// <returns>Returns true if the expression is validated; otherwise, it throws an <see cref="ArgumentException"/>.</returns>
    public bool ValidateExpression(string expression)
    {
        lock (lockProceed)
        {
            prepare
            .SetNext(validate);

            prepare.Handle(expression);
        }

        return true;
    }

    /// <summary>
    /// Constructs a chain using <see cref="PrepareInputHandler"/>, <see cref="ValidateInputHandler"/>, and <see cref="ToOutputPostfixHandler"/> 
    /// where the input expression undergoes preparation, validation, and conversion to Reverse Polish Notation.
    /// </summary>
    /// <param name="expression">Input expression as a string that should be prepared, validated and converted to Reverse Polish Notation for further processing.</param>
    /// <returns>The input expression in Reverse Polish Notation as a string for further processing.</returns>
    public string ExpressionToPostfix(string expression)
    {
        lock (lockProceed)
        {
            prepare
            .SetNext(validate)
            .SetNext(postfix);

            return prepare.Handle(expression);
        }       
    }

    /// <summary>
    /// Constructs a chain using <see cref="PrepareInputHandler"/>, <see cref="ValidateInputHandler"/>, <see cref="ToOutputPostfixHandler"/> and <see cref="CalculatePostfixHandler"/>
    /// where expression undergoes preparation, validation, conversion to Reverse Polish Notation, and calculation.
    /// </summary>
    /// <param name="expression">Input expression as a string that should be prepared, validated, converted to Reverse Polish Notation and calculated.</param>
    /// <returns>The result of the calculated expression as a string.</returns>
    public double CalculateExpression(string expression)
    {
        lock (lockProceed)
        {
            calc.OnCalculateStep += (sender, e) =>
            {
                OnCalculateIteration?.Invoke(sender, e);
            };

            prepare
            .SetNext(validate)
            .SetNext(postfix)
            .SetNext(calc);

            var result =  prepare.Handle(expression);

            return Convert.ToDouble(result);
        }        
    }
}