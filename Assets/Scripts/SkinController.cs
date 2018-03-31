using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour {

	private int skinId = 0;
	public int maxSkinId = 1;
	public GameObject skinSelectUI;

	void Start () {
		Time.timeScale = 0;
	}

	public void Apply () {
		Time.timeScale = 1;
		skinSelectUI.SetActive (false);
	}

	void DisableSkin (int skinId) {
		transform.FindChild (skinId.ToString ()).gameObject.SetActive (false);
	}

	void EnableSkin (int skinId) {
		transform.FindChild (skinId.ToString ()).gameObject.SetActive (true);
	}

	public void Increase () {
		if (skinId < maxSkinId) {
			DisableSkin (skinId);
			skinId++;
			EnableSkin (skinId);
		}
	}

	public void Descrease () {
		if (skinId > 0) {
			DisableSkin (skinId);
			skinId--;
			EnableSkin (skinId);
		}
	}
}
