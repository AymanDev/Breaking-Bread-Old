using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathEvent : MonoBehaviour
{
    public Highscore highscore;
    public Text highscoreText;

    private IEnumerator Start()
    {
        StartCoroutine(highscore.GetPersonalHighscore());
        yield return new WaitForSeconds(1);
        AddNewRecord();
        // highscoreText.text = "Highscore: " + personalHighscore.GetEnumerator().Current;
    }

    private void AddNewRecord()
    {
        var maxScore = highscore.highscore;
        var score = Convert.ToInt32(highscore.scoreText.text.Replace("Score: ", ""));


        if (score <= maxScore) return;
        StartCoroutine(highscore.AddHighscore());
    }
}