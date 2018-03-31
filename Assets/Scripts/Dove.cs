using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dove : MonoBehaviour {

	Rigidbody2D rigidBody;

	[SerializeField]
	private float timeForRandomMovement = 2f;

	public int phase = 0;
	[SerializeField]
	private float speedPush = 20f;

	private bool right = true;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();	
		InvokeRepeating ("RandomMovement", 0f, timeForRandomMovement);
	}

	void RandomMovement () {
		if (phase == 0) {
			int side = Random.Range (0, 100);
			Vector2 force = new Vector2 (-speedPush, 200f);

			if (side < 50) {
				force = new Vector2 (speedPush, 200f);
				if (!right) {
					Flip ();
					right = true;
				}
			} else if (right) {
				Flip ();
				right = false;
			}
			rigidBody.AddForce (force);
		}
	}

	void Flip () {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	enum EnumDoveStatus {
		NEUTRAL,
		AGRESSIVE,
		RAGE,
		TIRED,
		SAD,
		HUNGRY,
		INFECTED
	}
}
