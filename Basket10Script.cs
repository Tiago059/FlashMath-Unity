using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class Basket10Script : MonoBehaviour {

	public Text txtPontuacao; // Texto aonde aparecem a pontuação do caboco

	public Text infoCombo; // Texto que aparece quantos acertos em sequência o jogador teve
	public Text infoBonus; // Representa o bônus que o jogador ganha por acertar em sequência
	private int comboCounter; // Conta quantos acertos em sequência o jogador teve, seus combos
	private int bonusLimit; // Representa a quantidade mínima de combo para que seja feita a bonificação

	public GameObject life1, life2, life3; // Imagens das vidas, inicialmente são as vidas cheias

	private float shakeDuration = 0.01f; // Duração do choacolhar da câmera

	private bool gameOver; // Flag que controla a execução do jogo ou não

	// Use this for initialization
	void Start () {

		Debug.Log("sa");

		// Começo de jogo, ele ainda não acertou nenhuma, não há combo nem bônus NEM NADA POHA
		this.comboCounter = 0; 
		this.infoCombo.text = "";
		this.infoBonus.text = "";

		// Todas as vidas aqui são exibidas...
		this.life1.SetActive(true);
		this.life2.SetActive(true);
		this.life3.SetActive(true);

		// Setando as vidas
		Jogador.setVidas (3);
		// Setando o tempo
		TimeHandler1.setTimer(15);
		// Setando o bonus
		this.bonusLimit = 10;

		// Zerando a caixinha dos pontos
		this.txtPontuacao.text = Jogador.getPontuacao().ToString();

	}

	/*private void gerarCesta10(){
		O objetivo aqui deste método é preencher os números em cada cesta, além de 
		escolher uma das cestas a se ter o resultado 10. A grandeza dos números, por
		assim dizer, se baseia na quantidade de expressões atuais do jogador.  

		 Para isso, usaremos objetos que nos retornarão expressões e gerenciaração
		   quais cestas têm os resultados que queremos. 

		// 1: Selecionamos a cesta que terá o resultado 10, que ficará marcada como a correta
		int cesta = ExpressaoBasket10.randomNumCesta(this.basketIsSelect);
		this.basketIsSelect[cesta - 1] = true; 
		this.basketIsCorrect[cesta - 1] = true;
		this.numCestaCorreta = cesta;

		// 2: Tendo a cesta em mãos, colocamos os números escolhidos na cesta escolhida
		int[] numerosDesejados = ExpressaoBasica.gerarExpressaoDesejada(Jogador.getExpressoes(), 10);
		this.baskets[cesta - 1][0] = numerosDesejados[0].ToString();
		this.baskets[cesta - 1][1] = numerosDesejados[1].ToString();

		// 3: Agora selecionamos os outros números das outras cestas, não sobrescrevendo os já selecionados
		// Gerando a cesta que dará resultado 9
		cesta = ExpressaoBasket10.randomNumCesta(this.basketIsSelect);
		this.basketIsSelect[cesta - 1] = true;
		numerosDesejados = ExpressaoBasica.gerarExpressaoDesejada(Jogador.getExpressoes(), 9);
		this.baskets[cesta - 1][0] = numerosDesejados[0].ToString();
		this.baskets[cesta - 1][1] = numerosDesejados[1].ToString();

		// Gerando a cesta que dará resultado 11
		cesta = ExpressaoBasket10.randomNumCesta(this.basketIsSelect);
		this.basketIsSelect[cesta - 1] = true;
		numerosDesejados = ExpressaoBasica.gerarExpressaoDesejada(Jogador.getExpressoes(), 11);
		this.baskets[cesta - 1][0] = numerosDesejados[0].ToString();
		this.baskets[cesta - 1][1] = numerosDesejados[1].ToString();

		// Gerando a cesta que dará resultado 12
		cesta = ExpressaoBasket10.randomNumCesta(this.basketIsSelect);
		this.basketIsSelect[cesta - 1] = true;
		numerosDesejados = ExpressaoBasica.gerarExpressaoDesejada(Jogador.getExpressoes(), 12);
		this.baskets[cesta - 1][0] = numerosDesejados[0].ToString();
		this.baskets[cesta - 1][1] = numerosDesejados[1].ToString();

		// Com a lista de strings preenchida, retornamos seus valores aos objetos Text
		this.basket1Num1.text = this.baskets[0][0];
		this.basket1Num2.text = this.baskets[0][1];
		this.basket2Num1.text = this.baskets[1][0];
		this.basket2Num2.text = this.baskets[1][1];
		this.basket3Num1.text = this.baskets[2][0];
		this.basket3Num2.text = this.baskets[2][1];
		this.basket4Num1.text = this.baskets[3][0];
		this.basket4Num2.text = this.baskets[3][1];
	}

	public void clickCesta1(){
		if (1 == this.numCestaCorreta){ Debug.Log("acertou"); }
	}*/

	// Faz a tela balançar, depedendo do número de vidas do jogador 
	private void shake(){ if (Jogador.getVidas () > 0) CameraShakeHandler.TriggerShake(this.shakeDuration); }

	// Chama a tela de game over
	private void chamarGameOver(){
		
		// Muda a cena atual para o do Precisao, mas do Basket10, para o game over reiniciar neste game
		Jogador.setCenaAtual("PrecisaoBasket10");
		// É hora de dar tchau!
		this.gameOver = true;
		// Carrega a cena do Game Over
		SceneManager.LoadScene("gameOverPrecisao");
	}

	// Chama a tela de game over, após o jogador clicar no botão de desistir
	public void giveUpClick(){ chamarGameOver(); }

	// Update is called once per frame
	void Update () {
		// Enquanto não estivermos no estado de Game Over, podemos jogar! (UAU)
		if (this.gameOver == false) {

			// Aqui controlamos os delays e os cronômetros secundários, dependendo do modo
			if (Jogador.getJogoAtual() == "precisaoArcade"){
				// Se não tiver delay, então não temos nenhuma mensagem
				if (!TimeHandler2.ligadoDelay) this.infoBonus.text = "";
			}

			// Testando se o tempo do jogador acabou, o que acarreta perda de vida também, fazendo a mesma coisa lá em cima
			if (!TimeHandler1.isLigado ()) {
				// Tocamos um efeito sonoro de erro... :/
				SoundEffectManager.Instance.playSong("wrongResponse");

				Jogador.tirarVidas ();
				Handheld.Vibrate ();
				shake();
			}

			// Atualizando a pontuação do cara, toda vez que a pontuação dele mudar surtirá efeito aqui. 
			this.txtPontuacao.text = Jogador.getPontuacao ().ToString (); 

			// Exibindo as vidas do jogador, dependendo do modo
			if (Jogador.getVidas () == 2) { this.life3.SetActive (false); }
			if (Jogador.getVidas () == 1) { this.life2.SetActive (false); }
			if (Jogador.getVidas() == 0) { this.life1.SetActive (false); }
		}
	}
}
