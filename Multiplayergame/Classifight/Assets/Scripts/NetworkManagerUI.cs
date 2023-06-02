using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{
    public GameObject chat;

    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            chat.SetActive(true);
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() =>
        {
            chat.SetActive(true);
            NetworkManager.Singleton.StartClient();
        });
    }
}
