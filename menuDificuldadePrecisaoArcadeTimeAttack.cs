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

	public GameObject[] selectedImgs; // Imagens de seleção que ficarão em volto da opção selecionada

	public Text descriptionText; // Texto da descrição da dificuldade selecionada
	
	public Text highscoreText; // Texto da melhor pontuação daquele modo naquela dificuldade
	public Text rankingText; // Texto do melhor ranking daquele modo naquela dificuldade

	public AudioSource helpClick; // Som que toca quando o botão de ajuda é clicado
	
	public Button btnPlay; // Botão de iniciar jogo, ele está aqui pois vamos mexer na aparência dele

	private int helpButtonClicked = 1; // Flag que controla o aperto do botão de ajuda

	private bool optionIsSelected; // Flag que controla se uma opção foi selecionada. 

	private string scene; // Representa espeficicamente o modo de jogo que estamos

	private Color btnColor; // Objeto Color que usaremos para mudar a cor do botão

	// Use this for initialization
	void Start () {

		// Isso carrega os efeitos sonoros e toca a música de Background continuamente
		helpClick = GetComponent<AudioSource>();
		BackgroudMusicManager.Instance.play();

		// Começamos com false, ou seja, com nenhuma opção selecionada
		optionIsSelected = false;

		// Inserimos o texto que mostra qual dificuldade foi escolhida
		descriptionText.text = "Chosen: ";
		
		// O melhor ranking e a melhor pontuação começam em branco já que ele não selecionou nada
		highscoreText.text = " - ";
		rankingText.text = "-";
		
		// Deixando o botão levemente transparente, já que ele não clicou em nada
		btnColor = btnPlay.GetComponent<Image>().color;	// Pegamos a cor atual da imagem do botão
        btnColor.a = 0.5f; //Mudamos seu valor alpha para deixá-lo 50% transparente
        btnPlay.GetComponent<Image>().color = btnColor; // Setamos a nova cor do botão

	}
	

	// Funções de clique de botão
	public void clickBtnExit(){ AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrecisao"); }

	public void clickBtnPlay(){ 
		
		// Se o usuário selecionou uma dificuldade, podemos levá-lo para o jogo
		if (optionIsSelected){
			LoadingSceneManager.startLoadingScene(true);
			LoadingSceneManager.setSceneToLoadAfterLoading(Jogador.getCenaAtual());
			AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "loadingScreen");
		}
		// Se não, soltamos um aviso para o jogador
		else {
			this.imgPlaceHolder.SetActive(false);
			this.imgWarning.SetActive(true);
		}
	
	}
	
	// Essa função seta a dificuldade do jogador, seta o highscore e o melhor ranking daquela dificuldade e o texto descritivo
	private void settingDificult(string dificult, int numDificult){
		
		// Fazendo o botão ficar 100% visível denovo, já que ele selecionou uma dificuldade
		btnColor = btnPlay.GetComponent<Image>().color;		
        btnColor.a = 1f; 
        btnPlay.GetComponent<Image>().color = btnColor;
		
		Debug.Log(button.GetComponent<Image>().color.a.ToString());

		helpClick.Play(0); // Reproduz o efeito sonoro de clique
		descriptionText.text = "Chosen: " + dificult; // Mostra o texto com a dificuldade selecionada
		optionIsSelected = true; // Ativa nossa flag, para que ele possa poder jogar
		imgPlaceHolder.SetActive(false); // Removemos o placeholder
		imgWarning.SetActive(false); // Removemos a imagem de aviso
		Jogador.setDificuldadeAtual(dificult); // Setamos a dificuldade do jogador
		
		// Aqui vemos quais são as imagens de descrição que vamos carregar, dependendo do jogo
		GameObject[] imgList = null;
		if (Jogador.getJogoAtual() == "precisaoArcade"){ imgList = helpBallonsArcade; }
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){ imgList = helpBallonsTimeAttack; }
	
		// Deixamos todas as imagens invisíveis
		for (int i = 0; i < imgList.Length; i++){ 
			imgList[i].SetActive(false); 
			selectedImgs[i].SetActive(false);
		}
		
		// E deixamos visível apenas a imagem referente aquele modo, junto com sua imagem seletora
		imgList[numDificult].SetActive(true);
		selectedImgs[numDificult].SetActive(true);
		
		// Setando o highscore e o melhor ranking daquela dificuldade
		highscoreText.text = Jogador.getHighscores().melhorPontuacao().ToString();
		rankingText.text = Jogador.gerarRanking(Jogador.getHighscores().melhorRanking());

	}

	// Botões que mudam a dificuldade do Jogo
	public void clickBtnKids(){ settingDificult("Kids", 0); }
	public void clickBtnBeginner(){ settingDificult("Beginner", 1); }
	public void clickBtnExperient(){ settingDificult("Experient", 2); }
	public void clickBtnChallenger(){ settingDificult("Challenger", 3); }

	/*55te amooooooooooooooooooooooooooooooooooooooooooooooo try
	tiago pmn VertexHelper*/
	
	// Update is called once per frame
	void Update () {}

}
