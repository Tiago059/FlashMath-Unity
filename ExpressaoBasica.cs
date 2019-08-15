using System;

namespace AssemblyCSharp {
	
	public class ExpressaoBasica {

		// Atributos
		private string numero1, numero2, operador; // Os dois números a serem gerados, mais o operador
		private int[] dificuldade; // A dificuldade da expressão, um vetor de 3 inteiros
		private string resultado, expressao; // Strings representando o resultado da expressão e a própria expressão
		
		// Jogo e Dificuldade atual do jogador;
		private string jogoAtual, dificAtual;

		// Métodos privados 
		private int gerarNumeroAleatorio(int intervalo1, int intervalo2){
			Random aleatorio = new Random (Guid.NewGuid().GetHashCode()); // Muda o seed a cada execução, para não repetir números aleatórios
			return aleatorio.Next (intervalo1, intervalo2 + 1);
		}
		
		// Gera uma string contendo um operador da operação (HUMMM)
		private string gerarOperador(int numOperador){
			switch (numOperador){
				case 1: return " + "; 
				case 2: return " - "; 
				case 3: return " x "; 
				case 4: return " / "; 
				default: return " error ";
			}
		}

		/* Essa função recebe a quantidade de expressões e retorna um vetor de dificuldade baseada nesta
    	quantidade e na dificuldade escolhida pelo jogador. */
		private static int[] gerarDificuldade(int qtdExpressoes){
			
			// Um vetor de 3 posições: Intervalo1 e Intervalo2 dos números, e o número do operador
			int[] dificuldade = new int[3]; 

			// Dependendo do modo de Jogo e da dificuldade, a dificuldade os números são gerados de uma maneira um pouco diferente
			switch(jogoAtual){
				case "precisaoArcade":
					switch(dificAtual){
						// *** Dificuldade 1: Kids (Easy) *** //
						case "Kids":
							if (qtdExpressoes < 8) {
								dificuldade[0] = 0;
								dificuldade[1] = 3; 
								dificuldade[2] = 1; 
							} 
							else if (qtdExpressoes >= 8 && qtdExpressoes < 20) {
								dificuldade[0] = 0;
								dificuldade[1] = 6;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 20 && qtdExpressoes < 34) {
								dificuldade[0] = 0;
								dificuldade[1] = 9;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 34 && qtdExpressoes < 51) {
								dificuldade[0] = 0;
								dificuldade[1] = 12;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 51 && qtdExpressoes < 69) {
								dificuldade[0] = 0;
								dificuldade[1] = 4;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 69 && qtdExpressoes < 84) {
								dificuldade[0] = 0;
								dificuldade[1] = 8;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 84 && qtdExpressoes < 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 12;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 15;
								dificuldade[2] = 2;
							}
							break;
						// *** Dificuldade 2: Beginner (Medium) *** //
						case "Beginner":
							if (qtdExpressoes < 7) {
								dificuldade[0] = 0;
								dificuldade[1] = 5;
								dificuldade[2] = 1; 
							} 
							else if (qtdExpressoes >= 7 && qtdExpressoes < 15) {
								dificuldade[0] = 0;
								dificuldade[1] = 10;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 15 && qtdExpressoes < 29) {
								dificuldade[0] = 0;
								dificuldade[1] = 15;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 29 && qtdExpressoes < 36) {
								dificuldade[0] = 0;
								dificuldade[1] = 5;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 36 && qtdExpressoes < 48) {
								dificuldade[0] = 0;
								dificuldade[1] = 10;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 48 && qtdExpressoes < 71) {
								dificuldade[0] = 0;
								dificuldade[1] = 15;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 71 && qtdExpressoes < 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 20;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 100) {
								dificuldade [0] = 0;
								dificuldade [1] = 25;
								dificuldade [2] = 2;
							}
							break;
						// *** Dificuldade 3: Experient (Hard) *** //
						case "Experient":
							if (qtdExpressoes < 7) {
								dificuldade[0] = 0;
								dificuldade[1] = 10; 
								dificuldade[2] = 2; 
							} else if (qtdExpressoes >= 7 && qtdExpressoes < 15) {
								dificuldade[0] = -15;
								dificuldade[1] = 15;
								dificuldade[2] = 2;
							} else if (qtdExpressoes >= 15 && qtdExpressoes < 29) {
								dificuldade[0] = -25;
								dificuldade[1] = 25;
								dificuldade[2] = 3;
							} else if (qtdExpressoes >= 29 && qtdExpressoes < 36) {
								dificuldade[0] = 0;
								dificuldade[1] = 10;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 36 && qtdExpressoes < 48) {
								dificuldade[0] = -15;
								dificuldade[1] = 15;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 48 && qtdExpressoes < 71) {
								dificuldade[0] = -35;
								dificuldade[1] = 35;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 71 && qtdExpressoes < 100) {
								dificuldade[0] = -70;
								dificuldade[1] = 70;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 100) {
								dificuldade[0] = -100;
								dificuldade[1] = 100;
								dificuldade[2] = 4;
							}
							break;

						// *** Dificuldade 4: Challenger (Expert) *** //
						case "Challenger":
							if (qtdExpressoes < 3) {
								dificuldade[0] = 0;
								dificuldade[1] = 10; 
								dificuldade[2] = 2; 
							} else if (qtdExpressoes >= 3 && qtdExpressoes < 10) {
								dificuldade[0] = 0
								dificuldade[1] = 50;
								dificuldade[2] = 2;
							} else if (qtdExpressoes >= 10 && qtdExpressoes < 30) {
								dificuldade[0] = -75;
								dificuldade[1] = 75;
								dificuldade[2] = 3;
							} else if (qtdExpressoes >= 30 && qtdExpressoes < 50) {
								dificuldade[0] = -100;
								dificuldade[1] = 100;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 50 && qtdExpressoes < 66) {
								dificuldade[0] = -250;
								dificuldade[1] = 250;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 66 && qtdExpressoes < 89) {
								dificuldade[0] = -500;
								dificuldade[1] = 500;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 89 && qtdExpressoes < 100) {
								dificuldade[0] = -666;
								dificuldade[1] = 666;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 100) {
								dificuldade[0] = -999;
								dificuldade[1] = 999;
								dificuldade[2] = 4;
							}
							break;
					}
					break;
				case "precisaoTimeAttack":
					switch(dificAtual){
						// *** Dificuldade 1: Kids (Easy) *** //
						case "Kids":
							if (qtdExpressoes < 10) {
								dificuldade[0] = 0;
								dificuldade[1] = 3; 
								dificuldade[2] = 1; 
							} 
							else if (qtdExpressoes >= 10 && qtdExpressoes < 20) {
								dificuldade[0] = 0;
								dificuldade[1] = 5;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 20 && qtdExpressoes < 30) {
								dificuldade[0] = 0;
								dificuldade[1] = 7;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 30 && qtdExpressoes < 50) {
								dificuldade[0] = 0;
								dificuldade[1] = 9;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 50 && qtdExpressoes < 70) {
								dificuldade[0] = 0;
								dificuldade[1] = 9;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 70 && qtdExpressoes < 80) {
								dificuldade[0] = 0;
								dificuldade[1] = 11;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 80 && qtdExpressoes < 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 13;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 15;
								dificuldade[2] = 2;
							}
							break;
						// *** Dificuldade 2: Beginner (Medium) *** //
						case "Beginner":
							if (qtdExpressoes < 7) {
								dificuldade[0] = 0;
								dificuldade[1] = 3; 
								dificuldade[2] = 1; 
							} 
							else if (qtdExpressoes >= 7 && qtdExpressoes < 15) {
								dificuldade[0] = 0;
								dificuldade[1] = 7;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 15 && qtdExpressoes < 29) {
								dificuldade[0] = 0;
								dificuldade[1] = 10;
								dificuldade[2] = 1;
							} 
							else if (qtdExpressoes >= 29 && qtdExpressoes < 36) {
								dificuldade[0] = 0;
								dificuldade[1] = 5;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 36 && qtdExpressoes < 48) {
								dificuldade[0] = 0;
								dificuldade[1] = 9;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 48 && qtdExpressoes < 71) {
								dificuldade[0] = 0;
								dificuldade[1] = 12;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 71 && qtdExpressoes < 100) {
								dificuldade[0] = 0;
								dificuldade[1] = 20;
								dificuldade[2] = 2;
							} 
							else if (qtdExpressoes >= 100) {
								dificuldade [0] = 0;
								dificuldade [1] = 25;
								dificuldade [2] = 2;
							}
							break;
						// *** Dificuldade 3: Experient (Hard) *** //
						case "Experient":
							if (qtdExpressoes < 7) {
								dificuldade[0] = -5;
								dificuldade[1] = 5; 
								dificuldade[2] = 2; 
							} else if (qtdExpressoes >= 7 && qtdExpressoes < 15) {
								dificuldade[0] = -10;
								dificuldade[1] = 10;
								dificuldade[2] = 2;
							} else if (qtdExpressoes >= 15 && qtdExpressoes < 29) {
								dificuldade[0] = -10;
								dificuldade[1] = 10;
								dificuldade[2] = 3;
							} else if (qtdExpressoes >= 29 && qtdExpressoes < 36) {
								dificuldade[0] = -20;
								dificuldade[1] = 20;
								dificuldade[2] = 3;
							} else if (qtdExpressoes >= 36 && qtdExpressoes < 48) {
								dificuldade[0] = -30;
								dificuldade[1] = 30;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 48 && qtdExpressoes < 71) {
								dificuldade[0] = -50;
								dificuldade[1] = 50;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 71 && qtdExpressoes < 100) {
								dificuldade[0] = -75;
								dificuldade[1] = 75;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 100) {
								dificuldade[0] = -100;
								dificuldade[1] = 100;
								dificuldade[2] = 4;
							}
							break;

						// *** Dificuldade 4: Challenger (Expert) *** //
						case "Challenger":
							if (qtdExpressoes < 3) {
								dificuldade[0] = -999;
								dificuldade[1] = 999;
								dificuldade[2] = 1;
							} else if (qtdExpressoes >= 3 && qtdExpressoes < 10) {
								dificuldade[0] = 0
								dificuldade[1] = 10;
								dificuldade[2] = 2;
							} else if (qtdExpressoes >= 10 && qtdExpressoes < 30) {
								dificuldade[0] = -25;
								dificuldade[1] = 25;
								dificuldade[2] = 3;
							} else if (qtdExpressoes >= 30 && qtdExpressoes < 50) {
								dificuldade[0] = -55;
								dificuldade[1] = 100;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 50 && qtdExpressoes < 66) {
								dificuldade[0] = -250;
								dificuldade[1] = 250;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 66 && qtdExpressoes < 89) {
								dificuldade[0] = -500;
								dificuldade[1] = 500;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 89 && qtdExpressoes < 100) {
								dificuldade[0] = -666;
								dificuldade[1] = 666;
								dificuldade[2] = 4;
							} else if (qtdExpressoes >= 100) {
								dificuldade[0] = -999;
								dificuldade[1] = 999;
								dificuldade[2] = 4;
							}
							break;
					}
					break;
				case "PrecisaoBasket10":
					// Se tratando deste modo, todas as expressões só serão de somar :)
					if (qtdExpressoes < 12) {
						// Dificuldade 1 - Very Easy: Números de 0 a 5
						dificuldade [0] = 0;
						dificuldade [1] = 5; 
						dificuldade [2] = 1; 
					} else if (qtdExpressoes >= 12 && qtdExpressoes < 25) {
						// Dificuldade 2 - Easy: Números de -7 a 7
						dificuldade [0] = -7;
						dificuldade [1] = 7;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 25 && qtdExpressoes < 41) {
						// Dificuldade 3 - Somewhat Easy: Números de -15 a 15
						dificuldade [0] = -15;
						dificuldade [1] = 15;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 41 && qtdExpressoes < 61) {
						// Dificuldade 4 - Medium: Números de 0 a 30
						dificuldade [0] = 0;
						dificuldade [1] = 30;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 61 && qtdExpressoes < 79) {
						// Dificuldade 5 - Somewhat Hard: Números de -30 a 30
						dificuldade [0] = -30;
						dificuldade [1] = 30;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 79 && qtdExpressoes < 89) {
						// Dificuldade 6 - Hard: Números de -60 a 60
						dificuldade [0] = -60;
						dificuldade [1] = 60;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 89 && qtdExpressoes < 100) {
						// Dificuldade 7 - Very Hard: Números de 0 a 100
						dificuldade [0] = 0;
						dificuldade [1] = 100;
						dificuldade [2] = 1;
					} else if (qtdExpressoes >= 100) {
						// Dificuldade 8 - Insanaly Hard: Números de -100 a 100
						dificuldade [0] = -100;
						dificuldade [1] = 100;
						dificuldade [2] = 1;
					}
					break;
				}
				return dificuldade;
		}

