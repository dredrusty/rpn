[![NuGet stable version](https://badgen.net/nuget/v/VV.Algorithm.RPN)](https://www.nuget.org/packages/VV.Algorithm.RPN)
[![Build Status](https://dev.azure.com/rustyvik/ReversePolishNotation/_apis/build/status%2Freversepolishnotation.build?branchName=develop)](https://dev.azure.com/rustyvik/ReversePolishNotation/_build/latest?definitionId=22&branchName=develop)
[![Azure DevOps tests](https://img.shields.io/azure-devops/build/rustyvik/ReversePolishNotation/21)](https://dev.azure.com/rustyvik/ReversePolishNotation/_build/latest?definitionId=21&branchName=develop)

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=style-flat&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=style-flat&logo=c-sharp&logoColor=white)


# Reverse Polish Notation (RPN)
This project provides a robust implementation for evaluating mathematical expressions using Reverse Polish Notation (RPN). The solution includes classes for parsing, validating, converting, and calculating RPN expressions, with event handling for each calculation step.

## Installation

	dotnet add package VV.Algorithm.RPN --version 1.0.0-CI-20240523-194601
    
## Methods

- **PrepareExpression(string):** Prepares expression by removing whitespaces, replacing brackets, and handling unary minus signs.
- **ValidateExpression(string):** Validates expression for correct syntax and structure.
- **ExpressionToPostfix(string):** Converts infix expression to postfix (RPN) format.
- **CalculateExpression(string):** Evaluates RPN expression and supports event handling for each calculation step.

Every method gets and returns type `string`.

This version supports the following mathematical operations and functions:

- **Binary Operators:** +, -, *, /, ^.
- **Unary Operators:** ~ (unary minus).
- **Functions:** sin, cos.

Parentheses precedence is taken into account.

## Usage
To use this library, create an instance of ReversePolishNotation and call its methods to prepare, validate, convert, and calculate expressions.
```csharp
var rpnCalculator = new ReversePolishNotation();

// Prepare the expression
string preparedExpression = rpnCalculator.PrepareExpression("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3");

// Validate the expression
bool isValid = rpnCalculator.ValidateExpression(preparedExpression);

// Convert to postfix notation
string postfixExpression = rpnCalculator.ExpressionToPostfix(preparedExpression);

// Calculate the result
string result = rpnCalculator.CalculateExpression(preparedExpression);

Console.WriteLine($"Result: {result}");
```

You do not need to call each method separately to get the result. You can directly call the CalculateExpression method, which internally calls all necessary steps:
```csharp
var rpnCalculator = new ReversePolishNotation();
string result = rpnCalculator.CalculateExpression("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3");
Console.WriteLine($"Result: {result}");
```

## Events
The `ReversePolishNotation` class supports events that are triggered during each calculation step. This feature is particularly useful for tracking the progress of the calculation, debugging, or implementing custom logging.

Subscribe to the OnCalculateIteration event to get notified each time a calculation step is performed.
```csharp
void PrintDetails(object sender, EventArgsInCalculate e)
{
    Console.WriteLine($"Operation: {e.OperationInfo.MathOperator}, Operands: {e.OperationInfo.FirstOperand} {e.OperationInfo.SecondOperand}, Result: {e.OperationInfo.StepResult}");
}

var rpn = new ReversePolishNotation();

string evaluate = "2.5/8 + 2^3";

rpn.OnCalculateIteration += PrintDetails;

var result = res.CalculateExpression(evaluate);

//Console output:
Operation: /, Operands: 2,5 8, Result: 0,3125
Operation: ^, Operands: 2 3, Result: 8
Operation: +, Operands: 0,3125 8, Result: 8,3125 
```

#### Tests
Project covered by xUnit tests.
