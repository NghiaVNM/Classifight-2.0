using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    static bool connect = false;
    public UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Training() {
        SceneManager.LoadScene("TrainingScene");
    }

    public void Setting() {
        SceneManager.LoadScene("SettingMenu");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

    public void SignIn() {
        SceneManager.LoadScene("LoginScenes");
    }

    public void SkyBlock() {
        SceneManager.LoadScene("SkyBlock");
    }

    public void ChoosingMap() {
        if (!connect)
        {
            ui.ConnectToServer();
            connect = true;
        }
        SceneManager.LoadScene("ChooseMap");
    }
}
