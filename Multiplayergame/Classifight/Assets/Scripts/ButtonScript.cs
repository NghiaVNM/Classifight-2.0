using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    static bool connect = false;
    public UIManager ui;
    // Start is called before the first frame update
    [SerializeField] private AudioSource ButtonSoundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Training() {
        ButtonSoundEffect.Play();
        SceneManager.LoadScene("TrainingScene");
    }

    public void Setting() {
        ButtonSoundEffect.Play();
        SceneManager.LoadScene("SettingMenu");
    }

    public void Menu() {
        ButtonSoundEffect.Play();
        SceneManager.LoadScene("Menu");
    }

    public void SignIn() {
        ButtonSoundEffect.Play();
        SceneManager.LoadScene("LoginScenes");
    }

    public void SkyBlock() {
        ButtonSoundEffect.Play();//
        SceneManager.LoadScene("SkyBlock");
    }

    public void ChoosingMap() {
        
        SceneManager.LoadScene("ChooseMap");
    }

    public void con()
    {
        ui.ConnectToServer();
    }
}
