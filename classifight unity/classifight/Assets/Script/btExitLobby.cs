using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btExitLobby : MonoBehaviour
{
    public void click()
    {
        SceneManager.LoadScene("Start");
    }
}
