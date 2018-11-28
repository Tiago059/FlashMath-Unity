using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour {

	// Esta classe é responsável por fazer a movimentação da câmera. Não foi idelizado por mim. 
	// Referência: https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516

	public Transform camTransform; // Atributo da câmera - arraste sua câmera para o script

	private static float shakeDuration; // Duranção do shake

	private float shakeMagnitude = 0.5f; // Magnitude, impacto do shake

	private float dampingSpeed = 0.1f; // Medida do quanto o efeito pode durar

	Vector3 initialPosition; // Posição inicial do Game Object

	//void Awake() { if (transform == null) { transform = GetComponent(typeof(Transform)) as Transform; } }
	void OnEnable() { initialPosition = this.camTransform.localPosition; }

	void Update() {
		if (shakeDuration > 0) {
			camTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
			CameraShakeHandler.shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			CameraShakeHandler.shakeDuration = 0f;
			camTransform.localPosition = initialPosition;
		}
	}

	// Use esta função para fazer a tela balançar
	public static void TriggerShake(float duration){ CameraShakeHandler.shakeDuration = duration; }

}