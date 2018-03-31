using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	[SerializeField]
	private Timer timer;
	private Text text;
	public double score = 0;

	void Start () {
		text = GetComponent<Text> ();
		InvokeRepeating ("Tick", 0f, 1f);
	}

	void Tick () {
		score += 1.5 * (timer.seconds / 5);
		text.text = "Score: " + Convert.ToInt32 (score);
	}
}
