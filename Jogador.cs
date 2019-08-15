namespace AssemblyCSharp {
	
	public abstract class Jogador {
		// Aqui ficam as informações estáticas a respeito do jogador que estiver jogando o jogo in-time. 

		private static string cenaAtual, jogoAtual; // Representa a cena e o jogo que o jogador está no momento
		private static string dificuldade; // Representa a dificuldade escolhida pelo jogador

		private static Highscores highscores; // Representa todas as melhores pontuações e rankings que o jogador já teve.

		private static int pontuacao = 0; // Pontos do jogador
		private static int qtdExpressoes = 0; // Expressões feitas pelo jogador
		private static int vidas; // Vidas do jogador, UAU

		public static string getCenaAtual(){ return cenaAtual; }
		public static void setCenaAtual(string cenaAtual){ Jogador.cenaAtual = cenaAtual; }

		public static string getJogoAtual(){ return jogoAtual; }
		public static void setJogoAtual(string jogoAtual){ Jogador.jogoAtual = jogoAtual; }

		public static string getDificuldadeAtual(){ return dificuldade; }
		public static void setDificuldadeAtual(string dificuldade){ Jogador.dificuldade = dificuldade; }

		public static void setHighscoresIniciais(){ 
			highscores = new Highscores();
			highscores.setValoresIniciais();
		}

		public static Highscores getHighscores(){ return highscores; }
		public static void setHighscores(Highscores highscores){ Jogador.highscores = highscores; }

		public static int getPontuacao(){ return pontuacao; }
		public static void adicionarPontuacao(int pontos){ pontuacao += pontos; }

		public static int getExpressoes(){ return qtdExpressoes; }
		public static void adicionarExpressoes(){ Jogador.qtdExpressoes++; }

		public static int getVidas(){ return vidas; }
		public static void tirarVidas(){ vidas--; }
		public static void setVidas(int vidas){ Jogador.vidas = vidas;}

		// Zeramos a pontuação e as expressões do jogador, resetando a vida também dependendo do modo
		public static void resetarDadosJogador(){
			pontuacao = 0;
			qtdExpressoes = 0;
			switch (jogoAtual){
				case "precisaoArcade": vidas = 3; break;
				case "precisaoTimeAttack": vidas = 1; break;
			}
		}

		// Aqui geramos o número do ranking, para depois ser convertido em letras, dependendo do modo
		public static int gerarNumeroRanking(){
			switch (jogoAtual) {
			case ("precisaoArcade"):
				if (pontuacao < 350) { return 6; } 
				else if (pontuacao >= 350 && pontuacao < 575) { return 5; }
				else if (pontuacao >= 575 && pontuacao < 790) { return 4; } 
				else if (pontuacao >= 790 && pontuacao < 935) { return 3; } 
				else if (pontuacao >= 935 && pontuacao < 1150) { return 2; } 
				else if (pontuacao >= 1150 && pontuacao < 1766) { return 1; } 
				else if (pontuacao >= 1766) { return 0; }
				break;
			case ("precisaoTimeAttack"):
				if (pontuacao < 18) { return 6; } 
				else if (pontuacao >= 18 && pontuacao < 30) { return 5; }
				else if (pontuacao >= 30 && pontuacao < 45) { return 4; } 
				else if (pontuacao >= 45 && pontuacao < 67) { return 3; } 
				else if (pontuacao >= 67 && pontuacao < 82) { return 2; } 
				else if (pontuacao >= 82 && pontuacao < 100) { return 1; } 
				else if (pontuacao >= 100) { return 0; }
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
				default: return "X";
			}
		}
	}
}