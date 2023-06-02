using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public Text chatText;
    Stack<string> chat = new Stack<string>();
    int maxMessage = 30;

    public static ChatManager instance;
    public InputField inputField;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S)) {
            AddChat(Random.value.ToString());
        }*/
    }   

    public void AddChat(string v)
    {
        chat.Push("> " + v);
        if (chat.Count > maxMessage)
            chat.Pop();
        
        // convert stack to array, separate with \n
        chatText.text = string.Join("\n", chat);
    }
/*
    public void SendMessageFromUI(string msg) {
        Debug.Log(msg);
        inputField.text = "";
    }*/

    public void SendMessageToServer()
    {
        string mess = inputField.text;
        ClientSend.Message(mess);
        inputField.text = "";
    }
}
