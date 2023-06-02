using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSend : MonoBehaviour
{
    public void SendMessageToServer(string _message){
        ClientSend.Message(_message);
    }
}
