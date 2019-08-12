using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler2: MonoBehaviour {

	public Text cronometro;
	private static float timer; // Temporizador na sua forma bruta
	private static float timer_backup; // Backup do tempo original
	private static int tempoTexto; // Temporizador já na forma pronta para ser exibida
	private static bool ligado = true; 

	// Variáveis referente a um contador que tem como objetivo exibir algo por X segundos
	public static float timerDelay = 1; // Temporizador na sua forma bruta
	public static int tempoTextoDelay;
	public static bool ligadoDelay = false; 

	void Start(){

		TimeHandler2.timer = TimeHandler2.timer_backup;
		TimeHandler2.ligado = true;

	}

	// Update is called once per frame
	void Update () {

		if (TimeHandler2.ligado) {
	
			TimeHandler2.timer -= Time.deltaTime; // Subtraindo o delta time
			TimeHandler2.tempoTexto = (int) TimeHandler2.timer; // Convertendo para Int, que permite uma visualização melhor

			if (TimeHandler2.timer < 0) { TimeHandler2.ligado = false; }
		}

		// Referente ao cronômetro delay, funciona idêntico ao outros cronômetros
		if (TimeHandler2.ligadoDelay) {
			TimeHandler2.timerDelay -= Time.deltaTime;
			TimeHandler2.tempoTextoDelay = (int) TimeHandler2.timerDelay;
			if (TimeHandler2.timerDelay < 0) { TimeHandler2.ligadoDelay = false; }
		}

	}

	public static void setTimer(float timer){ 
		TimeHandler2.timer = timer + 1;
		TimeHandler2.timer_backup = TimeHandler2.timer;
	}

	public static void addTime(float time){ 
		TimeHandler2.timer += time; 
	}

	public static bool isLigado(){ 
		return TimeHandler2.ligado; 
	}

	public static int getTimer(){
		return TimeHandler2.tempoTexto; 
	}

	public static void turnOnTimer() { 
		TimeHandler2.ligado = true; 

	}
	public static void resetTimer(){ 
		TimeHandler2.timer = TimeHandler2.timer_backup;
		TimeHandler2.turnOnTimer();
	}		
}