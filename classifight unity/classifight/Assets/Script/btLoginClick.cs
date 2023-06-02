using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btLoginClick : MonoBehaviour
{
    public void click()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Terrain2");
    }
}
