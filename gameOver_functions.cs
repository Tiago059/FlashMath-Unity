using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class gameOver_functions : MonoBehaviour {

	public Text txtPontuacao; 
	public Text txtMelhorPontuacao;
	public Text txtRanking;
	public Text txtMelhorRanking;

	public Text novaMelhorPontuacao;
	public Text novoMelhorRanking;

	private bool blinkMelhorPontuacao = false;
	private bool blinkMelhorRanking = false;

	// Use this for initialization
	void Start () {

		// As mensagens de melhores pontuações e melhores rankings começam desligado, inicialmente
		this.novaMelhorPontuacao.text = "";
		this.novoMelhorRanking.text = "";

		// Exibe a pontuação feita pelo player
		this.txtPontuacao.text = Jogador.getPontuacao ().ToString();

		// Recuperamos o arquivo salvo com os highscores
		BinaryFormatter bf = new BinaryFormatter();
    	FileStream file = File.Open(Application.persistentDataPath + "/highscores.save", FileMode.Open);
    	Highscores highscores = (Highscores) bf.Deserialize(file);
    	file.Close();

    	// Uma vez com os dados recuperados, este será o novo highscore do jogador
    	Jogador.setHighscores(highscores);

		// Exibe a mensagem de melhor pontuação caso o Jogador tenha feito uma melhor pontuação
		if (Jogador.getPontuacao () > Jogador.getHighscores().melhorPontuacao()){
			this.novaMelhorPontuacao.text = "New Best Score!";
			this.blinkMelhorPontuacao = true;

		}

		// Aqui tentamos ver se o jogador conseguiu fazer um novo ranking, exibindo a mensagem se sim
		if (Jogador.getHighscores().adicionarRanking(Jogador.gerarNumeroRanking())){
			this.novoMelhorRanking.text = "New Best Rank!";
			this.blinkMelhorRanking = true;
		}

		// Tentando adicionar a nova pontuação do jogador como um possível novo recorde
		Jogador.getHighscores().adicionarRecordes(Jogador.getPontuacao());

		// Exibindo qual é a melhor pontuação do jogador, naquele modo
		this.txtMelhorPontuacao.text = Jogador.getHighscores().melhorPontuacao().ToString();

		// Mostrando o ranking que o Jogador ficou no jogo que ele jogou naquele momento
		this.txtRanking.text = Jogador.gerarRanking(Jogador.gerarNumeroRanking());

		// Mostrando o melhor ranking que o Jogador já obteve
		this.txtMelhorRanking.text = Jogador.gerarRanking(Jogador.getHighscores().melhorRanking());

		// Salvamos as possíveis atualizações do Highscore no arquivo 
		File.Delete(Application.persistentDataPath + "/highscores.save");
		file = File.Create(Application.persistentDataPath + "/highscores.save");

		bf = new BinaryFormatter();
		bf.Serialize(file, Jogador.getHighscores());

		// Reseta os dados do jogador
		Jogador.resetarDadosJogador();
	}

	public void goToSelectMode(){
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrecisao");
	}

	// Carrega o jogo que o player estava jogando antes
	public void reiniciarJogo(){ 
		LoadingSceneManager.startLoadingScene(true);
		LoadingSceneManager.setSceneToLoadAfterLoading(Jogador.getCenaAtual());
		SceneManager.LoadScene("loadingScreen");
	}

	void Update(){
		if (this.blinkMelhorPontuacao) {
			this.novaMelhorPontuacao.color = new Color(this.novaMelhorPontuacao.color.r, this.novaMelhorPontuacao.color.g, this.novaMelhorPontuacao.color.b, Mathf.PingPong(Time.time, 1));
		}
		if (this.blinkMelhorRanking) {
			this.novoMelhorRanking.color = new Color(this.novoMelhorRanking.color.r, this.novoMelhorRanking.color.g, this.novoMelhorRanking.color.b, Mathf.PingPong(Time.time, 1));
		}
	}
}