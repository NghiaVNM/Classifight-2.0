using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private AudioSource soundBtn;

    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {   
            soundBtn.Play();
            NetworkManager.Singleton.StartHost();
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
        });
        clientBtn.onClick.AddListener(() =>
        {
            soundBtn.Play();
            NetworkManager.Singleton.StartClient();
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
        });
    }
}
