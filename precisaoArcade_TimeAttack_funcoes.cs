using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class precisaoArcade_TimeAttack_funcoes : MonoBehaviour {

	public Button[] btnsNumerados; // Lista dos botões numerados, mais o de negativar e o de apagar

	public Text txtEntradaResposta; // Onde o usuário digita a resposta dele
	public Text txtExpressao; // Texto aonde a expressão é mostrada
	public Text txtPontuacao; // Texto aonde aparecem a pontuação do caboco

	public Button btnConfirmarResposta; // Botão aonde o usuário confirma sua resposta
	public GameObject life1, life2, life3; // Imagens das vidas, inicialmente são as vidas cheias

	private ExpressaoBasica exp; // Objeto do tipo expressão que contém a expressão e métodos para manipulação da mesma

	private float shakeDuration = 0.01f; // Duração do choacolhar da câmera

	private bool gameOver; // Flag que controla a execução do jogo ou não

	// Use this for initialization
	void Start () {

		print ("entrou no start");
		// Bom, começamos o jogo, então nada de game over ainda né?
		this.gameOver = false; 

		// Deixando a primeira vida sendo exibida. Indepedentemente do modo, pelo menos uma vida deve aparecer.
		this.life1.SetActive (true);

		// Setando as vidas e o tempo do jogador, dependendo do modo de jogo carregado.
		if (Jogador.getJogoAtual() == "precisaoArcade") {
			// Setando as vidas
			this.life2.SetActive (true);
			this.life3.SetActive (true);
			Jogador.setVidas (3);
			// Setando o tempo
			TimeHandler.setTimer(15); 
		}
		if (Jogador.getJogoAtual() == "precisaoTimeAttack") {
			this.life2.SetActive (false);
			this.life3.SetActive (false);
			Jogador.setVidas (1);
			// Setando o tempo
			TimeHandler.setTimer (75);
		}
			
		// Setando a expressão inicial para o jogador
		this.exp = new ExpressaoBasica(Jogador.getExpressoes());
		this.txtExpressao.text = this.exp.Expressao + " = ?";

		// Zerando a caixinha dos pontos
		this.txtPontuacao.text = Jogador.getPontuacao().ToString();
		print("entrou aqui");

	}

	// Funções privadas

	// Gera uma nova expressão e limpa a resposta anterior do jogador
	private void gerarNovaExpressao(){
		this.exp = new ExpressaoBasica(Jogador.getExpressoes());
		this.txtExpressao.text = exp.Expressao + " = ?";
		this.txtEntradaResposta.text = "";
	}

	// Faz a tela balançar, depedendo do número de vidas do jogador 
	private void shake(){ if (Jogador.getVidas () > 0) CameraShakeHandler.TriggerShake(this.shakeDuration); }

	// Chama a tela de game over
	private void chamarGameOver(){
		
		// Muda a cena atual para o do Precisao, não importa se é Arcade ou Time Attack
		Jogador.setCenaAtual("precisaoArcadeTimeAttack");
		// É hora de dar tchau!
		this.gameOver = true;
		// Carrega a cena do Game Over
		SceneManager.LoadScene("gameOver_precisaoArcadeTimeAttack");
	}
		
	// Funções públicas

	// Funções para cada um dos botões, adicionando o removendo elementos
	public void cliqueBotaoNumero1(){ this.txtEntradaResposta.text += "1"; } // Adicionando 1 na resposta 
	public void cliqueBotaoNumero2(){ this.txtEntradaResposta.text += "2"; } // Adicionando 2 na resposta 
	public void cliqueBotaoNumero3(){ this.txtEntradaResposta.text += "3"; } // Adicionando 3 na resposta 
	public void cliqueBotaoNumero4(){ this.txtEntradaResposta.text += "4"; } // Adicionando 4 na resposta 
	public void cliqueBotaoNumero5(){ this.txtEntradaResposta.text += "5"; } // Adicionando 5 na resposta
	public void cliqueBotaoNumero6(){ this.txtEntradaResposta.text += "6"; } // Adicionando 6 na resposta 
	public void cliqueBotaoNumero7(){ this.txtEntradaResposta.text += "7"; } // Adicionando 7 na resposta 
	public void cliqueBotaoNumero8(){ this.txtEntradaResposta.text += "8"; } // Adicionando 8 na resposta 
	public void cliqueBotaoNumero9(){ this.txtEntradaResposta.text += "9"; } // Adicionando 9 na resposta 
	public void cliqueBotaoNumero0(){ this.txtEntradaResposta.text += "0"; } // Adicionando 0 na resposta 
	public void cliqueBotaoNumeroNegativo(){ this.txtEntradaResposta.text += "-"; } // Adicionando - na resposta 
	public void cliqueBotaoApagandoUltimoNumero(){ 
		// Removendo o ultimo caractere da string, fatiando a string até o penultimo elemento
		if (this.txtEntradaResposta.text != "") 
			this.txtEntradaResposta.text = this.txtEntradaResposta.text.Substring (0, txtEntradaResposta.text.Length - 1);
	} 

	// Checando se a resposta tá correta, quando o usuário clicar no botão de confirmar
	public void confirmarResposta(){

		// Checando se a resposta da expressão é a mesma digitada pelo usuário
		if (this.exp.checarResposta (this.txtEntradaResposta.text)) {
			
			// Adicionamos pontos, pois nosso queridíssimo acertou, e a pontuação varia com o modo
			if (Jogador.getJogoAtual() == "precisaoArcade") Jogador.adicionarPontuacao(TimeHandler.getTimer()); // De acordo com o tempo
			if (Jogador.getJogoAtual() == "precisaoTimeAttack") Jogador.adicionarPontuacao(1); // De acordo com a quant. de expressões

			// Adicionando XP para o jogador, fazendo a dificuldade ser aumentada gradativamente 
			Jogador.adicionarExpressoes ();
	
		}
		// Mas nem tudo são flores... 
		else {
			Jogador.tirarVidas(); // Tirando a vida do jogador vacilão por ele ter errado
			Handheld.Vibrate(); // Dá uma vibradinha gostosa ui, só pra avisar que o jogador perdeu uma vida
			shake(); // Se permitido, faz a tela balançar também
		}

		// Setando a nova expressão quando este botão for clicado se o jogador possuir vidas ainda, claro
		if (Jogador.getVidas () > 0) {
			gerarNovaExpressao ();
			// Se estivermos no Precisão-Arcade, podemos reiniciar o tempo quando o jogador errar
			if (Jogador.getJogoAtual() == "precisaoArcade") TimeHandler.resetTimer ();
		}
		// Agora claro, se a vida dele chegar a zero, é preciso chamar a tela de game over, indepedentemente do modo
		else { chamarGameOver (); } 

	}
		
	// Update is called once per frame
	void Update () {

		/* Checando qual é o jogo que o jogador está jogando atualmente
		this.gameLoaded = Jogador.getJogoAtual(); */

		// Enquanto não estivermos no estado de Game Over, podemos jogar!
		if (this.gameOver == false) {
			
			// Testando se o tempo do jogador acabou, o que acarreta perda de vida também, fazendo a mesma coisa lá em cima
			if (!TimeHandler.isLigado ()) {
				Jogador.tirarVidas ();
				Handheld.Vibrate ();
				shake ();

				// Nada muito diferente do que já fizemos ali em cima
				if (Jogador.getVidas () > 0) {
					gerarNovaExpressao ();
					// Se estivermos no Precisão-Arcade, podemos reiniciar o tempo quando o jogador errar
					if (Jogador.getJogoAtual() == "precisaoArcade") TimeHandler.resetTimer ();
				}
				// Agora claro, se a vida dele chegar a zero, é preciso chamar a tela de game over, indepedentemente do modo
				else { chamarGameOver (); } 
			}
				
			// Atualizando a pontuação do cara, toda vez que a pontuação dele mudar surtirá efeito aqui. 
			this.txtPontuacao.text = Jogador.getPontuacao ().ToString (); 

			// Se estivermos no Precisão-Arcade, podemos eliminar as outras vidad
			if (Jogador.getJogoAtual() == "precisaoArcade") {
				if (Jogador.getVidas () == 2) { this.life3.SetActive (false); }
				if (Jogador.getVidas () == 1) { this.life2.SetActive (false); }
			}

			// Mas independemente do modo, se o usuário perde todas as vidas, a primeira vida tem que SUMIU!?
			if (Jogador.getVidas() == 0) { this.life1.SetActive (false); }
	
		}

	}
}