using System;
using UnityEngine;

/*
	Esta classe é responsável por guardar algumas configurações relacionadas ao jogador e ao 
	próprio sistema do jogo, como o gerenciamento das músicas que são tocadas de fundo. 
*/

public abstract class PlayerSeetings: MonoBehaviour {

	/* --------------------------- MÚSICAS DE FUNDO ------------------------------- */
	private static bool bgmIsPlaying = false;
	private static bool bgmObjectExists = false;
	private static AudioSource bgmObject;

	// Getters e Setters
	public static bool getBgmIsPlaying() { return PlayerSeetings.bgmIsPlaying; }
	public static void setBgmIsPlaying(bool b) { PlayerSeetings.bgmIsPlaying = b; }

	public static bool getBgmObjectExists() { return PlayerSeetings.bgmObjectExists; }
	public static void setBgmObjectExists(bool b) { PlayerSeetings.bgmObjectExists = b; }

	public static AudioSource getBgmObject(){ return PlayerSeetings.bgmObject; }
	public static void setBgmObject(AudioSource audioS){ PlayerSeetings.bgmObject = audioS; }

	/* --------------------- TRANSIÇÕES FADE IN / FADE OUT ------------------------- */
	private static bool FadeOutTelaTitulo;
	private static bool FadeInMenuPrincipal;

	// Getters e Setters
	public static bool getFadeInMenuPrincipal(){ return PlayerSeetings.FadeInMenuPrincipal; }
	public static void setFadeInMenuPrincipal(bool b){ PlayerSeetings.FadeInMenuPrincipal = b; }

	public static bool getFadeOutTelaTitulo(){ return PlayerSeetings.FadeOutTelaTitulo; }
	public static void setFadeOutTelaTitulo(bool b){ PlayerSeetings.FadeOutTelaTitulo = b; }

}