using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private Text text;
	private DateTime dateTime;
	public int seconds = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		dateTime = new DateTime ();
		InvokeRepeating ("Tick", 1f, 1f);
	}

	void Tick () {
		seconds++;
		TimeSpan time = TimeSpan.FromSeconds(seconds);
		string str = time.ToString();
		text.text = "Time: " + str;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
