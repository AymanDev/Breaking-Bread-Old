using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetDrop : MonoBehaviour {

	void Start(){
		Invoke ("SelfDestruct", 30f);
	}

	void SelfDestruct(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag.Equals ("Player")) {
			PlayerController playerController = collider.GetComponent < PlayerController > ();
			playerController.resistCharges = 2;
			Destroy (gameObject);
		}
	}
}
