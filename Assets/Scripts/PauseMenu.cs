using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pausePanelObject;

    private void Start()
    {
        pausePanelObject = transform.FindChild("PausePanel").gameObject;
    }

    private void Update()
    {
        var selectCharacterMenu = transform.FindChild("Panel");
        if (!selectCharacterMenu.gameObject.activeSelf && Input.GetButton("Cancel"))
        {
            Pause();
        }
    }

    private void Pause()
    {
        pausePanelObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pausePanelObject.SetActive(false);
        Time.timeScale = 1;
    }
}