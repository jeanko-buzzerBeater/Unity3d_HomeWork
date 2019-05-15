using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseImage;
    public void SwitchPause()
    {
        if (Time.timeScale < 0.9f)
        {
            Time.timeScale = 1;
            pauseImage.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseImage.SetActive(true);
        }
    }
}

