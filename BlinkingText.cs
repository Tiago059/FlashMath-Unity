using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

	public Text blinkingText; // Texto a ser piscado
	public float duration; // Duração do efeito

	private float timerBlinkDelay; // Timer que gerenciará o tempo a ser piscado
	private long myTurn = 1; // Contador que gerenciará o tempo dado ao efeito

	void Update () {

		/* O contador muda a cada vez que o tempo é zerado, fazendo o texto ser exibido
		   uma hora e na outra hora ficar invisivel */
		if (this.myTurn % 2 == 0){ this.blinkingText.color = new Color32(0, 0, 0, 255); }
		else {this.blinkingText.color = new Color32(0, 0, 0, 0); }

		this.timerBlinkDelay -= Time.deltaTime;

		// Toda vez que o tempo for zerado, mudamos o efeito e reiniciamos o cronômetro
		if (this.timerBlinkDelay <= 0){
			this.myTurn++;
			this.timerBlinkDelay = this.duration;
		}
	}
}