using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class menuGameMode : MonoBehaviour {

	public GameObject[] imagesOptions; // Onde as imagens ficarão
	public Text descriptionText; // Texto da descrição da opção

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit;

	private int numOption; // Numero da opção. Representa qual opção está sendo vista no momento

	// Use this for initialization
	void Start () {

		BackgroudMusicManager.Instance.play();

		this.numOption = 0;  
	}

	public void clickBtnExit(){
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrincipal");
	}
	
	public void clickBtnLeft(){
		if (this.numOption == 0) { this.numOption = this.imagesOptions.Length - 1; } 
		else { this.numOption--; }
	}

	public void clickBtnRight(){
		if (this.numOption == this.imagesOptions.Length - 1) { this.numOption = 0; } 
		else { this.numOption++; }
	}
		
	public void clickSelect(){
		string scene = "";

		// Aqui selecionamos a cena a ser carregada, dependendo de qual imagem estamos vendo atualmente
		switch (this.numOption) {
		case 0: scene = "menuPrecisao"; break;
		case 1: scene = "precisaoArcade"; break;
		case 2: scene = "precisaoZen10"; break;
		}

		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", scene);
		//SceneManager.LoadScene (scene);
	}

	// Update is called once per frame
	void Update () {

		// A imagem fica sendo alterada dependendo do numero da imagem que aparecer
		this.imagesOptions[this.numOption].SetActive(true);
		for (int i = 0; i < this.imagesOptions.Length; i++) {
			if (i != this.numOption) { this.imagesOptions [i].SetActive (false); }
		}

		switch (this.numOption) {
		case 0: this.descriptionText.text = "Precision"; break;
		case 1: this.descriptionText.text = "Sagacity"; break;
		case 2: this.descriptionText.text = "Resistence"; break;
		}

	}
}