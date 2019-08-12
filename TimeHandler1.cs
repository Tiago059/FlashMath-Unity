using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler1: MonoBehaviour {

	/* Observe que essa classe possui atributos e métodos static, logo seus valores não mudam entre classes e outras. 
	 * Mas o atributo crônometro é público. Porquê? Observe que temos um Update() aqui, junto com o Update() da função
	 * da outra classe. Para que as duas trabalhem juntas é necessário que este script seja acoplado do _GameController,
	 * deixando cronometro público deixamos você usar o editor do Unity para colocar o cronometro que deixa o codigo mais
	 * organizado e limpo, mas principalmente, faz com que você possa maniuplar o cronometro aqui em outras classes usando
	 * modificando os atributos static. Outra coisa é que classes que são anexadas ao Unity em algum objeto não podem ser
	 * abstratas, senão o Update jamais funcionaria. Você sempre seguirá esse esquema de script sempre que precisar anexar
	 * um objeto que tenha que trabalhar com um Update() paralelo ao Update() de outros scripts.
	*/

	/* Este cronômetro é usado como cronômetro principal de todos os modos de Precisão
	   Ele contém um cronômetro extra que é usado para fazer o texto do cronômetro piscar
	   quando o tempo é menor que 6 segundos. 
	*/

	public Text cronometro; // Texto do cronômetro
	public static float timer; // Temporizador na sua forma bruta
	private static float timer_backup; // Backup do tempo original
	private static int tempoTexto; // Temporizador já na forma pronta para ser exibida
	public static bool ligado = true;
	// Muda a cor do cronômetro para mostrar que o tempo está acabando


	void Start(){ TimeHandler1.timer = TimeHandler1.timer_backup; }

	// Cronômetro referente a fazer o texto piscar
	private float timerBlinkDelay;
	private bool ligadoBlinkDelay = false;

	// Update is called once per frame
	void Update () {

		if (TimeHandler1.ligado) {

			TimeHandler1.timer -= Time.deltaTime; // Subtraindo o delta time
			TimeHandler1.tempoTexto = (int) TimeHandler1.timer; // Convertendo para Int, que permite uma visualização melhor

			// Fazendo o texto piscar, quando o tempo chegar a 5 segundos
			if (TimeHandler1.tempoTexto < 6){
				this.ligadoBlinkDelay = true;
			}
			else { this.cronometro.color = Color.white; }

			// Imprimindo o cronometro, dependendo do tempo
			if (TimeHandler1.tempoTexto >= 60) {
				int minutos = (TimeHandler1.tempoTexto / 60);
				this.cronometro.text = minutos.ToString ();

				if (TimeHandler1.tempoTexto - 60 < 10) {
					this.cronometro.text += ":0";
				} else {
					this.cronometro.text += ":"; 
				}

				this.cronometro.text += (TimeHandler1.tempoTexto - 60).ToString ();
			} else {
				this.cronometro.text = "0:" + TimeHandler1.tempoTexto.ToString ();
				if (TimeHandler1.tempoTexto < 10)
					this.cronometro.text = "0:0" + TimeHandler1.tempoTexto.ToString ();
				if (TimeHandler1.timer < 0){
					TimeHandler1.ligado = false;
				}
			}

			// O 0 no 4º argumento representa a transparência
			if (this.ligadoBlinkDelay){
				this.cronometro.color = new Color32(255, 255, 255, 0); 
				this.timerBlinkDelay -= Time.deltaTime;
				if (this.timerBlinkDelay <= 0){
					this.cronometro.color = new Color32(255, 255, 255, 255); 
					this.ligadoBlinkDelay = false;
					this.timerBlinkDelay = 0.1f;
				}
			}
		}
	}

	public static void setTimer(float timer){  
		if (timer >= 1){
			TimeHandler1.timer = timer + 1;
		}
		else TimeHandler1.timer = timer;

		TimeHandler1.timer_backup = TimeHandler1.timer;
	}

	public static void addTime(float time){ 
		TimeHandler1.timer += time; 
	}

	public static bool isLigado(){ 
		return TimeHandler1.ligado; 
	}

	public static int getTimer(){
		return TimeHandler1.tempoTexto; 
	}

	public static void turnOnTimer() { 
		TimeHandler1.ligado = true; 

	}

	public static void turnOffTimer(){ TimeHandler1.ligado = false; }
	public static void resetTimer(){ 
		TimeHandler1.timer = TimeHandler1.timer_backup;
		TimeHandler1.turnOnTimer();
	}
}