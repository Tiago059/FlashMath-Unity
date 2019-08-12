namespace AssemblyCSharp {
	
	public abstract class Jogador {
		// Aqui ficam as informações estáticas a respeito do jogador que estiver jogando o jogo in-time. 

		private static string cenaAtual, jogoAtual; // Representa a cena e o jogo que o jogador está no momento
		private static string dificuldade; // Representa a dificuldade escolhida pelo jogador

		private static Highscores highscores; // Representa todas as melhores pontuações e rankings que o jogador já teve.

		private static int pontuacao = 0; // Pontos do jogador
		private static int qtdExpressoes = 0; // Expressões feitas pelo jogador
		private static int vidas; // Vidas do jogador, UAU

		public static string getCenaAtual(){ return Jogador.cenaAtual; }
		public static void setCenaAtual(string cenaAtual){ Jogador.cenaAtual = cenaAtual; }

		public static string getJogoAtual(){ return Jogador.jogoAtual; }
		public static void setJogoAtual(string jogoAtual){ Jogador.jogoAtual = jogoAtual; }

		public static string getDificuldadeAtual(){ return Jogador.dificuldade; }
		public static void setDificuldadeAtual(string dificuldade){ Jogador.dificuldade = dificuldade; }

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

		// Zeramos a pontuação e as expressões do jogador, resetando a vida também dependendo do modo
		public static void resetarDadosJogador(){
			Jogador.pontuacao = 0;
			Jogador.qtdExpressoes = 0;
			switch (Jogador.jogoAtual){
				case "precisaoArcade": Jogador.vidas = 3; break;
				case "precisaoTimeAttack": Jogador.vidas = 1; break;
			}
		}

		// Aqui geramos o número do ranking, para depois ser convertido em letras, dependendo do modo
		public static int gerarNumeroRanking(){
			switch (Jogador.jogoAtual) {
			case ("precisaoArcade"):
				if (Jogador.pontuacao < 350) { return 6; } 
				else if (Jogador.pontuacao >= 350 && Jogador.pontuacao < 575) { return 5; }
				else if (Jogador.pontuacao >= 575 && Jogador.pontuacao < 790) { return 4; } 
				else if (Jogador.pontuacao >= 790 && Jogador.pontuacao < 935) { return 3; } 
				else if (Jogador.pontuacao >= 935 && Jogador.pontuacao < 1150) { return 2; } 
				else if (Jogador.pontuacao >= 1150 && Jogador.pontuacao < 1766) { return 1; } 
				else if (Jogador.pontuacao >= 1766) { return 0; }
				break;
			case ("precisaoTimeAttack"):
				if (Jogador.pontuacao < 18) { return 6; } 
				else if (Jogador.pontuacao >= 18 && Jogador.pontuacao < 30) { return 5; }
				else if (Jogador.pontuacao >= 30 && Jogador.pontuacao < 45) { return 4; } 
				else if (Jogador.pontuacao >= 45 && Jogador.pontuacao < 67) { return 3; } 
				else if (Jogador.pontuacao >= 67 && Jogador.pontuacao < 82) { return 2; } 
				else if (Jogador.pontuacao >= 82 && Jogador.pontuacao < 100) { return 1; } 
				else if (Jogador.pontuacao >= 100) { return 0; }
				break;
			}

			return 7;
		}

		// E aqui geramos a letra correspondente ao ranking
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