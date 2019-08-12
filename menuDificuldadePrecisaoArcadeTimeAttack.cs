using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class menuDificuldadePrecisaoArcadeTimeAttack : MonoBehaviour {
	
	public GameObject[] helpBallonsArcade, helpBallonsTimeAttack; // Onde as imagens contendo os textos de ajuda ficarão
	public GameObject imgPlaceHolder; // Imagem de ajuda que fica na tela enquanto uma dificuldade não é escolhida
	public GameObject imgWarning; // Imagem que aparece se o jogador tenta clicar sem selecionar uma opção

	public Text descriptionText; // Texto da descrição da opção
	public Text highscoreText; // Texto da melhor pontuação daquele modo
	public Text rankingText; // Texto do melhor ranking daquele modo

	public AudioSource helpClick; // Som que toca quando o botão de ajuda é clicado

	private int helpButtonClicked = 1; // Flag que controla o aperto do botão de ajuda

	private bool optionIsSelected; // Flag que controla se uma opção foi selecionada. 

	private string scene; // Representa espeficicamente o modo de jogo que estamos

	// Use this for initialization
	void Start () {

		this.helpClick = GetComponent<AudioSource>();
		BackgroudMusicManager.Instance.play();

		// Começamos com false, ou seja, com nenhuma opção selecionada
		this.optionIsSelected = false;

		this.descriptionText.text = "Chosen: ";

	}

	// Funções de clique de botão
	public void clickBtnExit(){ AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrecisao"); }

	public void clickBtnPlay(){ 

		if (this.optionIsSelected){
			LoadingSceneManager.startLoadingScene(true);
			LoadingSceneManager.setSceneToLoadAfterLoading(Jogador.getCenaAtual());
			AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "loadingScreen");
		}
	
	}
	
	public void clickBtnKids(){
		helpClick.Play(0);
		this.descriptionText.text = "Chosen: Kids";
		this.optionIsSelected = true;
		this.imgPlaceHolder.SetActive(false);

		if (Jogador.getJogoAtual() == "precisaoArcade"){
			for (int i = 0; i < this.helpBallonsArcade.Length; i++){ this.helpBallonsArcade[i].SetActive(false); }
			this.helpBallonsArcade[0].SetActive(true);
		}
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
			for (int i = 0; i < this.helpBallonsTimeAttack.Length; i++){ this.helpBallonsTimeAttack[i].SetActive(false); }
			this.helpBallonsTimeAttack[0].SetActive(true);
		}
	}

	public void clickBtnBeginner(){
		helpClick.Play(0);
		this.descriptionText.text = "Chosen: Beginner";
		this.optionIsSelected = true;
		this.imgPlaceHolder.SetActive(false);

		if (Jogador.getJogoAtual() == "precisaoArcade"){
			for (int i = 0; i < this.helpBallonsArcade.Length; i++){ this.helpBallonsArcade[i].SetActive(false); }
			this.helpBallonsArcade[1].SetActive(true);
		}
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
			for (int i = 0; i < this.helpBallonsTimeAttack.Length; i++){ this.helpBallonsTimeAttack[i].SetActive(false); }
			this.helpBallonsTimeAttack[1].SetActive(true);
		}
	}

	public void clickBtnExperient(){
		helpClick.Play(0);
		this.descriptionText.text = "Chosen: Experient";
		this.optionIsSelected = true;
		this.imgPlaceHolder.SetActive(false);

		if (Jogador.getJogoAtual() == "precisaoArcade"){
			for (int i = 0; i < this.helpBallonsArcade.Length; i++){ this.helpBallonsArcade[i].SetActive(false); }
			this.helpBallonsArcade[2].SetActive(true);
		}
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
			for (int i = 0; i < this.helpBallonsTimeAttack.Length; i++){ this.helpBallonsTimeAttack[i].SetActive(false); }
			this.helpBallonsTimeAttack[2].SetActive(true);
		}
	}

	public void clickBtnChallenger(){
		helpClick.Play(0);
		this.descriptionText.text = "Chosen: Challenger";
		this.optionIsSelected = true;
		this.imgPlaceHolder.SetActive(false);

		if (Jogador.getJogoAtual() == "precisaoArcade"){
			for (int i = 0; i < this.helpBallonsArcade.Length; i++){ this.helpBallonsArcade[i].SetActive(false); }
			this.helpBallonsArcade[3].SetActive(true);
		}
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
			for (int i = 0; i < this.helpBallonsTimeAttack.Length; i++){ this.helpBallonsTimeAttack[i].SetActive(false); }
			this.helpBallonsTimeAttack[3].SetActive(true);
		}
	}

	/*55te amooooooooooooooooooooooooooooooooooooooooooooooo try
	tiago pmn VertexHelper*/

	
	// Update is called once per frame
	void Update () {

		this.highscoreText.text = Jogador.getHighscores().melhorPontuacao().ToString();
		this.rankingText.text = Jogador.gerarRanking(Jogador.getHighscores().melhorRanking());
	}
}