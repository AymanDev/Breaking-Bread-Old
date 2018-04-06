using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Timer timer;
    private Text text;
    public double score;

    private void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("Tick", 0f, 1f);
    }

    private void Tick()
    {
        var playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        double multiplier = (100.0 * 2.0 - playerController.health) / 100.0;
        if (timer != null) score += 1.5 * (timer.seconds / 5.0) * multiplier;
        text.text = "Score: " + Convert.ToInt32(score) + " (" + multiplier + "x)";
    }
}