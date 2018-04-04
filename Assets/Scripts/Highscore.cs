using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    private const string secretKey = "bj94bMdt2q";
    private const string URL = "https://itect.ru/score.php?";
    public string nickname;
    public int highscore;

    public InputField inputField;
    public GameObject applyButton;
    public Text scoreText;
    public Text highscoreText;

    public int personalHighscore;

    private void Start()
    {
        applyButton.GetComponent<Button>().enabled = false;
    }

    public void ChangingField()
    {
        applyButton.GetComponent<Button>().enabled = inputField.text.Length > 3;
    }

    public void SetNickname()
    {
        nickname = inputField.text;
    }

    public IEnumerator AddHighscore()
    {
        var score = Convert.ToInt32(scoreText.text.Replace("Score:", ""));
        var hash = MD5.Md5Sum(nickname + score + secretKey);
        var form = new WWWForm();
        form.AddField("action", "add");
        form.AddField("name", nickname);
        form.AddField("score", score);
        form.AddField("hash", hash);

        var www = new WWW(URL, form);

        yield return www;
        Debug.Log("All is fine " + www.text + " " + hash);

        if (www.error != null)
        {
            Debug.LogWarning("There was an error posting the high score: " + www.error);
        }
        
    }

    public IEnumerator GetHighscores()
    {
        var form = new WWWForm();
        form.AddField("action", "get");
        form.AddField("name", nickname);

        var www = new WWW(URL, form);

        yield return www; // Wait until the download is done

        if (www.error != null)
        {
            print("There was an error posting the high score: " + www.error);
        }
    }

    public IEnumerator GetPersonalHighscore()
    {
        Debug.Log("Getting highscore");
        /*var hash = MD5.Md5Sum(nickname + secretKey);
        var form = new WWWForm();
        form.AddField("action", "get_personal");
        form.AddField("name", nickname);
        form.AddField("hash", hash);
        Debug.Log(hash);*/

        var form = new WWWForm();
        form.AddField("action", "get_personal");
        form.AddField("name", nickname);

        var www = new WWW(URL, form);
        yield return www;

        highscoreText.text = "Highscore: " + www.text;
        highscore = Convert.ToInt32(www.text);

        if (www.error == null) yield break;
        scoreText.text = "Highscore: -1";
        print("There was an error posting the high score: " + www.error);
    }
}