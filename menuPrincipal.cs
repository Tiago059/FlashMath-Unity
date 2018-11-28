using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour {

	public GameObject[] imagesOptions; // Onde as imagens ficarão
	public Text descriptionText; // Texto da descrição da opção

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit;

	protected int numOption; // Numero da opção. Representa qual opção está sendo vista no momento

	// Use this for initialization
	void Start () {

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

	public void clickSelect(){
		string scene = "";

		// Aqui selecionamos a cena a ser carregada, dependendo de qual imagem estamos vendo atualmente
		switch (this.numOption) {
		case 0: scene = "selecionarModoJogo"; break;
		case 1: scene = "highscoresMenu"; break;
		case 2: scene = "achievementsMenu"; break;
		case 3: scene = "settingsMenu"; break;
		case 4: scene = "creditsMenu"; break;
		}
			
		SceneManager.LoadScene (scene);
	}

	// Update is called once per frame
	void Update () {
		 
		// A imagem fica sendo alterada dependendo do numero da imagem que aparecer
		this.imagesOptions[this.numOption].SetActive(true);
		for (int i = 0; i < this.imagesOptions.Length; i++) {
			if (i != this.numOption) { this.imagesOptions [i].SetActive (false); }
		}

		switch (this.numOption) {
		case 0: this.descriptionText.text = "Game Modes"; break;
		case 1: this.descriptionText.text = "Highscores"; break;
		case 2: this.descriptionText.text = "Achievements"; break;
		case 3: this.descriptionText.text = "Settings"; break;
		case 4: this.descriptionText.text = "Credits"; break;
		}
			
	}
}