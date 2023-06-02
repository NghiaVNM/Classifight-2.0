using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btMultiplay_Click : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;
    
    public void click()
    {
        panel.SetActive(true);
        button.SetActive(false);
    }
}
