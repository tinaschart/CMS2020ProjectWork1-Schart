using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("restart");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
