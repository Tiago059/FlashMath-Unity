using System;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {

	/* Esta classe é responsável por armazenar as melhores pontuações do usuário para cada modo de jogo
	 * e seus respectivos rankings, em dados do tipo JSON para escrita e leitura. 
	*/

	/* Sequência de ações:
		1 - Ao iniciar o jogo pela primeira vez, criamos um objeto Highscores com todos os valores zerados, e salvamos
		no JSON. Setamos em PlayerPrefs um valor booleano para dizer que o Highscore JSON já está lá pronto para usar,
		de modo que quando o jogador inicie o jogo pela segunda vez ele não crie esse JSON zerado.

		2 - Nas exibições do Highscore, pegamos do arquivo JSON já salvo, convertemos esse arquivo JSON em um objeto 
		Highscore, setamos esse objeto na classe abstrata Jogador, e usamos o método obterMaiorPontuacao(string modo) 
		para fazer a exibição. Não há necessidade de ficarmos criando arquivos JSON sempre que quisermos apenas ler os 
		dados, na execução inicial do programa é setado o JSON para o Jogador.
			
		3 - Na hora de adicionar uma possível nova pontuação no highscores, pegamos a pontuação, e chamamos o método
		calcularHighscore(string modo, int novaPontuacao) e modificamos o Highscore. Feito isso, gravamos novamente 
		no arquivo JSON as atualizações do highscore do jogador. O mesmo vale para o ranking, através do método 
		setRanking(string modo, string novoRanking). 
		
	*/
	[System.Serializable]
	public class Highscores {

		// Listas onde serão guardadas as melhores pontuações do jogado de cada modo. 
		private List<int> highScorePrecisaoArcade;
		private List<int> highScorePrecisaoTimeAttack;

		// Strings que contém cada um dos melhores rankings de cada modo pelo jogador.
		private int bestRankPrecisaoArcade;
		private int bestRankPrecisaoTimeAttack;

		// Número de recordes que serão armazenados. 
		private const int numRecordes = 5;

		public Highscores(){
					
			this.highScorePrecisaoArcade = new List<int> ();
			this.highScorePrecisaoTimeAttack = new List<int> ();
		
		}

		private List<int> ordenarRecordes (List<int> recordes) {
			for (int i = 0; i < Highscores.numRecordes + 1; i++) {
				for (int j = 0; j < Highscores.numRecordes + 1; j++) {
					if (recordes[i] <= recordes[j]){
						int temp = recordes[j];
						recordes[i] = recordes[j];
						recordes[j] = temp; 
					}
				}
			}
			recordes.RemoveAt(Highscores.numRecordes);
			return recordes;
		}
		
		public void setValoresIniciais(){

			this.bestRankPrecisaoArcade = 6;
			this.bestRankPrecisaoTimeAttack = 6;

			for (int i = 0; i < Highscores.numRecordes; i++) {

				this.highScorePrecisaoArcade.Add(0);
				this.highScorePrecisaoTimeAttack.Add(0);

			}
		}

		public int melhorPontuacao(){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": return this.highScorePrecisaoArcade[0]; 
				case "precisaoTimeAttack": return this.highScorePrecisaoTimeAttack[0];
			}

			return 0;
		}

		public int melhorRanking(){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": return this.bestRankPrecisaoArcade; 
				case "precisaoTimeAttack": return this.bestRankPrecisaoTimeAttack;
			}

			return 0;
		}


		public void adicionarRecordes(int novoRecorde){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": 
					this.highScorePrecisaoArcade.Add(novoRecorde);
					this.highScorePrecisaoArcade = this.ordenarRecordes(this.highScorePrecisaoArcade);
					break;
				case "precisaoTimeAttack":
					this.highScorePrecisaoTimeAttack.Add(novoRecorde);
					this.highScorePrecisaoTimeAttack = this.ordenarRecordes(this.highScorePrecisaoTimeAttack);
					break;

			}
		}

		public bool adicionarRanking(int novoRanking){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": 
					if (novoRanking < this.bestRankPrecisaoArcade){ 
						this.bestRankPrecisaoArcade = novoRanking;
						return true; 
					} else { return false; }
				case "precisaoTimeAttack":
					if (novoRanking < this.bestRankPrecisaoTimeAttack){ 
						this.bestRankPrecisaoTimeAttack = novoRanking;
						return true; 
					} else { return false; }
			}

			return false;
		}			
	}
}

