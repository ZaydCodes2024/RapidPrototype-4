using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
