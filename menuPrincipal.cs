﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

[RequireComponent(typeof(AudioSource))]
public class menuPrincipal : MonoBehaviour {

	public GameObject[] imagesOptions; // Onde as imagens de cada modo ficarão
	public Text descriptionText; // Texto da descrição da opção

	public GameObject[] imagesHelp; // Onde as imagens de cada texto de ajuda ficarão

	public Button btnLeft, btnRight, btnSelect, btnHelp, btnExit; // Botões de movimentação

	public AudioSource helpClick; // Som que toca quando o botão de ajuda é clicado

	protected int numOption; // Numero da opção. Representa qual opção está sendo vista no momento

	private int helpButtonClicked = 1; // Flag que controla o aperto do botão de ajuda

	// Use this for initialization
	void Start () {
		
		// Debug: esta linha serve pra deletar o highscore atual
		// PlayerPrefs.DeleteKey("highscores");
		
		this.helpClick = GetComponent<AudioSource>();

		BackgroudMusicManager.Instance.play();

		this.numOption = 0; 
		for (int i = 0; i < this.imagesHelp.Length; i++) {
			this.imagesHelp[i].SetActive (false); 
		}

		// Se não houver uma chave contendo highscores, significa que um objeto do tipo highscore precisa ser criado
		if (PlayerPrefs.HasKey("highscores")) {
			try {

				/* O processo é semelhante ao serializar, a diferença é que vamos deserializar, isto é, converter de volta
				   ao formato original do objeto Highscores. 
				*/
				BinaryFormatter bf = new BinaryFormatter();
    			FileStream file = File.Open(Application.persistentDataPath + "/highscores.save", FileMode.Open);
    			Highscores highscores = (Highscores) bf.Deserialize(file);
    			file.Close();

    			// Uma vez com os dados recuperados, este será o novo highscore do jogador
    			Jogador.setHighscores(highscores);

    		}
    		// Caso dê algum erro, já saberei porque
    		catch (FileLoadException e){ Debug.Log("Erro. Não foi possível abrir o arquivo. " + e.Message); }
			catch (FileNotFoundException e) { Debug.Log("Erro. Arquivo não encontrado. " + e.Message); }
		
		}
		// No entanto, se essa chave já foi setada, precisamos recuperar o arquivo já salvo
		else {

			PlayerPrefs.SetInt("highscores", 1);

			// Setamos o estado inicial do objeto highscore
			Jogador.setHighscoresIniciais();

			/* Aqui estamos criando um arquivo chamado highscores.save, e vamos serializar (transformar em dados binários, neste caso) 
			   o objeto Highscore neste arquivo highscores.save
			*/
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/highscores.save");
			bf.Serialize(file, Jogador.getHighscores());

			// Apenas uma mensagem pra saber se deu tudo ok
			Debug.Log("Highscores Saved");

			PlayerPrefs.Save();
		}
	}

	public void clickBtnExit(){
		AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "telaTitulo");
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

	public void clickHelp(){
		if (this.helpButtonClicked % 2 != 0) { 
			helpClick.Play(0);
			this.imagesHelp[this.numOption].SetActive(true); 
		}
		else { this.imagesHelp[this.numOption].SetActive(false); }

		this.helpButtonClicked++;
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
			case 0: this.descriptionText.text = "Game Modes"; break;
			case 1: this.descriptionText.text = "Highscores"; break;
			case 2: this.descriptionText.text = "Achievements"; break;
			case 3: this.descriptionText.text = "Settings"; break;
			case 4: this.descriptionText.text = "Credits"; break;
		}
	}
}