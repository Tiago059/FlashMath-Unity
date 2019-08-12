using System.Collections.Generic;

namespace AssemblyCSharp {

	/* Esta classe é responsável por armazenar as melhores pontuações do usuário para cada modo de jogo
	 * e seus respectivos rankings, transformando em arquivos que estarão no formato .save criados através 
     * do Binary Formatter para escrita e leitura. 
	*/

	/* Mas como isso funcionará na prática?
		1 - Ao iniciar o jogo pela primeira vez, criamos um novo objeto Highscores com todos os valores zerados. 
        Então salvamos esse objeto em um arquivo do tipo .save através do Binary Formatter. Setamos no 
        PlayerPrefs um valor booleano para dizer que o Highscore .save já está lá pronto para usar, de modo que 
        quando o jogador inicie o jogo pela segunda vez ele não crie novamente outro arquivo.

		2 - Nas exibições do Highscore, pegamos do arquivo .save já salvo, convertemos esse arquivo .save em um objeto 
		Highscore, setamos esse objeto na classe abstrata Jogador e voalá. Agora basta chamar qualquer método que eu
        precise em cima deste objeto Highscore, como exibir a maior pontuação por exemplo. Não há necessidade de 
        ficarmos criando arquivos .save sempre que quisermos apenas ler os dados, na execução inicial do jogo 
        é setado o .save para o Jogador.
			
		3 - Na hora de adicionar uma possível nova pontuação no highscores, pegamos a pontuação, e chamamos o método
		calcularHighscore(string modo, int novaPontuacao) e modificamos o Highscore. Feito isso, gravamos novamente 
		no arquivo .save, no caso, sobrescrevendo o objeto que estava lá com o novo objeto que criamos, que tem as 
        atualizações do highscore do jogador. O mesmo vale para o ranking, através do método setRanking. 
		
	*/
	[System.Serializable]
	public class Highscores {

		/* Para adicionar novos recordes referentes a novos modos de jogo, onde estiver escrito
		   "adicione aqui para novo recorde", crie uma variável de acordo com as já existentes. */

		// Listas onde serão guardadas as melhores pontuações do jogado de cada modo.
		// --> Adicione aqui para novo recorde <-- 
		private List<int> highScorePrecisaoArcade;
		private List<int> highScorePrecisaoTimeAttack;
		private List<int> highScorePrecisaoBasket10;

		// Strings que contém cada um dos melhores rankings de cada modo pelo jogador.
		// --> Adicione aqui para novo recorde <-- 
		private int bestRankPrecisaoArcade;
		private int bestRankPrecisaoTimeAttack;
		private int bestRankPrecisaoBasket10;

		// Número de recordes que serão armazenados. 
		private const int numRecordes = 5;

		public Highscores(){
			
			this.highScorePrecisaoArcade = new List<int> ();
			this.highScorePrecisaoTimeAttack = new List<int> ();
			this.highScorePrecisaoBasket10 = new List<int>();
			// --> Adicione aqui para novo recorde <-- 
		
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
			this.bestRankPrecisaoBasket10 = 6;
			// --> Adicione aqui para novo recorde <--

			for (int i = 0; i < Highscores.numRecordes; i++) {

				this.highScorePrecisaoArcade.Add(0);
				this.highScorePrecisaoTimeAttack.Add(0);
				this.highScorePrecisaoBasket10.Add(0);
				// --> Adicione aqui para novo recorde <--

			}
		}

		public int melhorPontuacao(){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": return this.highScorePrecisaoArcade[0];     
				case "precisaoTimeAttack": return this.highScorePrecisaoTimeAttack[0];
				case "PrecisaoBasket10": return this.highScorePrecisaoBasket10[0];
				// --> Adicione aqui para novo recorde <--
			}

			return 0;
		}

		public int melhorRanking(){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": return this.bestRankPrecisaoArcade; 
				case "precisaoTimeAttack": return this.bestRankPrecisaoTimeAttack;
				case "PrecisaoBasket10": return this.bestRankPrecisaoBasket10;
				// --> Adicione aqui para novo recorde <--
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
				case "PrecisaoBasket10":
					this.highScorePrecisaoBasket10.Add(novoRecorde);
					this.highScorePrecisaoBasket10 = this.ordenarRecordes(this.highScorePrecisaoBasket10);
					break;
				// --> Adicione aqui para novo recorde <--

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
				case "PrecisaoBasket10":
					if (novoRanking < this.bestRankPrecisaoBasket10){ 
						this.bestRankPrecisaoBasket10 = novoRanking;
						return true; 
					} else { return false; }
				// --> Adicione aqui para novo recorde <--
			}

			return false;
		}			
	}
}