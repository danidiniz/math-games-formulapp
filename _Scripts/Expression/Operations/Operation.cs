using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Operation {

	protected System.Text.StringBuilder sb;

	// Lista com todas questões erradas.
	private List<string> wrongQuestions;

	// Quantidade de questões que acertou.
	private int quantityCorrectQuestions;
	// Quantidade de questões que errou.
	private int quantityWrongQuestions;
	// Porcentagem de questões que acertou. (fazer depois)
	private int percentageCorrectQuestions;

	void Start(){
		//wrongQuestions = new List<string> ();
		if (sb == null) {
			sb = new System.Text.StringBuilder();
		}
	}

	public int QuantityCorrectQuestions{
		get{ 
			return quantityCorrectQuestions;
		}
		set{ 
			quantityCorrectQuestions = value;
		}
	}

	public int QuantityWrongQuestions{
		get{ 
			return quantityCorrectQuestions;
		}
		set{ 
			quantityCorrectQuestions = value;
		}
	}

	public abstract Expression RandomExpression (int difficulty, int difficultyNumberAlgarismRange);

	protected abstract int RandomNumber (int numberOfAlgarism);
}
