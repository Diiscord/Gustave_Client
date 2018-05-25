using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using HoloToolkit.Unity.InputModule;

	
public class ServerConnect : MonoBehaviour, IInputClickHandler
{
	public InputField emailField;
	public InputField usernameField;
	public InputField passwordField;
	
    void Start()
    {
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        StartCoroutine(SignInRequest(emailField.text, passwordField.text, usernameField.text));
    }
	
	public IEnumerator SignInRequest(string email, string password, string userName)
	{
		WWWForm form = new WWWForm();
        form.AddField("email", email);
		form.AddField("password", password);
		form.AddField("username", userName);

        GameObject parent = GameObject.Find("Menu Manager");

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.gustave.pro/auth/signup", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(">>>>>>" + www.downloadHandler.text);
                find_object.FindObject(parent, "Error Popup").SetActive(true);
                find_object.FindObject(parent, "EmailTaken").SetActive(false);
                find_object.FindObject(parent, "Wrong password").SetActive(false);
                find_object.FindObject(parent, "Missing Email").SetActive(false);
                find_object.FindObject(parent, "Missing Password").SetActive(false);
                find_object.FindObject(parent, "Missing Username").SetActive(false);

                if (email == "")
                {
                    find_object.FindObject(parent, "Missing Email").SetActive(true);
                }
                else if (password == "")
                {
                    find_object.FindObject(parent, "Missing Password").SetActive(true);
                }
                else if (userName == "")
                {
                    find_object.FindObject(parent, "Missing Username").SetActive(true);
                }
                else if (www.downloadHandler.text.Contains("Email already taken"))
                {
                    find_object.FindObject(parent, "EmailTaken").SetActive(true);
                }
                else if (www.downloadHandler.text.Contains("Validation error: Validation is on password failed"))
                {
                    find_object.FindObject(parent, "Wrong password").SetActive(true);
                }

            }
            else
            {
                find_object.FindObject(parent, "Log In Menu").SetActive(true);
                find_object.FindObject(parent, "Sign In Menu").SetActive(false);
                Debug.Log("Form upload complete!");
            }
        }
	}
}