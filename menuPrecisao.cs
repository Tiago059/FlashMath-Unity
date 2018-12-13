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

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit; // Botões de movimentação

	private int numOption; // Numero da opção. Representa qual opção está sendo vista no momento

	private string scene; // Representa espeficicamente o modo de jogo que estamos


	// Use this for initialization
	void Start () {

		// Como sei que a opção inicial sempre será a do Arcade, já fazemos a exibição dos dados referentes ao arcade inicialmente
		this.numOption = 0; 

	}

	public void clickBtnLeft(){
		if (this.numOption == 0) {
			this.numOption = this.imagesOptions.Length - 1;
		} else {
			this.numOption--;
		}
	}

	public void clickBtnRight(){
		if (this.numOption == this.imagesOptions.Length - 1) {
			this.numOption = 0;
		} else {
			this.numOption++;
		}
	}

	public void clickSelect(){ SceneManager.LoadScene (this.scene); }

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
			case 2: this.descriptionText.text = "Zen 10"; break;
		}

		this.highscoreText.text = Jogador.getHighscores().melhorPontuacao().ToString();
		this.rankingText.text = Jogador.gerarRanking(Jogador.getHighscores().melhorRanking());

	}
}
