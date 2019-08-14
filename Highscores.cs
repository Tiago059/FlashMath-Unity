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
		uma lista dos recordes referentes àquela dificuldade. Crie também um IDictionary<string, int> que guardará
		o melhor ranking referentes a cada dificuldade do modo. */
	
		// Dicionários onde serão guardadas as melhores pontuações do jogado de cada modo
		private IDictionary<string, List<int>> highScorePrecisaoArcade;
		private IDictionary<string, List<int>> highScorePrecisaoTimeAttack;
		private IDictionary<string, List<int>> highScorePrecisaoBasket10;
		
		// Dicionários que associarão as dificuldades aos seus respectivos melhores rankings
		private IDictionary<string, int> bestRanksPrecisaoArcade;
		private IDictionary<string, int> bestRanksPrecisaoTimeAttack;
		private IDictionary<string, int> bestRanksPrecisaoBasket10;

		// Número máximo de recordes que serão armazenados. 
		private const int numMaxRecordes = 5;
		
		// Jogo e dificuldade atual do Jogador no momento da criação do objeto Highscores. 
		private string jogoAtual, dificAtual;

		public Highscores(){
		
			// --> Adicionando recordes para os novos modos de jogo, parte 2:
			/* Simples, apenas inicialize todas variáveis criadas anteriormente. */
			
			highScorePrecisaoArcade = new IDictionary<string, List<int>>();
			highScorePrecisaoTimeAttack = new IDictionary<string, List<int>>();
			highScorePrecisaoBasket10 = new IDictionary<string, List<int>>();
			
			bestRanksPrecisaoArcade = new IDictionary<string, int>();
			bestRanksPrecisaoTimeAttack = new IDictionary<string, int>();
			bestRanksPrecisaoBasket10 = new IDictionary<string, int>();
			
			jogoAtual = Jogador.getJogoAtual();
			dificAtual = Jogador.getDificuldadeAtual();

		}

		public void setValoresIniciais(){
			
			// --> Adicionando recordes para os novos modos de jogo, parte 3:
			/* Essa função aqui é chamada na classe "X". Para IDictionary referente a cada modo de jogo, 
			adicione, uma por vez, a string referente ao nome das dificuldades do seu jogo, mapeando a 
			string com a variável zero que é uma List<int> contendo todos seus elementos iguais a zero.
			*/

			List<int> zero = new List<int>();
			for (int i = 0; i < numMaxRecordes; i++) { zero.Add(0); }
		
			// Adicionando os recordes baseado em cada uma das dificuldades e o ranking mais baixo
			// **** Precisão-Arcade, 4 dificuldades **** //
			this.highScorePrecisaoArcade.Add("Kids", zero);
			this.highScorePrecisaoArcade.Add("Beginner", zero);
			this.highScorePrecisaoArcade.Add("Experient", zero);
			this.highScorePrecisaoArcade.Add("Challenger", zero);
			for (int i = 0; i < 4; i++) { this.bestRanksPrecisaoArcade.Add(6); }
			
			// **** Precisão-TimeAttack, 4 dificuldades **** //
			this.highScorePrecisaoTimeAttack.Add("Kids", zero);
			this.highScorePrecisaoTimeAttack.Add("Beginner", zero);
			this.highScorePrecisaoTimeAttack.Add("Experient", zero);
			this.highScorePrecisaoTimeAttack.Add("Challenger", zero);
			for (int i = 0; i < 4; i++) { this.bestRanksPrecisaoTimeAttack.Add(6); }
			
			// **** Precisão-Basket10, 3 dificuldades **** //
			this.highScorePrecisaoBasket10.Add("Beginner", zero);
			this.highScorePrecisaoBasket10.Add("Experient", zero);
			this.highScorePrecisaoBasket10.Add("Challenger", zero);
			for (int i = 0; i < 3; i++) { this.bestRanksPrecisaoBasket10.Add(6); }
			
			
		}
		
		// --> Adicionando recordes para os novos modos de jogo, parte 4:
		/* Essa função aqui quando chamada vai tentar adicionar um novo recorde na lista de recordes. 
		O que ele faz é simplesmente chamar a função de ordenar recordes que retorna uma lista contendo todos
		os recordes ordenados. Para um novo modo de jogo basta adicionar um case com o nome do seu modo de jogo
		e atribuir a sua lista de recordes atual (passando a dificuldade escolhida que é a nossa chave) ao retorno
		da chamada "ordenarRecordes" passando a sua própria lista de recordes e novoRecorde que a função vai 
		ordenar automaticamente (se precisar). */
		public void adicionarRecordes(int novoRecorde){
			switch (jogoAtual){
				case "precisaoArcade": 
					highScorePrecisaoArcade[dificAtual] = ordenarRecordes(highScorePrecisaoArcade[dificAtual], novoRecorde);
					break;
				case "precisaoTimeAttack":
					highScoreTimeAttack[dificAtual] = ordenarRecordes(highScorePrecisaoTimeAttack[dificAtual], novoRecorde);
					break;
				case "PrecisaoBasket10":
					thighScoreTimeAttack[dificAtual] = ordenarRecordes(highScorePrecisaoTimeAttack[dificAtual], novoRecorde);
					break;
			}
		}
		
		
		// --> Adicionando recordes para os novos modos de jogo, parte 5:
		/* Esta função aqui retorna a melhor pontuação do jogador, baseado na dificuldade escolhida. O funcionamento
		é bem semelhante à função anterior, apenas o retorno que é diferente. Do mesmo jeito, adicione um case com
		o nome do seu modo de jogo e retorne o maior valor da sua lista de recordes associada com a sua chave definida
		pela dificuldade atual. */
		public int melhorPontuacao(){
			switch (jogoAtual){
				case "precisaoArcade": return highScorePrecisaoArcade[dificAtual].ElementAt(0);     
				case "precisaoTimeAttack": return highScorePrecisaoTimeAttack[dificAtual].ElementAt(0);
				case "PrecisaoBasket10": return highScorePrecisaoBasket10[dificAtual].ElementAt(0);
				default: return null; 
			}
		}
		
		// --> Adicionando recordes para os novos modos de jogo, parte 6:
		/* Mais uma vez, apenas questão de retorno. Aqui retornamos */
		public int melhorRanking(){
			switch (Jogador.getJogoAtual()){
				case "precisaoArcade": return this.bestRankPrecisaoArcade; 
				case "precisaoTimeAttack": return this.bestRankPrecisaoTimeAttack;
				case "PrecisaoBasket10": return this.bestRankPrecisaoBasket10;
				// --> Adicione aqui para novo recorde <--
			}

			return 0;
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
	
	/* Essa função aqui ordena os recordes na ordem descrescente, isto se o novo recorde a ser passado é 
		maior que o último elemento da lista. Se sim, ele ordena os elementos, remove o último, que no caso
		corresponderá ao novo recorde mais baixo da lista e o remove. */
		private List<int> ordenarRecordes (List<int> recordes, int novoRecorde) {
			
			if (novoRecorde > recordes[numMaxRecordes - 1]){
				recordes.Add(novoRecorde);
				for (int i = 0; i < numMaxRecordes + 1; i++) {
					for (int j = 0; j < numMaxRecordes + 1; j++) {
						if (recordes[i] <= recordes[j]){
							int temp = recordes[j];
							recordes[i] = recordes[j];
							recordes[j] = temp; 
						}
					}
				}
				recordes.RemoveAt(numMaxRecordes);
			}
			
			return recordes;
		}
}
