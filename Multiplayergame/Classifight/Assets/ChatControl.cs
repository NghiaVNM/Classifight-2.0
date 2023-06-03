using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatControl : NetworkBehaviour
{
    public TMP_Text show_Message;
    public TMP_InputField input;

    [ClientRpc]
    public void SendMessageClientRpc(string message)
    {
        // Call the ClientRpc to update the chat message on all clients
        ReceiveMessageClientRpc(message);
    }

    [ClientRpc]
    public void ReceiveMessageClientRpc(string message)
    {
        show_Message.text += $"{message}\n";
    }

    // Call this method when the send button is clicked or Enter key is pressed
    public void SendButtonClicked()
    {
        if (input.text.Trim() == string.Empty)
            return;

        string message = input.text;
        SendMessageClientRpc(message);
        input.text = string.Empty;
    }
}