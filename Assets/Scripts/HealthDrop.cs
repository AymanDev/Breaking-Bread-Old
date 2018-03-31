using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour {

	public int health = 50;

	void Start () {
		Invoke ("SelfDestruct", 30f);
	}

	void SelfDestruct () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag.Equals ("Player")) {
			PlayerController playerController = collider.GetComponent < PlayerController > ();
			if (playerController.health < 100) {
				if (playerController.health + health > 100) {
					playerController.health = 100;
				} else {
					playerController.health += health;
					playerController.Damage (0, false);
				}
				Destroy (gameObject);
			}
		}
	}
}
