using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

	public EnumWeatherType weatherType = EnumWeatherType.CLOUDY;

	public GameObject rainObject;
	public GameObject spawnZone;

	[SerializeField]
	private GameObject pitPrefab;
	[SerializeField]
	private GameObject poolPrefab;

	public GameObject attackZone;
	public GameObject secondAttackZone;
	public GameObject secondAttackZoneDamage;
	public GameObject doveBomb;


	public GameObject defaultDove;
	public GameObject flyingDove;
	public GameObject attackingDove;

	public float timeBetweenFly = 20f;
	public Animator doveAnimator;

	public float pitChance = 10f;

	public GameObject fogObject;

	public GameObject mealPrefab;
	public GameObject helmetPrefab;

	void Start () {
		InvokeRepeating ("RandomEvent", 15f, 30f);
		InvokeRepeating ("RandomObject", 0f, 10f);
		Invoke ("Fly", timeBetweenFly);
		Invoke ("SecondAttack", 10f);
	}

	void Fly () {
		GameObject doveObject = GameObject.Find ("DoveBoss");
		GameObject playerObject = GameObject.Find ("Player");
		doveObject.GetComponent<Dove> ().phase = 1;
		float x = playerObject.transform.position.x;

		Vector3 newPos = playerObject.transform.position;
		newPos.Set (x, doveObject.transform.position.y, doveObject.transform.position.z);
		doveObject.transform.position = newPos;
		doveAnimator.SetBool ("attacking", true);
		/*defaultDove.SetActive (false);
		flyingDove.SetActive (true);*/
		attackZone.transform.position = playerObject.transform.position;
		Invoke ("Attack", 1.5f);
	}

	void Attack () {

		/*flyingDove.SetActive (false);
		attackingDove.SetActive (true);*/
		/*Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
		position.z = 100;*/

		attackZone.SetActive (true);
		Invoke ("Land", 0.7f);
	}

	void Land () {
		/*attackZone.SetActive (false);
		attackingDove.SetActive (false);
		defaultDove.SetActive (true);*/
		attackZone.SetActive (false);
		doveAnimator.SetBool ("attacking", false);
		GameObject.Find ("DoveBoss").GetComponent<Dove> ().phase = 0;
		if (timeBetweenFly > 1.5f) {
			timeBetweenFly -= 0.3f;
		}
		Invoke ("Fly", timeBetweenFly);
	}

	void SecondAttack () {
		int num = Random.Range (0, 3);
		doveAnimator.SetBool ("secondAttack", true);
		Vector3 pos = secondAttackZone.transform.position;
		pos.y -= num * 3;
		secondAttackZone.transform.position = pos;
		/*pos = doveBomb.transform.position;
		pos.y -= num * 3;
		doveBomb.transform.position = pos;*/

		secondAttackZone.SetActive (true);
		Invoke ("SpawnSecondAttackZone", 0.7f);
	}

	void SpawnSecondAttackZone () {
		secondAttackZoneDamage.SetActive (true);
		Invoke ("DespawnSecondAttackZone", 0.05f);
	}

	void DespawnSecondAttackZone () {
		secondAttackZoneDamage.SetActive (false);
		secondAttackZone.SetActive (false);
		Vector3 pos = secondAttackZone.transform.position;
		pos.y = -7f;
		secondAttackZone.transform.position = pos;
		doveAnimator.SetBool ("secondAttack", false);
		Invoke ("SecondAttack", 10f);
	}

	void RandomEvent () {
		GameObject gameObject = GameObject.Find ("Player");
		PlayerController playerController = gameObject.GetComponent<PlayerController> ();
		rainObject.SetActive (false);
		fogObject.SetActive (false);

		int chance = Random.Range (0, 100);
		if (chance < 35) {
			weatherType = EnumWeatherType.RAIN;
			rainObject.SetActive (true);
		}
		if (chance >= 35 && chance < 60) {
			weatherType = EnumWeatherType.CLOUDY;
		}
		if (chance >= 60) {
			weatherType = EnumWeatherType.FOGGY;
			fogObject.SetActive (true);
		}
		Debug.Log ("current weather: " + weatherType.ToString ());

		chance = Random.Range (0, 100);
		Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
		if (chance <= 60) {
			GameObject mealObject = Instantiate (mealPrefab);
			mealObject.transform.position = position;
		} else {
			GameObject mealObject = Instantiate (helmetPrefab);
			mealObject.transform.position = position;
		}
	}

	void RandomObject () {
		if (weatherType == EnumWeatherType.CLOUDY || weatherType == EnumWeatherType.FOGGY) {
			int chance = Random.Range (0, 100);
			if (chance <= pitChance) {
				Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
				GameObject spawnedPit = Instantiate (pitPrefab);
				if (GameObject.Find ("Player").GetComponent<Collider2D> ().IsTouching (spawnedPit.GetComponent<Collider2D> ())) {
					Destroy (spawnedPit);
				}
				position.z = 100;
				spawnedPit.transform.position = position;
				pitChance = 10f;
				Camera.main.GetComponent<CameraShake> ().shakeDuration = 2f;
			} else {
				pitChance += 10f;
			}
		}
		if (weatherType == EnumWeatherType.RAIN) {
			Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
			GameObject spawnedPool = Instantiate (poolPrefab);
			position.z = 100;
			spawnedPool.transform.position = position;
		}
	}

	private Vector3 RandomPointInBox (Vector3 center, Vector3 size) {

		return center + new Vector3 (
			(Random.value - 0.5f) * size.x,
			(Random.value - 0.5f) * size.y,
			(Random.value - 0.5f) * size.z
		);
	}

	public enum EnumWeatherType {
		RAIN,
		CLOUDY,
		FOGGY
	}
}
