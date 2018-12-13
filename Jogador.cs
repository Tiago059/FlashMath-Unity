using System;

namespace AssemblyCSharp {
	
	public abstract class Jogador {
		// Aqui ficam as informações estáticas a respeito do jogador que estiver jogando o jogo in-time. 

		private static string cenaAtual, jogoAtual; // Representa  a cena e o jogo que o jogador está no momento

		private static Highscores highscores; // Representa todas as melhores pontuações e rankings que o jogador já teve.

		private static int pontuacao = 0; // Pontos do jogador
		private static int qtdExpressoes = 0; // Expressões feitas pelo jogador
		private static int vidas; // Vidas do jogador, UAU

		public static string getCenaAtual(){ return Jogador.cenaAtual; }
		public static void setCenaAtual(string cenaAtual){ Jogador.cenaAtual = cenaAtual; }

		public static string getJogoAtual(){ return Jogador.jogoAtual; }
		public static void setJogoAtual(string jogoAtual){ Jogador.jogoAtual = jogoAtual; }

		public static void setHighscoresIniciais(){ 
			Jogador.highscores = new Highscores();
			Jogador.highscores.setValoresIniciais();
		}
		public static Highscores getHighscores(){ return Jogador.highscores; }
		public static void setHighscores(Highscores highscores){ Jogador.highscores = highscores; }

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

		public static int gerarNumeroRanking(){
			switch (Jogador.jogoAtual) {
			case ("precisaoArcade"):
				if (Jogador.pontuacao < 250) { return 6; } 
				else if (Jogador.pontuacao >= 250 && Jogador.pontuacao < 475) { return 5; }
				else if (Jogador.pontuacao >= 475 && Jogador.pontuacao < 690) { return 4; } 
				else if (Jogador.pontuacao >= 690 && Jogador.pontuacao < 835) { return 3; } 
				else if (Jogador.pontuacao >= 835 && Jogador.pontuacao < 1050) { return 2; } 
				else if (Jogador.pontuacao >= 1050 && Jogador.pontuacao < 1666) { return 1; } 
				else if (Jogador.pontuacao >= 1666) { return 0; }
				break;
			case ("precisaoTimeAttack"):
				if (Jogador.pontuacao < 13) { return 6; } 
				else if (Jogador.pontuacao >= 13 && Jogador.pontuacao < 25) { return 5; }
				else if (Jogador.pontuacao >= 25 && Jogador.pontuacao < 40) { return 4; } 
				else if (Jogador.pontuacao >= 40 && Jogador.pontuacao < 62) { return 3; } 
				else if (Jogador.pontuacao >= 62 && Jogador.pontuacao < 77) { return 2; } 
				else if (Jogador.pontuacao >= 77 && Jogador.pontuacao < 100) { return 1; } 
				else if (Jogador.pontuacao >= 100) { return 0; }
				break;
			}

			return 7;
		}

		public static string gerarRanking(int numRanking){

			switch (numRanking) {
			case 6: return "F"; 
			case 5: return "E"; 
			case 4: return "D"; 
			case 3: return "C"; 
			case 2: return "B"; 
			case 1: return "A"; 
			case 0: return "S"; 
			}

			return "";
				
		}
	}
}