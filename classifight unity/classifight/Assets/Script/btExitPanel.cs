using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btExitPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;

    public void click()
    {
        panel.SetActive(false);
        button.SetActive(true);
    }
}
