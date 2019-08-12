using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class menuPrecisao : MonoBehaviour {
	
	public GameObject[] imagesOptions; // Onde as imagens de cada modo ficarão
	public Text descriptionText; // Texto da descrição da opção
	public Text highscoreText; // Texto da melhor pontuação daquele modo
	public Text rankingText; // Texto do melhor ranking daquele modo

	public GameObject[] imagesHelp; // Onde as imagens de cada texto de ajuda ficarão

	public AudioSource helpClick; // Som que toca quando o botão de ajuda é clicado

	private int helpButtonClicked = 1; // Flag que controla o aperto do botão de ajuda

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit; // Botões de movimentação

	private int numOption; // Numero da opção. Representa qual opção está sendo vista no momento

	private string scene; // Representa espeficicamente o modo de jogo que estamos

	// Use this for initialization
	void Start () {

		this.helpClick = GetComponent<AudioSource>();

		BackgroudMusicManager.Instance.play();

		// Como sei que a opção inicial sempre será a do Arcade, já fazemos a exibição dos dados referentes ao arcade inicialmente
		this.numOption = 0; 

		for (int i = 0; i < this.imagesHelp.Length; i++) { this.imagesHelp[i].SetActive (false); }

	}

	public void clickBtnExit(){
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "selecionarModoJogo");
	}


	public void clickBtnLeft(){

		this.imagesHelp[this.numOption].SetActive(false);
		this.helpButtonClicked = 1;

		if (this.numOption == 0) {
			this.numOption = this.imagesOptions.Length - 1;
		} else {
			this.numOption--;
		}
	}

	public void clickBtnRight(){

		this.imagesHelp[this.numOption].SetActive(false);
		this.helpButtonClicked = 1;

		if (this.numOption == this.imagesOptions.Length - 1) {
			this.numOption = 0;
		} else {
			this.numOption++;
		}
	}
	/*55te amooooooooooooooooooooooooooooooooooooooooooooooo try
	tiago pmn VertexHelper*/

	public void clickHelp(){
		if (this.helpButtonClicked % 2 != 0) { 
			helpClick.Play(0);
			this.imagesHelp[this.numOption].SetActive(true); 
		}
		else { this.imagesHelp[this.numOption].SetActive(false); }

		this.helpButtonClicked++;

		Debug.Log(this.helpButtonClicked);

	}

	public void clickSelect(){ 
		/* 
	 	LoadingSceneManager.startLoadingScene(true);
		LoadingSceneManager.setSceneToLoadAfterLoading(this.scene);
		Debug.Log(this.scene);
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "loadingScreen");
		*/
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuDificuldadePrecisao");
		//SceneManager.LoadScene("loadingScreen");
	}

	// Update is called once per frame
	void Update () {

		// A imagem fica sendo alterada dependendo do numero da imagem que aparecer, deixando as outras invisíveis
		this.imagesOptions[this.numOption].SetActive(true);
		for (int i = 0; i < this.imagesOptions.Length; i++) {
			if (i != this.numOption) { this.imagesOptions [i].SetActive (false); }
		}

		switch (this.numOption) {
			case 0:
				this.descriptionText.text = "Arcade"; 
				this.scene = "precisaoArcadeTimeAttack";
				Jogador.setCenaAtual ("precisaoArcadeTimeAttack");
				Jogador.setJogoAtual("precisaoArcade");
			 	break;
			case 1: 
				this.descriptionText.text = "Time Attack";
				this.scene = "precisaoArcadeTimeAttack"; 
				Jogador.setCenaAtual ("precisaoArcadeTimeAttack");
				Jogador.setJogoAtual("precisaoTimeAttack");
			 	break;
			case 2: 
				this.descriptionText.text = "Basket 10"; 
				this.scene = "PrecisaoBasket10";
				//Jogador.setCenaAtual ("precisaoArcadeTimeAttack");
				//Jogador.setJogoAtual("precisaoArcade");
				Jogador.setCenaAtual("PrecisaoBasket10");
				Jogador.setJogoAtual("PrecisaoBasket10");
				break;
		}

		this.highscoreText.text = Jogador.getHighscores().melhorPontuacao().ToString();
		this.rankingText.text = Jogador.gerarRanking(Jogador.getHighscores().melhorRanking());
	}
}