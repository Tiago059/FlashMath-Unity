using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class precisaoArcadeTimeAttackMusic : MonoBehaviour {
	/*
		Esta classe é responsável por gerenciar as músicas que tocam nos modos de jogo
		Arcade e TimeAttack do Precision. O principal motivo de elas estarem separadas é
		porque cada música tem que ser gerenciada para cada cena, pois um gameObject de uma
		cena não fica compartilhado para as outras cenas. 
	*/

	public AudioClip arcadeMusic; // Música do modo Precisao-Arcade
	public AudioClip timeAttackMusic; // Música do modo Precisao-Time Attack

	private AudioSource audioManager; // AudioSource que é responsável por tocar a música

	// Use this for initialization
	void Start () {

		// Retiramos o AudioManager que está atrelado ao Unity
		this.audioManager = GetComponent<AudioSource>();

		// Sem muito mistério, dependendo do modo de jogo atual, a determinada música toca
		if (Jogador.getJogoAtual() == "precisaoArcade"){
			this.audioManager.clip = this.arcadeMusic;
			this.audioManager.Play();
		}
		else if (Jogador.getJogoAtual() == "precisaoTimeAttack"){
			this.audioManager.clip = this.timeAttackMusic;
			this.audioManager.Play();	
		}
	}
}