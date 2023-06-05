using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;

public class RegisterLogin : MonoBehaviour
{

    private string baseUrl = "http://localhost:8080/thirteenov/UnityLoginLogoutRegister/";
    [SerializeField] private InputField accountUserName;
    [SerializeField] private InputField accountPassword;
    [SerializeField] private InputField accountConfirmPassword;
    [SerializeField] private Text info;
    [SerializeField] private AudioSource butonCLick;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AccountRegister()
    {
        string uName = accountUserName.text;
        string pWord = accountPassword.text;
        string cfpword = accountConfirmPassword.text;
        StartCoroutine(RegisterNewAccount(uName, pWord, cfpword));
    }
    public void AccountLogin()
    {
        string uName = accountUserName.text;
        string pWord = accountPassword.text;
        StartCoroutine(LoginAccount(uName, pWord));
    }

    public void RegisterEvent()
    {
        butonCLick.Play();
        SceneManager.LoadScene("RegisterScenes");
    }
    
    public void LoginEvent()
    {
        butonCLick.Play();
        SceneManager.LoadScene("LoginScenes");
    }
    IEnumerator RegisterNewAccount(string uName, string pWord, string cfpWord)
    {
        if (pWord != cfpWord)
        {
            info.text = "The confirm password is incorrect, please type again";
        }
        else
        {
            pWord = HashPassword(pWord);
            WWWForm form = new WWWForm();
            form.AddField("newAccountUsername", uName);
            form.AddField("newAccountPassword", pWord);

            using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string responseText = www.downloadHandler.text;
                    info.text = responseText;
                }
            }
        }

    }
    IEnumerator LoginAccount(string uName, string pWord)
    {
        pWord = HashPassword(pWord);
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", uName);
        form.AddField("loginPassword", pWord);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if (responseText == "200") SceneManager.LoadScene("MainScenes");
                else info.text = responseText;
            }
        }
    }
    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Chuyển đổi chuỗi thành mảng byte
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Băm dữ liệu
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Chuyển đổi mảng byte thành chuỗi hexa
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            return hash;
        }
    }
}
