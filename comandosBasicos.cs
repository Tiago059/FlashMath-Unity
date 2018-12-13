using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class comandosBasicos : MonoBehaviour {

	public void carregarCena(string cena){ SceneManager.LoadScene (cena); }

	public void fecharAplicativo(){ Application.Quit (); }

	public void printe (){
		print ("foi");
	}

	public void deleteSeetings(){ 
		//PlayerPrefs.DeleteKey("highscores"); 
	}
}
