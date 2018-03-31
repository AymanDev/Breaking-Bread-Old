using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

	[SerializeField]
	private EnumWeatherType weatherType = EnumWeatherType.CLOUDY;

	public GameObject rainObject;
	public GameObject spawnZone;

	[SerializeField]
	private GameObject pitPrefab;
	[SerializeField]
	private GameObject poolPrefab;

	public GameObject attackZone;

	public GameObject defaultDove;
	public GameObject flyingDove;
	public GameObject attackingDove;

	public float timeBetweenFly = 20f;

	void Start () {
		InvokeRepeating ("RandomEvent", 150f, 150f);
		InvokeRepeating ("RandomObject", 30f, 30f);
		InvokeRepeating ("Fly", timeBetweenFly, timeBetweenFly);
	}

	void Fly () {
		defaultDove.SetActive (false);
		flyingDove.SetActive (true);
		Invoke ("Attack", 1f);
	}

	void Attack () {
		flyingDove.SetActive (false);
		attackingDove.SetActive (true);
		Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
		position.z = 100;
		attackZone.transform.position = position;
		attackZone.SetActive (true);
		Invoke ("Land", 1f);
	}

	void Land () {
		attackZone.SetActive (false);
		attackingDove.SetActive (false);
		defaultDove.SetActive (true);
	}

	void RandomEvent () {
		rainObject.SetActive (false);

		int chance = Random.Range (0, 100);
		if (chance < 35) {
			weatherType = EnumWeatherType.RAIN;
			rainObject.SetActive (true);
		}
		if (chance >= 35 && chance < 70) {
			weatherType = EnumWeatherType.CLOUDY;
		}
		if (chance >= 70) {
			weatherType = EnumWeatherType.FOGGY;
		}
		Debug.Log ("current weather: " + weatherType.ToString ());
	}

	void RandomObject () {
		if (weatherType == EnumWeatherType.CLOUDY || weatherType == EnumWeatherType.FOGGY) {
			int chance = Random.Range (0, 100);
			if (chance <= 10) {
				Vector3 position = RandomPointInBox (spawnZone.transform.position, spawnZone.GetComponent<Collider2D> ().transform.localScale);
				GameObject spawnedPit = Instantiate (pitPrefab);
				position.z = 100;
				spawnedPit.transform.position = position;
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

	enum EnumWeatherType {
		RAIN,
		CLOUDY,
		FOGGY
	}
}
