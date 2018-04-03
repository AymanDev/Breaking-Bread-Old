using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}