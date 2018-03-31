using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

	[SerializeField]
	private EnumWeatherType weatherType = EnumWeatherType.CLOUDY;

	public GameObject rainObject;

	public 

	void Start () {
		InvokeRepeating ("RandomEvent", 30f, 30f);
	}

	void RandomEvent () {
		rainObject.SetActive (false);

		int chance = Random.Range (0, 100);
		if (chance < 35) {
			weatherType = EnumWeatherType.RAIN;
		}
		if (chance >= 35 && chance < 70) {
			weatherType = EnumWeatherType.CLOUDY;
			rainObject.SetActive (true);
		}
		if (chance >= 70) {
			weatherType = EnumWeatherType.FOGGY;
		}
		Debug.Log ("current weather: " + weatherType.ToString ());
	}

	enum EnumWeatherType {
		RAIN,
		CLOUDY,
		FOGGY
	}
}
