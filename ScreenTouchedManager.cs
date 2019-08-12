using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class ScreenTouchedManager : MonoBehaviour {

	private bool isTouched = false;

	void Update () { 
		
		if (Input.anyKeyDown && !isTouched) { 
			AnimationManager.Instance.startAnimationAndLoadScene("FadeIn", "menuPrincipal");
			isTouched = true;
		}
	}

}
