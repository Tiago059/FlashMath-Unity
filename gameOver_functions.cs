using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class gameOver_functions : MonoBehaviour {

	public Button btnReiniciarJogo;
	public Text txtPontuacao;

	// Use this for initialization
	void Start () {

		this.txtPontuacao.text = Jogador.getPontuacao ().ToString();
	}

	public void reiniciarJogo(){
		// Reinicia os dados do jogador
		Jogador.resetarDadosJogador();
		//Jogador.setIsReinicializado (true);
		// Carrega o jogo que o player estava jogando antes
		SceneManager.LoadScene(Jogador.getCenaAtual());
	}
	

}
