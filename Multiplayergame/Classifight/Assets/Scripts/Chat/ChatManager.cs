using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public Text chatText;
    Stack<string> chat = new Stack<string>();
    int maxMessages = 30;
    public static ChatManager instance;
    public InputField inputField;

    void Start()
    {
        instance = this;
    }

    public void AddChat(string v, ulong id) {
        chat.Push(id + "> " + v);
        if(chat.Count > maxMessages) 
            chat.Pop();

        chatText.text = string.Join("\n", chat);
    }
}
