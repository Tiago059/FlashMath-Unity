using UnityEngine;
using UnityEngine.SceneManagement;

public class comandosBasicos : MonoBehaviour {

	public void carregarCena(string cena){
		RenderSettings.ambientSkyColor = Color.black;
	 	SceneManager.LoadScene (cena); 
	}

	public void fecharAplicativo(){ Application.Quit (); }

}
