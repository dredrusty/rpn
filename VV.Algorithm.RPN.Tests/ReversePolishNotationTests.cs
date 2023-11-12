using Xunit;
using VV.Algorithm.RPN.Resources;

namespace VV.Algorithm.RPN;

public class ReversePolishNotationTests
{
    [Theory]
    [MemberData(nameof(PrepareExpressionReturnsCorrectPreparedString_Data))]
    public void PrepareExpressionReturnsCorrectPreparedString(string expression, string expected)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = rpn.PrepareExpression(expression);

        //Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(PrepareExpressionThrowsArgumentExceptionIfInputIsNullOrEmpty_Data))]
    public void PrepareExpressionThrowsArgumentExceptionIfInputIsNullOrEmpty(string expression)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = Assert.Throws<ArgumentException>(() => rpn.PrepareExpression(expression));

        //Assert
        Assert.Contains(RPNRes.PrepareInputEmptyOrNull, result.Message);
    }

    [Theory]
    [MemberData(nameof(ValidateExpressionReturnsTrueIfExpressionCorrect_Data))]
    public void ValidateExpressionReturnsTrueIfExpressionCorrect(string expression)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = rpn.ValidateExpression(expression);

        //Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(ValidateExpressionThrowsArgumentExceptionIfParenthesisMismatch_Data))]
    public void ValidateExpressionThrowsArgumentExceptionIfParenthesisMismatch(string expression)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = Assert.Throws<ArgumentException>(() => rpn.ValidateExpression(expression));

        //Assert
        Assert.Contains(RPNRes.ValidateInputUnmatchedParenthesis, result.Message);
    }

    [Theory]
    [MemberData(nameof(ValidateExpressionThrowsArgumentExceptionIfIncorrectCharactersInExpression_Data))]
    public void ValidateExpressionThrowsArgumentExceptionIfIncorrectCharactersInExpression(string expression)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = Assert.Throws<ArgumentException>(() => rpn.ValidateExpression(expression));

        //Assert
        Assert.Contains(RPNRes.ValidateInputIncorrectCharacters, result.Message);
    }

    [Theory]
    [MemberData(nameof(ExpressionToPostfixReturnsExressionInReversePolishNotationForm_Data))]
    public void ExpressionToPostfixReturnsExressionInReversePolishNotationForm(string expression, string expected)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = rpn.ExpressionToPostfix(expression);

        //Assert
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(CalculateExpressionReturnsCorrectResult_Data))]
    public void CalculateExpressionReturnsCorrectResult(string expression, string expected)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = rpn.CalculateExpression(expression);

        //Assert
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(CalculateExpressionThrowArgumentExceptionWhenDivisionByZero_Data))]
    public void CalculateExpressionThrowArgumentExceptionWhenDivisionByZero(string expression)
    {
        //Arrange
        var rpn = new ReversePolishNotation();

        //Act
        var result = Assert.Throws<ArgumentException>(() => rpn.CalculateExpression(expression));

        //Assert
        Assert.Contains(RPNRes.CalculatePostfixHandlerSecondIsZero, result.Message);
    }

    public static IEnumerable<object[]> PrepareExpressionReturnsCorrectPreparedString_Data =>
        new List<object[]>
        {
            new object[] { "10*  {-15+19)*sin(90]-5^12,8412", "10*(~15+19)*sin(90)-5^12.8412" },
            new object[] { "-5-6*{8+  6*3} /  [5^5,8)", "~5-6*(8+6*3)/(5^5.8)" },
        };

    public static IEnumerable<object[]> PrepareExpressionThrowsArgumentExceptionIfInputIsNullOrEmpty_Data =>
        new List<object[]>
        {
            new object[] { "" },
            new object[] { null! },
        };

    public static IEnumerable<object[]> ValidateExpressionReturnsTrueIfExpressionCorrect_Data =>
        new List<object[]>
        {
            new object[] { "10*  {-15+19)*sin(90]-5^12,8412" },
            new object[] { "-5-6*{8+  6*3} /  [5^5,8)" },
        };

    public static IEnumerable<object[]> ValidateExpressionThrowsArgumentExceptionIfParenthesisMismatch_Data =>
        new List<object[]>
        {
            new object[] { "10*  {-15+19)*sin)90]-5^12,8412" },
            new object[] { "-5-6*{8+  6*3} /  [5^5,8) + (8" },
        };

    public static IEnumerable<object[]> ValidateExpressionThrowsArgumentExceptionIfIncorrectCharactersInExpression_Data =>
        new List<object[]>
        {
            new object[] { "10*/ {-15+19)*sin(90]-5^12,8412" },
            new object[] { "-5-6*{8+  6*3} /  [5^5,8) + sinsin(8)" },
            new object[] { "-5-6*{8+  6*3} /  [5^5,.8) + sin(8)" },
            new object[] { "10* {-15+19)*sin(90]-5^+12,8412" },
        };

    public static IEnumerable<object[]> ExpressionToPostfixReturnsExressionInReversePolishNotationForm_Data =>
        new List<object[]>
        {
            new object[] { "10*(-15+19)*sin(90)-5^12.8412", "10 15 ~ 19 + * 90 sin * 5 12.8412 ^ -" },
            new object[] { "-5-6*(8+6*3)/(5^5.8)+sin(8)", "5 ~ 6 8 6 3 * + * 5 5.8 ^ / - 8 sin +" },
            new object[] { "(5+16)^3-sin(cos(45))+14.254", "5 16 + 3 ^ 45 cos sin - 14.254 +" },
            new object[] { "sin(80)/cos(35)+5.4/(-12+7)^3", "80 sin 35 cos / 5.4 12 ~ 7 + 3 ^ / +" },
        };

    public static IEnumerable<object[]> CalculateExpressionReturnsCorrectResult_Data =>
        new List<object[]>
        {
            new object[] { "10*(-15+19)*sin(90)-5^12.8412", "-945395657,7878376" },
            new object[] { "-5-6*(8+6*3)/(5^5.8)+sin(8)", "-4,024416974316647" },
            new object[] { "(5+16)^3-sin(cos(45))+14.254", "9274,752508396681" },
            new object[] { "sin(80)/cos(35)+5.4/(-12+7)^3", "1,0566088157933546" },
        };

    public static IEnumerable<object[]> CalculateExpressionThrowArgumentExceptionWhenDivisionByZero_Data =>
        new List<object[]>
        {
            new object[] { "10/(5-5)" },
            new object[] { "5/((14+2)-16)" },
            new object[] { "17/0" },
            new object[] { "(sin(95) + 2^6) / 0 + 12,5748*3" },
        };
}