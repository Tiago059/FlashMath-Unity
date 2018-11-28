using System;

namespace AssemblyCSharp {
	
	public abstract class Jogador {
		// Aqui ficam as informações estáticas a respeito do jogador que estiver jogando o jogo in-time. 

		private static string cenaAtual, jogoAtual; // Representa  a cena e o jogo que o jogador está no momento

		private static int pontuacao = 0; // Pontos do jogador
		private static int qtdExpressoes = 0; // Expressões feitas pelo jogador
		private static int vidas; // Vidas do jogador, UAU

		public static string getCenaAtual(){ return Jogador.cenaAtual; }
		public static void setCenaAtual(string cenaAtual){ Jogador.cenaAtual = cenaAtual; }

		public static string getJogoAtual(){ return Jogador.jogoAtual; }
		public static void setJogoAtual(string jogoAtual){ Jogador.jogoAtual = jogoAtual; }

		public static int getPontuacao(){ return Jogador.pontuacao; }
		public static void adicionarPontuacao(int pontos){ Jogador.pontuacao += pontos; }

		public static int getExpressoes(){ return Jogador.qtdExpressoes; }
		public static void adicionarExpressoes(){ Jogador.qtdExpressoes++; }

		public static int getVidas(){ return Jogador.vidas; }
		public static void tirarVidas(){ Jogador.vidas--; }
		public static void setVidas(int vidas){Jogador.vidas = vidas;}

		public static void resetarDadosJogador(){
			Jogador.pontuacao = 0;
			Jogador.qtdExpressoes = 0;
			if (jogoAtual == "precisaoTimeAttack") { Jogador.vidas = 1; } 
			else { Jogador.vidas = 3; }

		}
	}
}