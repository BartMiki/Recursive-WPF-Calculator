using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_WPF_Calculator
{
    public class Equation
    {
        private Equation leftEquation;
        private Equation rightEquation;
        private MathematicOperation operation;
        private string equation;

        public static string EquationResult(string equationString)
        {
            Equation equation = new Equation(equationString);
            string result = equation.CalculateEquation().ToString();
            return result;
        }

        private Equation(string equationString)
        {
            equation = equationString;
            equation = TrimExcessBrackets(equation);
            CreateEquation();
        }

        private void CreateEquation()
        {
            int? operatorIndex = 0;
            var operationsInOrder = new MathematicOperation[] {
                MathematicOperation.Addition,
                MathematicOperation.Substraction,
                MathematicOperation.Multiplication,
                MathematicOperation.Division,
                MathematicOperation.Power
            };

            foreach (MathematicOperation mathOperation in operationsInOrder)
            {
                operatorIndex = IndexOfOperator(equation, mathOperation);
                if (operatorIndex != null)
                {
                    int substringLeght = operatorIndex.Value;
                    leftEquation = new Equation(equation.Substring(0, substringLeght));
                    substringLeght += 1;
                    rightEquation = new Equation(equation.Substring(substringLeght));
                    operation = mathOperation;
                    return;
                }
            }

            leftEquation = null;
            rightEquation = null;
        }

        private string TrimExcessBrackets(string equationString)
        {
            while (equationString[0] == '(')
            {
                int closingBracketIndex = IndexOfClosingBracket(equationString, 1);
                if (closingBracketIndex == equationString.Length - 1)
                {
                    equationString = equationString.Substring(1, (equationString.Length - 2));
                }
                else
                {
                    return equationString;
                }
            }
            return equationString;
        }

        private int? IndexOfOperator(string equation, MathematicOperation operation)
        {
            char operationChar = (char)operation;
            int charIndex = 0;
            while (charIndex < equation.Length)
            {
                if (equation[charIndex] == '(')
                {
                    charIndex++;
                    charIndex = IndexOfClosingBracket(equation, charIndex);
                    charIndex++;
                }
                else if (equation[charIndex] == operationChar)
                {
                    return charIndex;
                }
                else
                {
                    charIndex++;
                }
            }
            return null;
        }

        private int IndexOfClosingBracket(string equation, int index)
        {
            while (index < equation.Length)
            {
                if (equation[index] == '(')
                {
                    index++;
                    index = IndexOfClosingBracket(equation, index);
                    index++;
                }
                else if (equation[index] == ')')
                {
                    return index;
                }
                else
                {
                    index++;
                }
            }
            throw new InvalidOperationException();
        }

        private double CalculateEquation()
        {
            if (leftEquation == null || rightEquation == null)
            {
                if(MainWindow.constantDeclarations.ContainsKey(equation))
                {
                    return MainWindow.constantDeclarations[equation];
                }
                return Convert.ToDouble(equation);
            }
            else
            {
                switch (operation)
                {
                    case MathematicOperation.Addition:
                        return leftEquation.CalculateEquation() + rightEquation.CalculateEquation();
                    case MathematicOperation.Substraction:
                        return leftEquation.CalculateEquation() - rightEquation.CalculateEquation();
                    case MathematicOperation.Multiplication:
                        return leftEquation.CalculateEquation() * rightEquation.CalculateEquation();
                    case MathematicOperation.Division:
                        return leftEquation.CalculateEquation() / rightEquation.CalculateEquation();
                    case MathematicOperation.Power:
                        return Math.Pow(leftEquation.CalculateEquation(), rightEquation.CalculateEquation());
                }
            }
            return 0;
        }
    }
}