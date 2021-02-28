using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");

        // Ensure that timescale is 1 in case player returned from pause
        Time.timeScale = 1;
    }
}
