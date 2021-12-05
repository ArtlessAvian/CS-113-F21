using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject credits;
    public void RestartGame()
    {
        SceneManager.LoadScene("MarvinsScene2");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void Credits()
    {
        Time.timeScale = 0f;
        credits.SetActive(true);
        gameObject.SetActive(false);

    }
}
