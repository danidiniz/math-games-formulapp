using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soma : Operation {

	public override Expression RandomExpression(int difficulty, int difficultyNumberAlgarismRange){
		Expression exp = new Expression ();

		exp.DifficutyOfExpression = difficulty;

		string expText = "";

		// Níveis de dificuldade da expressão e dificuldade da quantidade de algarismos
		/*
		 * 1:	4 + 5
		 * 2:	22 + 3 + 42
		 * 3:	3 + 12 + 99
		 * >4:	2 + 65 + 21 + 1 + 54 + 3... (máximo de 8)
		 * 
		 * 1 && número de 1 algarismo:	1 + 5
		 * 1 && número de 2 algarismos:	34 + 28
		 * 1 && número de 3 algarismos:	132 + 462
		 * 2 && número de 1 algarismo:	1 + 5 + 4
		 * 2 && número de 2 algarismo:	43 + 64 + 89
		 * etc
		 */

		if (difficulty <= 4) {
			for (int i = 0; i < difficulty; i++) {
				expText += RandomNumber(difficultyNumberAlgarismRange).ToString();
				if (i < difficulty - 1) {
					expText += " + ";
				}
			}
		}
		else if (difficulty > 4) {
		}

		exp.ExpressionText = expText;

		return exp;
	}

	protected override int RandomNumber (int numberOfAlgarism){
		if (numberOfAlgarism == 1) {
			return Random.Range (1, 10);
		}
		else if (numberOfAlgarism == 2) {
			return Random.Range (10, 100);
		}
		else if (numberOfAlgarism == 3) {
			return Random.Range (100, 1000);
		}
		return 0;
	}

}
