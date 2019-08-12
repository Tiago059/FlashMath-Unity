using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {
	/*
		Essa classe é responsável por fazer o offset dos materiais mudar,
		dando um efeito de movimentação. 
	*/

	private Material materialAtual; 
	public float velocidade; 
	private float offset; 

	// Use this for initialization
	void Start () {

		materialAtual = GetComponent<Renderer> ().material; 
	}
	
	// Update is called once per frame
	void Update () {

		offset += 0.001f;
		materialAtual.SetTextureOffset ("_MainTex", new Vector2 (offset * velocidade, 0));

	}
}