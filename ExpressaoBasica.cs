using System;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp {
	
	public class ExpressaoBasica {

		// Atributos
		private string numero1, numero2, operador; // Os dois números a serem gerados, mais o operador
		private int[] dificuldade; // A dificuldade da expressão, um vetor de 3 inteiros
		private string resultado, expressao; // Strings representando o resultado da expressão e a própria expressão

		// Métodos privados 
		private int gerarNumeroAleatorio(int intervalo1, int intervalo2){

			// Uso especificamente System.Random pois quero usar o Random o System, pois o Unity também tem um objeto Random
			System.Random aleatorio = new System.Random (Guid.NewGuid().GetHashCode()); // Muda o seed a cada execução, para não repetir números aleatórios
				return aleatorio.Next (intervalo1, intervalo2 + 1);
		}

		private string gerarOperador(int numOperador){
			if (numOperador == 1)
				return " + ";
			else if (numOperador == 2)
				return " - ";

			return "";

		}

		private int[] gerarDificuldade(int qtdExpressoes){

			int[] dificuldade = new int[3]; // Um vetor de 3 posições: Intervalo1 e Intervalo2 dos números, e o número do operador

			// Dependendo do modo de Jogo, a dificuldade os números são gerados de uma maneira um pouco diferente
			switch(Jogador.getJogoAtual()){
			case "precisaoArcade":
				if (qtdExpressoes < 21) {
					// Dificuldade 1: Números de 0 a 10, com 2 operações: somar e subtrair
					dificuldade [0] = 0;
					dificuldade [1] = 10;
					dificuldade [2] = 2;
				} else if (qtdExpressoes >= 21 && qtdExpressoes < 36) {
					// Dificuldade 2: Números de -15 a 15, com 2 operações: somar e subtrair
					dificuldade [0] = -15;
					dificuldade [1] = 15;
					dificuldade [2] = 2;
				}
				break;

			case "precisaoTimeAttack":
				if (qtdExpressoes < 15) {
					// Dificuldade 1: Números de 0 a 10, com 2 operações: somar e subtrair
					dificuldade [0] = -5;
					dificuldade [1] = 5;
					dificuldade [2] = 2;
				} else if (qtdExpressoes >= 15 && qtdExpressoes < 41) {
					// Dificuldade 2: Números de -15 a 15, com 2 operações: somar e subtrair
					dificuldade [0] = -15;
					dificuldade [1] = 15;
					dificuldade [2] = 2;
				}
				break;
			}

			return dificuldade;
			
		}

		private string gerarResultado(int num1, int num2, int operador){
			int resultado = 0;
	
			if (operador == 1) {
				resultado = num1 + num2;
			} else if (operador == 2) {
				resultado = num1 - num2;
			}
				
			return resultado.ToString();
		}

		// Construtor
		public ExpressaoBasica (int qtdExpressoes) {
			// Gerando a dificuldade, através da quantidade de expressões feitas pelo jogador
			this.dificuldade = gerarDificuldade (qtdExpressoes);
			// Gerando os números aleatórios, junto com o operador, inicialmente na forma inteira
			int num1 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
			int num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
			int op = gerarNumeroAleatorio(1, this.dificuldade[2]);
			// Gerando o resultado, já na forma de string
			this.resultado = gerarResultado (num1, num2, op); 
			// Convertendo os números para string, se forem negativos colocando parênteses
			this.numero1 = num1.ToString();
			this.numero2 = num2.ToString();

			if (num1 < 0)
				this.numero1 = "(" + this.numero1 + ")";
			if (num2 < 0)
				this.numero2 = "(" + this.numero2 + ")";
			
			// Gerando a string do operador
			this.operador = gerarOperador(op);

			/*print("Numero1: " + this.numero1);
			print("Numero2: " + this.numero2);
			print("OP: " + op);
			print ("resposta: " + this.resultado);*/

			this.expressao = this.numero1 + " " + this.operador + " " + this.numero2;

		}

		// Getters (sim, nada de Setters)
		public string Expressao { 
			get { return this.expressao; }
		}

		public string Resultado {
			get { return this.resultado; }
		}

		// Métodos públicos
		public bool checarResposta(string resposta){
			return this.resultado.Equals (resposta);
		}
	}
}

