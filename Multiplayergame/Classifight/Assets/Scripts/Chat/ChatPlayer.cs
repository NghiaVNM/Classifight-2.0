using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChatPlayer : NetworkBehaviour
{
    public InputField inputField;

    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer)
        {
            base.OnNetworkSpawn();
            AddChatServerRpc("Player " + (OwnerClientId + 1) + " joined the chat");
            inputField = ChatManager.instance.inputField;
            inputField.onSubmit.AddListener(SendMessageFromUI);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        AddChatServerRpc("Player " + (OwnerClientId + 1) + " left the chat");
    }

    public void SendMessageFromUI(string msg)
    {
        Debug.Log(msg);
        inputField.text = "";
        AddChatServerRpc(msg);
    }

    [ServerRpc(RequireOwnership = false)]
    void AddChatServerRpc(string v)
    {
        AddChatClientRpc(v);
    }

    [ClientRpc]
    void AddChatClientRpc(string v)
    {
        ChatManager.instance.AddChat(v, OwnerClientId);
    }
}

