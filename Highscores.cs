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
		
		// --> Adicionando recordes para os novos modos de jogo, parte 1:
		/* Crie um IDictionary<string, List<int>>, onde a chave é o nome da dificuldade mapeada para
		uma lista dos recordes referentes àquela dificuldade. Crie também uma List<int> que guardará
		os rankings referentes a cada dificuldade do modo. */
	
		// Listas onde serão guardadas as melhores pontuações do jogado de cada modo
		private IDictionary<string, List<int>> highScorePrecisaoArcade;
		private IDictionary<string, List<int>> highScorePrecisaoTimeAttack;
		private IDictionary<string, List<int>> highScorePrecisaoBasket10;

		private List<int> bestRanksPrecisaoArcade;
		private List<int> bestRanksPrecisaoTimeAttack;
		private List<int> bestRanksPrecisaoBasket10;

		// Número máximo de recordes que serão armazenados. 
		private const int numRecordes = 5;

		public Highscores(){
		
			// --> Adicionando recordes para os novos modos de jogo, parte 2:
			/* Simples, apenas inicialize todas variáveis criadas anteriormente. */
			
			this.highScorePrecisaoArcade = IDictionary<string, List<int>>();
			this.highScorePrecisaoTimeAttack = IDictionary<string, List<int>>();
			this.highScorePrecisaoBasket10 = IDictionary<string, List<int>>();
			
			this.bestRanksPrecisaoArcade = new List<int>();
			this.bestRanksPrecisaoTimeAttack = new List<int>();
			this.bestRanksPrecisaoBasket10 = new List<int>();

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
			
			// --> Adicionando recordes para os novos modos de jogo, parte 3:
			/* Essa função aqui é chamada na classe "X". Para IDictionary referente a cada modo de jogo, 
			adicione, uma por vez, a string referente ao nome das dificuldades do seu jogo, mapeando a 
			string com a variável zero que é uma List<int> contendo todos seus elementos iguais a zero.
			E pronto, está finalizado. Agora basta chamar as outras funções quando você quiser manipular
			seu objeto Highscore. */
	
		
			List<int> zero = new List<int>();
			for (int i = 0; i < Highscores.numRecordes; i++) { zero.Add(0); }
		
			// Adicionando os recordes baseado em cada uma das dificuldades e o ranking mais baixo
			// **** Precisão: Arcade, 4 dificuldades **** //
			this.highScorePrecisaoArcade.Add("Kids", zero);
			this.highScorePrecisaoArcade.Add("Beginner", zero);
			this.highScorePrecisaoArcade.Add("Experient", zero);
			this.highScorePrecisaoArcade.Add("Challenger", zero);
			for (int i = 0; i < 4; i++) { this.bestRanksPrecisaoArcade.Add(6); }
			
			// **** Precisão: TimeAttack, 4 dificuldades **** //
			this.highScorePrecisaoTimeAttack.Add("Kids", zero);
			this.highScorePrecisaoTimeAttack.Add("Beginner", zero);
			this.highScorePrecisaoTimeAttack.Add("Experient", zero);
			this.highScorePrecisaoTimeAttack.Add("Challenger", zero);
			for (int i = 0; i < 4; i++) { this.bestRanksPrecisaoTimeAttack.Add(6); }
			
			// **** Precisão: Basket10, 3 dificuldades **** //
			this.highScorePrecisaoBasket10.Add("Beginner", zero);
			this.highScorePrecisaoBasket10.Add("Experient", zero);
			this.highScorePrecisaoBasket10.Add("Challenger", zero);
			for (int i = 0; i < 3; i++) { this.bestRanksPrecisaoBasket10.Add(6); }
			
			
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
