using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Expression {

	private int difficutyOfExpression;
	private string expressionText;
	private string answerOfExpression;

	public int DifficutyOfExpression{
		get{ 
			return difficutyOfExpression;
		}
		set{ 
			difficutyOfExpression = value;
		}
	}

	public string ExpressionText{
		get{ 
			return expressionText;
		}
		set{ 
			expressionText = value;
		}
	}

	public string AnswerOfExpression{
		get{ 
			return answerOfExpression;
		}
		set{ 
			answerOfExpression = value;
		}
	}


	protected static int evaluate(string expression)
		{
			char[] tokens = expression.ToCharArray();

			// Stack for numbers: 'values'
			Stack<int> values = new Stack<int>();

			// Stack for Operators: 'ops'
			Stack<char> ops = new Stack<char>();

			for (int i = 0; i < tokens.Length; i++)
			{
				// Current token is a whitespace, skip it
				if (tokens[i] == ' ')
					continue;

				// Current token is a number, push it to stack for numbers
				if (tokens [i] >= '0' && tokens [i] <= '9') {
					StringBuilder sbuf = new StringBuilder ();
					// There may be more than one digits in number
					while (i < tokens.Length && tokens [i] >= '0' && tokens [i] <= '9')
						sbuf.Append (tokens [i++]); /* Adiciona o tokens[i] e já incrementa i */
					values.Push (int.Parse (sbuf.ToString ()));
					i--; /* Decrementa i porque esse while está incrementando 1, e ao final do for vai incrementar mais 1, totalizando 2 e resultando em erro */
				}

				// Current token is an opening brace, push it to 'ops'
				else if (tokens [i] == '(')
					ops.Push (tokens [i]);

				// Closing brace encountered, solve entire brace
				else if (tokens [i] == ')') {
					while (ops.Peek () != '(')
						values.Push (applyOp (ops.Pop (), values.Pop (), values.Pop ()));
					ops.Pop ();
					Debug.Log ("Calculou parenteses: " + values.Peek());
					Debug.Log ("Topo de ops: " + ops.Peek());

					// Após calcular o parênteses, verifico se o topo é uma potência
					if (ops.Count > 0) {
						if (ops.Peek () == '^') {
							// Pego o value anterior ao topo (penultimo de value)
							// e aplico a potencia
							if (values.Count > 0) {
								// Simplesmente preciso dar values.Pop duas vezes
								// Dessa forma passo o primeiro (que está no topo) como potencia,
								// e o segundo como o valor a ser potencializado.
								values.Push (applyOp ('^', values.Pop (), values.Pop ()));	
								ops.Pop ();
								Debug.Log ("Calculou potencia: " + values.Peek());
							}
						}
					}
				}

				// Current token is an operator.
				else if (tokens [i] == '+' || tokens [i] == '-' ||
				         tokens [i] == '*' || tokens [i] == '/') {
					// While top of 'ops' has same or greater precedence to current
					// token, which is an operator. Apply operator on top of 'ops'
					// to top two elements in values stack
					while ((ops.Count > 0) && hasPrecedence (tokens [i], ops.Peek ()))
						values.Push (applyOp (ops.Pop (), values.Pop (), values.Pop ()));

					// Push current token to 'ops'.
					ops.Push (tokens [i]);
				} 

				// Em caso de potencia, simplesmente pego o topo de value 
				// e potencializo com o proximo numero no token
				else if (tokens [i] == '^') {
					// Se a potência estiver entre parênteses,
					// adiciono AS OPERAÇÕES POTENCIA E PARENTESES em op.
					if(tokens[++i] == '('){
						ops.Push(tokens[--i]); // potência
						ops.Push(tokens[++i]); // parênteses
					}
					// Se a potência não estiver entre parênteses
					else if (++i < tokens.Length) { // (Excesso de precaução)
						if (tokens [i] >= '0' && tokens [i] <= '9') { // (Excesso de precaução [2])
							StringBuilder sbuf = new StringBuilder ();
							sbuf.Append (tokens[i]);
							// Passo o '^' manualmente porque ela não depende de nenhuma operação anterior,
							// e caso ela esteja potencializando um parênteses inteiro (2+3)^2, ela irá potencializar
							// o RESULTADO do parenteses, que no caso estará no topo de value.
							values.Push(applyOp ('^', int.Parse(sbuf.ToString()), values.Pop()));
						}
					}
				}

			}

			// Entire expression has been parsed at this point, apply remaining
			// ops to remaining values
			while (ops.Count > 0)
				values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));

			// Top of 'values' contains result, return it
			return values.Pop();
		}

		// Returns true if 'op2' has higher or same precedence as 'op1',
		// otherwise returns false.
		public static bool hasPrecedence(char op1, char op2)
		{
			if (op2 == '(' || op2 == ')')
				return false;
			if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
				return false;
			else
				return true;
		}

		// A utility method to apply an operator 'op' on operands 'a' 
		// and 'b'. Return the result.
		public static int applyOp(char op, int b, int a)
		{
			switch (op)
			{
			case '+':
				return a + b;
			case '-':
				return a - b;
			case '*':
				return a * b;
			case '/':
				if (b == 0) {
					Debug.Log ("Cannot divide by zero");
					return 0;
				}	
				return a / b;
			case '^':
				// b = potencia
				// a = value.Pop()
				if (b == 0)
					return 1;
				return (int)Mathf.Pow (a, b);
			}
			return 0;
		}
	
}