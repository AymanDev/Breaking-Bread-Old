using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {

	public int damage = 50;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag.Equals ("Player")) {
			PlayerController playerController = collider.GetComponent < PlayerController > ();
			playerController.Damage (damage);
		}
	}
}
