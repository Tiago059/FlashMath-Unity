using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour {

	/* Observe que essa classe possui atributos e métodos static, logo seus valores não mudam entre classes e outras. 
	 * Mas o atributo crônometro é público. Porquê? Observe que temos um Update() aqui, junto com o Update() da função
	 * da outra classe. Para que as duas trabalhem juntas é necessário que este script seja acoplado do _GameController,
	 * deixando cronometro público deixamos você usar o editor do Unity para colocar o cronometro que deixa o codigo mais
	 * organizado e limpo, mas principalmente, faz com que você possa maniuplar o cronometro aqui em outras classes usando
	 * modificando os atributos static. Outra coisa é que classes que são anexadas ao Unity em algum objeto não podem ser
	 * abstratas, senão o Update jamais funcionaria. Você sempre seguirá esse esquema de script sempre que precisar anexar
	 * um objeto que tenha que trabalhar com um Update() paralelo ao Update() de outros scripts.
	*/
	private static float timer, timer_backup; // Backup do tempo original
	private static int tempoTexto;
	private static bool ligado = true;
	public Text cronometro;

	void Start(){
		TimeHandler.timer_backup = TimeHandler.timer;
	}
	// Update is called once per frame
	void Update () {

		if (TimeHandler.ligado) {
			
			TimeHandler.timer -= Time.deltaTime; // Subtraindo o delta time
			TimeHandler.tempoTexto = (int) TimeHandler.timer; // Convertendo para Int, que permite uma visualização melhor

			// Imprimindo o cronometro, dependendo do tempo
			if (TimeHandler.tempoTexto >= 60) {
				int minutos = (TimeHandler.tempoTexto / 60);
				this.cronometro.text = minutos.ToString ();

				if (TimeHandler.tempoTexto - 60 < 10) {
					this.cronometro.text += ":0";
				} else {
					this.cronometro.text += ":"; 
				}

				this.cronometro.text += (TimeHandler.tempoTexto - 60).ToString ();
			} else {
				this.cronometro.text = "0:" + TimeHandler.tempoTexto.ToString ();
				if (TimeHandler.tempoTexto < 10)
					this.cronometro.text = "0:0" + TimeHandler.tempoTexto.ToString ();
				if (TimeHandler.timer < 0)
					TimeHandler.ligado = false;
			}

		}
		
	}

	public static void setTimer(float timer){ 
		TimeHandler.timer = timer + 1; 
		TimeHandler.timer_backup = TimeHandler.timer;
	}
	public static bool isLigado(){ return TimeHandler.ligado; }
	public static int getTimer(){ return TimeHandler.tempoTexto; }
	public static void turnOnTimer() { TimeHandler.ligado = true; }
	public static void resetTimer(){ 
		TimeHandler.timer = TimeHandler.timer_backup; 
		TimeHandler.turnOnTimer ();
	}
		
}