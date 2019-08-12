using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour {

	public Animator transitionAnim;

	public static AnimationManager Instance;

	void Awake() { 
		Instance = this;
	}

	public void startAnimationAndLoadScene(string trigger, string scene){
		StartCoroutine(LoadScene(trigger, scene));
	}

	IEnumerator LoadScene(string trigger, string scene){
		transitionAnim.SetTrigger(trigger);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(scene);
	}


}	
