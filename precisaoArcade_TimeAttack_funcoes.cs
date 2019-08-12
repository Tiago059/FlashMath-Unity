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

	public Text infoCombo; // Texto que aparece quantos acertos em sequência o jogador teve
	public Text infoBonus; // Representa o bônus que o jogador ganha por acertar em sequência
	private int comboCounter; // Conta quantos acertos em sequência o jogador teve, seus combos
	private int bonusLimit; // Representa a quantidade mínima de combo para que seja feita a bonificação

	public Button btnConfirmarResposta; // Botão aonde o usuário confirma sua resposta
	public GameObject life1, life2, life3; // Imagens das vidas, inicialmente são as vidas cheias

	private ExpressaoBasica exp; // Objeto do tipo expressão que contém a expressão e métodos para manipulação da mesma

	private float shakeDuration = 0.01f; // Duração do choacolhar da câmera

	private bool gameOver; // Flag que controla a execução do jogo

	// Use this for initialization
	void Start () {

		// Bom, começamos o jogo, então nada de game over ainda né?
		this.gameOver = false; 

		// Deixando a primeira vida sendo exibida. Indepedentemente do modo, pelo menos uma vida deve aparecer.
		this.life1.SetActive (true);

		// Começo de jogo, ele ainda não acertou nenhuma, não há combo nem bônus NEM NADA POHA
		this.comboCounter = 0; 
		this.infoCombo.text = "";
		this.infoBonus.text = "";
	
		// Setando as vidas e o tempo do jogador, dependendo do modo de jogo carregado.
		if (Jogador.getJogoAtual() == "precisaoArcade") {
			// Setando as vidas
			this.life2.SetActive (true);
			this.life3.SetActive (true);
			Jogador.setVidas (3);
			// Setando o tempo
		 	TimeHandler1.setTimer(15);
		 	// Setando o bonus
		 	this.bonusLimit = 10;

		}
		if (Jogador.getJogoAtual() == "precisaoTimeAttack") {
			// Setando as vidas
			this.life2.SetActive (false);
			this.life3.SetActive (false);
			Jogador.setVidas (1);
			// Setando o tempo
			TimeHandler1.setTimer(90); // Cronômetro principal
			TimeHandler2.setTimer(9); // Cronômetro oculto, serve para dar a bonificação
			this.bonusLimit = 3;
		
		}
			
		// Setando a expressão inicial para o jogador
		this.exp = new ExpressaoBasica(Jogador.getExpressoes());
		this.txtExpressao.text = this.exp.Expressao + " = ?";

		// Zerando a caixinha dos pontos
		this.txtPontuacao.text = Jogador.getPontuacao().ToString();

	}

	/* ---------------------------- Funções privadas ---------------------------------- */

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
		SceneManager.LoadScene("gameOverPrecisao");
	}

	// Chama a tela de game over, após o jogador clicar no botão de desistir
	public void giveUpClick(){ chamarGameOver(); }
		
	/* ---------------------------- Funções públicas ---------------------------------- */

	// Funções para cada um dos botões, adicionando ou removendo elementos
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
			this.txtEntradaResposta.text = this.txtEntradaResposta.text.Substring(0, txtEntradaResposta.text.Length - 1);
	} 

	// Checando se a resposta tá correta, quando o usuário clicar no botão de confirmar
	public void confirmarResposta(){

		// Checando se a resposta da expressão é a mesma digitada pelo usuário
		if (this.exp.checarResposta (this.txtEntradaResposta.text)) {

			// Tocamos um efeito sonoro de acerto :)
			SoundEffectManager.Instance.playSong("correctResponse");

			// Adicionamos pontos, pois nosso queridíssimo acertou, e a pontuação varia com o modo
			if (Jogador.getJogoAtual() == "precisaoArcade") {
				// Adicionando pontos igual ao tempo restante na tela
				Jogador.adicionarPontuacao(TimeHandler1.getTimer());
				// Contando seus combos
				this.comboCounter++;
				// Se a quantidade de combos é maior que o limitador, podemos exibir
				if (this.comboCounter >= this.bonusLimit) {
					this.infoCombo.text = "Combo X" + this.comboCounter + "!";
				}
			}
			if (Jogador.getJogoAtual() == "precisaoTimeAttack") {
				// Bom, cada acerto só dá um ponto mesmo neste modo
				Jogador.adicionarPontuacao(1); 
				/* Se seus combos ainda não atingiram a quantidade do limitador mas ainda o tempo 
				   de 9 segundos ainda não se esgotou, acumulamos */
				if (this.comboCounter < this.bonusLimit && TimeHandler2.isLigado()){ 
					// Mais um combo!
					this.comboCounter++;
					// E exibimos sua quantidade de combos atuais
					this.infoCombo.text = "Combo X" + this.comboCounter + "!";
				}
				/* Se o combo agora for igual ao limitador, adicionamos tempo extra e 
				   zeramos esse acumulador para a próxima execução */
				if (this.comboCounter == 3){
					// Zeramos o acumulador
					this.comboCounter = 0; 
					// Adicionando tempo extra (AEOOOO)
					TimeHandler1.addTime(3); 
					// Agora iniciamos um delay de 1 segundo para exibir uma mensagem e depois sumir
					TimeHandler2.timerDelay = 1;
					TimeHandler2.ligadoDelay = true;
					// Reiniciamos o cronômetro de 9 segundos para a próxima execução
					TimeHandler2.resetTimer(); 
				}
				// Mas se o tempo de 9 segundos estourou, zeramos o acumulador e reiniciamos o cronômetro
				if (!TimeHandler2.isLigado()){
					// Diga adeus ao seu combo...
					this.comboCounter = 0;
					// Inclusive, sua mensagem bonitinha também já era
					this.infoCombo.text = ""; 
					// Mas reinicio o cronômetro de 9 segundos para você tentar denovo
					TimeHandler2.resetTimer();
				}

			}

			// Adicionando XP para o jogador, para a dificuldade ser aumentada gradativamente (MUWAWA)
			Jogador.adicionarExpressoes ();
	
		}
		// Mas nem tudo são flores... 
		else {
			// Tocamos um efeito sonoro de erro... :/
			SoundEffectManager.Instance.playSong("wrongResponse");

			/* No caso do Precisão-Arcade, o jogador quando erra recebe pontos extras 
			   igual ao seu combo, mas apenas se ele superar o limitador */
			if (Jogador.getJogoAtual() == "precisaoArcade" && this.comboCounter >= this.bonusLimit){
				// Outro delay para exibir a mensagem de bonificação
				TimeHandler2.timerDelay = 1;
				TimeHandler2.ligadoDelay = true;
				this.infoBonus.text = "Bonus: +" + this.comboCounter + " pts!";
				// E adicionamos os pontos extras
				Jogador.adicionarPontuacao(this.comboCounter);
			}
			// Errou papai, diga adeus aos seus combos, independemente do modo
			this.comboCounter = 0;
			// O texto também vai pro beleléu
			this.infoCombo.text = ""; 
			// Tirando a vida do jogador vacilão por ele ter errado
			Jogador.tirarVidas(); 
			// Dá uma vibradinha gostosa ui, só pra avisar que o jogador perdeu uma vida
			Handheld.Vibrate(); 
			// Se permitido, faz a tela balançar também
			shake(); 
		}

		// Setando a nova expressão quando este botão for clicado se o jogador possuir vidas ainda, claro
		if (Jogador.getVidas () > 0) {
			gerarNovaExpressao();
			// Se estivermos no Precisão-Arcade, podemos reiniciar o tempo quando o jogador errar
			if (Jogador.getJogoAtual() == "precisaoArcade") TimeHandler1.resetTimer ();
		}
		// Se a vida dele chegar a zero, chamamos a tela de game over, indepedentemente do modo
		else { chamarGameOver (); } 

	}
		
	// Update is called once per frame
	void Update () {

		// Enquanto não estivermos no estado de Game Over, podemos jogar! (UAU)
		if (this.gameOver == false) {

			// Aqui controlamos os delays e os cronômetros secundários, dependendo do modo
			if (Jogador.getJogoAtual() == "precisaoArcade"){
				// Se não tiver delay, então não temos nenhuma mensagem
				if (!TimeHandler2.ligadoDelay) this.infoBonus.text = "";
			}
			if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
				/* O cronometro de delay estará ligado ao acertar o terceiro combo,
				   então a mensagem deve ser exibida */
				if (TimeHandler2.ligadoDelay){
					this.infoCombo.text = "Combo X3!";
					this.infoBonus.text = "+3 sec!";
				}
				// Depois ele desliga e o combo zera, apagando também as mensagens
				else if (!TimeHandler2.ligadoDelay && this.comboCounter == 0) { 
					this.infoCombo.text = "";
					this.infoBonus.text = ""; 
				}

				// Se o tempo de 9 segundos acabar... nada de combos!
				if (!TimeHandler2.isLigado()){
					this.comboCounter = 0;
					TimeHandler2.resetTimer();
				}
			}

			// Testando se o tempo do jogador acabou, o que acarreta perda de vida também, fazendo a mesma coisa lá em cima
			if (!TimeHandler1.isLigado ()) {
				// Tocamos um efeito sonoro de erro... :/
				SoundEffectManager.Instance.playSong("wrongResponse");

				Jogador.tirarVidas ();
				Handheld.Vibrate ();
				shake();

				// Nada muito diferente do que já fizemos ali em cima
				if (Jogador.getVidas () > 0) {
					gerarNovaExpressao ();
					// Se estivermos no Precisão-Arcade, podemos reiniciar o tempo quando o jogador errar
					if (Jogador.getJogoAtual() == "precisaoArcade"){ 
						// Reiniciamos o cronômetro
						TimeHandler1.resetTimer();
						// Adeus mais uma vez ao seu combo
						this.infoCombo.text = "";
						// Assim como lá em cima, o jogador pode receber bonus quando perde uma vida
						if (this.comboCounter >= this.bonusLimit) {
							TimeHandler2.timerDelay = 1;
							TimeHandler2.ligadoDelay = true;
							this.infoBonus.text = "Bonus: +" + this.comboCounter + " pts!";
							Jogador.adicionarPontuacao(this.comboCounter);
							this.comboCounter = 0;
						}
					}
				}
				// Chamando game over mais uma vez, caso sem vidas
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