		private void gerarResultado(int operador){

			int num1 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
			int num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);

			int resultado = 0;

			switch(operador){
				// *** Caso 1: Soma dos dois números *** //
				case 1: 
					switch(Jogador.getDificuldadeAtual()){
						// Restrições: Dificuldade 1 - Kids
						/* A soma não pode haver agrupamento. */
						case "Kids":
							while (num2 >= 10){ num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);  }
							while ( (num1 < 10 && num2 < 10) && (num1 + num2 >= 10) ) { 
								num1 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
							}
							break;
					}
					resultado = num1 + num2;
					break;
				// *** Caso 2: Subtração dos dois números *** //
				case 2: 
					switch(Jogador.getDificuldadeAtual()){
						// Restrições: Dificuldade 1 - Kids
						/* A subtração não pode haver agrupamento nem ter um resultado negativo. */
						case "Kids":
							while (num1 >= 10 && num2 < 10) { num1 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]); }
						    while (num1 < num2){ num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);  }
							break;
						// Restrições: Dificuldade 2 - Beginner
						/* A subtração não pode ter um resultado negativo. */
						case "Beginner":
							while (num1 < num2){  num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);  }
							break;
					}
					resultado = num1 - num2;
					break;
				// *** Caso 3: Multiplicação dos dois números *** //
				case 3:
					switch(Jogador.getDificuldadeAtual()){
						// Restrições: Dificuldade 3 - Experient
						/* As multiplicações não podem ser feitas por números menores que -10 e maiores que 10. */
						case "Experient":
							while ( num2 < -10 || num2 > 10 ){ num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]); }
							break;
						// Restrições: Dificuldade 4 - Challenger
						/* As multiplicações não podem ser feitas por números menores que -15 e maiores que 15. */
						case "Experient":
							while ( num2 < -15 || num2 > 15 ){ num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]); }
							break;
					}
					resultado = num1 * num2;
					break
				// *** Caso 4: Divisão dos dois números *** //
				case 4:
					// Tratando da divisão, as restrições são globais, tanto para o Experient quanto para o Challenger
					// Se o resto da divisão dos números for diferente de 0, com certeza o número não é inteiro
					// E também claro, divisões por zero não podem existir
					while (num2 == 0 || num1 % num2 != 0){
						num1 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
						num2 = gerarNumeroAleatorio(this.dificuldade[0], this.dificuldade[1]);
					}
					resultado = num1 / num2; 
					break;
			}

			// Convertendo os números para string, se forem negativos, coloco parênteses tbm
			this.numero1 = num1.ToString();
			this.numero2 = num2.ToString();
			if (num1 < 0) { this.numero1 = "(" + this.numero1 + ")"; }
			if (num2 < 0) { this.numero2 = "(" + this.numero2 + ")"; }
			
			// Com o resultado em mãos, convertemos ele para string para ser exibido na tela
			this.resultado = resultado.ToString();

		}

		// Construtor
		public ExpressaoBasica (int qtdExpressoes) {
		
			jogoAtual = Jogador.getJogoAtual();
			dificAtual = Jogador.getDificuldadeAtual();
			
			// Gerando a dificuldade, através da quantidade de expressões feitas pelo jogador
			this.dificuldade = gerarDificuldade (qtdExpressoes);

			// Gerando os números aleatórios, junto com o operador, inicialmente na forma inteira
			int op = gerarNumeroAleatorio(1, this.dificuldade[2]);

			// Gerando o resultado, já na forma de string
			gerarResultado(op); 

			// Gerando a string do operador
			this.operador = gerarOperador(op);

			// Gerando a string com a expressão COMPLETAÇA
			this.expressao = this.numero1 + " " + this.operador + " " + this.numero2;

		}

		// Getters (sim, nada de Setters)
		public string Expressao { get { return this.expressao; } }
		public int Numero1 { get { return Convert.ToInt32(this.numero1); } }
		public int Numero2 { get { return Convert.ToInt32(this.numero2); } }
		public string Resultado { get { return this.resultado; } }

		// Métodos públicos (só tem um...)
		public bool checarResposta(string resposta){ return this.resultado.Equals (resposta); }
		
		// Método de teste: Feito para forçar o resultado de uma expressão npara teste
		public static int[] gerarExpressaoDesejada(int qtdExpressoes, int numeroDesejado){ 
			ExpressaoBasica exp = new ExpressaoBasica(qtdExpressoes);
			while (Convert.ToInt32(exp.Resultado) != numeroDesejado){ 
				exp = new ExpressaoBasica(qtdExpressoes); 
			}
			int[] numeros = {exp.Numero1, exp.Numero2};

			return numeros;
		}
	}
}
