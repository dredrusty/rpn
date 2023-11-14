using Xunit;

namespace VV.Algorithm.RPN;

public class EventArgsEvaluateRPNTests
{
    [Theory]
    [MemberData(nameof(CalculatePostfixRisesOnCalculateStep_Data))]
    public void CalculatePostfixRisesOnCalculateStep(string expression, List<string> expOperators, List<string> expFirst, List<string> expSecond, List<string> expTrigger)
    {
        //Arrange
        var rpn = new ReversePolishNotation();
        List<string> mathOperators = new();
        List<string> first = new();
        List<string> second = new();
        List<string> triggerMethod = new();

        //Act
        rpn.OnCalculateIteration += (s, e) =>
        {
            mathOperators.Add(e.OperationInfo.MathOperator!);
            first.Add(e.OperationInfo.FirstOperand.ToString());
            second.Add(e.OperationInfo.SecondOperand.ToString());
            triggerMethod.Add(e.OperationInfo.TriggerMethod!);
        };

        rpn.CalculateExpression(expression);

        //Assert
        Assert.Equal(expOperators, mathOperators);
        Assert.Equal(expFirst, first);
        Assert.Equal(expSecond, second);
        Assert.Equal(expTrigger, triggerMethod);
    }

    public static IEnumerable<object[]> CalculatePostfixRisesOnCalculateStep_Data =>
        new List<object[]>
        {
            new object[] { "10*12,8412", new List<string> {"*"}, new List<string> {"10" }, new List<string> {12.8412.ToString() }, new List<string> {"Handle"}  },
            new object[] { "10/(5-3)", new List<string> {"-", "/" }, new List<string> { "5", "10" }, new List<string> { "3", "2" }, new List<string>{ "Handle", "Handle" } },
        };
}