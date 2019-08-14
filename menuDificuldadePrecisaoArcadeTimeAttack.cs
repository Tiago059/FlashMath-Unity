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

	public Text descriptionText; // Texto da descrição da opção
	public Text highscoreText; // Texto da melhor pontuação daquele modo
	public Text rankingText; // Texto do melhor ranking daquele modo

	public AudioSource helpClick; // Som que toca quando o botão de ajuda é clicado

	private int helpButtonClicked = 1; // Flag que controla o aperto do botão de ajuda

	private bool optionIsSelected; // Flag que controla se uma opção foi selecionada. 

	private string scene; // Representa espeficicamente o modo de jogo que estamos

	public Button button;

	Color tempColor; 

	

	// Use this for initialization
	void Start () {

		// Isso carrega os efeitos sonoros e toca a música de Background continuamente
		this.helpClick = GetComponent<AudioSource>();
		BackgroudMusicManager.Instance.play();

		// Começamos com false, ou seja, com nenhuma opção selecionada
		this.optionIsSelected = false;

		// Inserimos o texto que mostra qual dificuldade foi escolhida
		this.descriptionText.text = "Chosen: ";

		tempColor = button.GetComponent<Image>().color;		
        tempColor.a = 0.5f; //1f makes it fully visible, 0f makes it fully transparent.
        button.GetComponent<Image>().color = tempColor;

	}
	

	// Funções de clique de botão
	public void clickBtnExit(){ AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrecisao"); }

	public void clickBtnPlay(){ 

		if (this.optionIsSelected){
			LoadingSceneManager.startLoadingScene(true);
			LoadingSceneManager.setSceneToLoadAfterLoading(Jogador.getCenaAtual());
			AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "loadingScreen");
		}
		else {
		
			this.imgPlaceHolder.SetActive(false);
			this.imgWarning.SetActive(true);
		}
	
	}
	

	private void settingDificult(string dificult, int numDificult){
	
		tempColor = button.GetComponent<Image>().color;		
        tempColor.a = 1f; //1f makes it fully visible, 0f makes it fully transparent.
        button.GetComponent<Image>().color = tempColor;
		Debug.Log(button.GetComponent<Image>().color.a.ToString());

		helpClick.Play(0);
		this.descriptionText.text = "Chosen: " + dificult;
		this.optionIsSelected = true;
		this.imgPlaceHolder.SetActive(false);
		this.imgWarning.SetActive(false);
		Jogador.setDificuldadeAtual(dificult);

		GameObject[] imgList = null;
		if (Jogador.getJogoAtual() == "precisaoArcade"){ imgList = this.helpBallonsArcade; }
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){ imgList = this.helpBallonsTimeAttack; }

		for (int i = 0; i < imgList.Length; i++){ 
			imgList[i].SetActive(false); 
			this.selectedImgs[i].SetActive(false);
		}

		imgList[numDificult].SetActive(true);
		this.selectedImgs[numDificult].SetActive(true);

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