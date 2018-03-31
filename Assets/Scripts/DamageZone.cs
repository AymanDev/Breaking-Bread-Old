using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {

	public int damage = 50;
	public float secondsToRemove = 5f;
	public bool infecting = false;

	void Start () {
		Debug.Log ("start");
		if (secondsToRemove > 0f) {
			Invoke ("SelfDestruct", secondsToRemove);
		}
	}

	void SelfDestruct () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag.Equals ("Player")) {
			PlayerController playerController = collider.GetComponent < PlayerController > ();
			if (infecting) {
				playerController.Infect ();
			}
			playerController.Damage (damage);
		}
	}
}
