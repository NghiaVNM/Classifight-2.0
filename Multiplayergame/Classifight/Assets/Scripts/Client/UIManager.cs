using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public GameObject startMenu;
    //public GameObject chatBox;
    //public InputField usernameField;
    //public GameObject message;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        //startMenu.SetActive(false);
        //usernameField.interactable = false;
        Client.instance.ConnectToServer();
        //chatBox.SetActive(true);
    }

    public void SendMessageToServer()
    {
        //string _message = message.ToString();
        //ClientSend.Message(_message);
    }
}