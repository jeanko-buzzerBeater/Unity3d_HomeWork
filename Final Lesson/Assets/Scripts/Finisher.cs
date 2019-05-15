using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finisher : MonoBehaviour
{
    private bool isGameFinished;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isGameFinished) return;
        if (collider.gameObject.CompareTag("Player"))
        {
            isGameFinished = true;
        }
    }

    private void NextLavel()
    {
        if (isGameFinished)
        {
            SceneManager.LoadScene("Level_2");
        }
    }
}
