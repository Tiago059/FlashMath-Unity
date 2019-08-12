using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

	/*
		Este script gerencia todos os sons "one-shot", ou seja, aqueles que não são utilizados
		em loop. Para usá-los, coloque esse script vinculado ao um GameObject em todas as cenas
		que necessitarem usar um ou mais dos efeitos sonoros presentes aqui. Para tocá-los, 
		use a chamada "SoundEffectManager.Instance.playSound(nomeDaMúsica)" em qualquer 
		script que seja utlizada nessa cena. 

		Para músicas em loop, vá na cena que você quer que tenha a música e crie um 
		GameObject e anexe a ele um componente do tipo AudioSource. Coloque a música em questão
		lá, e marque as opções "playOnAwake"(para tocar assim que a cena é chamada) e 
		"loop" (nem preciso dizer né...). Só que assim que você mudar de cena, a música é
		parada imediatamente. Para saber como manter uma música tocando através das cenas, 
		consulte o código presente no GameObject "TitleMenuMusic", na cena "telaTitulo".

	*/

	// Criamos uma instância da classe SoundManager, para que possa ser utilizado por outras classes
	public static SoundEffectManager Instance;


	/* ----------------- Lista de Efeitos Sonoros do Jogo --------------------- */

	public AudioClip correctResponse; // Toca ao acertar uma resposta
	public AudioClip wrongResponse; // Toca ao errar uma questao

	// Use this for initialization
	void Awake () {
		// Checando se a instância é nula e em seguinda referindo ela a classe
		if (Instance != null) Debug.LogError("erro no instance");
		Instance = this;
	}

	// Essa função, como o nome diz né , toca um efeito sonoro, basta que seja passado o nome
	public void playSong(string songName){
		// AudioClip que representa a música a ser tocada
		AudioClip musicToPlay;

		// Checando qual música deve ser tocada, dependendo do nome que tiver sido passado
		switch (songName){
			case "correctResponse":
				musicToPlay = this.correctResponse;
				break;
			case "wrongResponse":
				musicToPlay = this.wrongResponse;
				break;
			default:
				Debug.Log("Não encontrou a música.");
				musicToPlay = null;
				break;
		}

		// Se uma música foi realmente setada, então nós a tocamos
		// Observe que esse método chama um método estático do AudioSource, já que em nenhum
		// momento a gente usa um objeto do tipo AudioSource
		if (musicToPlay != null){ AudioSource.PlayClipAtPoint(musicToPlay, transform.position); }
	}
}