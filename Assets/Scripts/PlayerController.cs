using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 20.0f;
	public int health = 100;
	private Rigidbody2D rigidBody;
	private bool right = true;
	[SerializeField]
	private Animator animator;
	private float horizontal;
	private float vertical;
	[SerializeField]
	private Text healthText;
	public EnumBreadType breadType = EnumBreadType.NORMAL;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		if (vertical != 0f || horizontal != 0f) {
			Move ();
			animator.SetBool ("walking", true);
		} else if (animator.GetBool ("walking")) {
			animator.SetBool ("walking", false);
		}

		if (health <= 50) {
			breadType = EnumBreadType.EATED;
		} else {
			breadType = EnumBreadType.NORMAL;
		}
	}

	void Move () {
		Vector2 force = rigidBody.transform.position;

		force.x += horizontal * Time.deltaTime * speed;
		force.y += vertical * Time.deltaTime * speed;
		rigidBody.MovePosition (force);

		if (horizontal > 0f && !right) {
			Flip ();
			right = true;
		} else if (horizontal < 0f && right) {
			Flip ();
			right = false;
		}
	}

	void Flip () {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Damage (int damage) {
		health -= damage;
		healthText.text = "Health: " + health + "%";
	}

	public enum EnumBreadType{
		NORMAL,
		EATED,
		INFECTED,
		EATED_INFECTED,
		DRYED,
		DRYED_EATED
	}
}
