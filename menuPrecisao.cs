﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class menuPrecisao : MonoBehaviour {
	
	public GameObject[] imagesOptions; // Onde as imagens ficarão
	public Text descriptionText; // Texto da descrição da opção

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit;

	private int numOption; // Numero da opção. Representa qual opção está sendo vista no momento


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
		case 0:
			scene = "precisaoArcadeTimeAttack";
			Jogador.setCenaAtual ("precisaoArcadeTimeAttack");
			Jogador.setJogoAtual("precisaoArcade");
			break;
		case 1: 
			scene = "precisaoArcadeTimeAttack"; 
			Jogador.setCenaAtual ("precisaoArcadeTimeAttack");
			Jogador.setJogoAtual("precisaoTimeAttack");
			break;
		case 2: scene = "precisaoArcade"; break;
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
		case 0: this.descriptionText.text = "Arcade"; break;
		case 1: this.descriptionText.text = "Time Attack"; break;
		case 2: this.descriptionText.text = "Resistence"; break;
		}

	}
}