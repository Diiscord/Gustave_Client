using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserTokens : MonoBehaviour
{
	public static Tokens tokens;

    private static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    void Start ()
	{
		Debug.Log("Start");
	}
	
	public Tokens getTokens()
	{
		return (tokens);
	}
	
	public void displayTokens()
	{
		Debug.Log("Display Tokens");
		Debug.Log(tokens.access_token);
	}

    public Tokens getTokensFromJson(string jsonString)
    {
        return JsonUtility.FromJson<Tokens>(jsonString);
    }

    public void RefreshToken()
    {
        StartCoroutine(RefreshTokenRequest("refresh_token", tokens.refresh_token));
    }

    public IEnumerator RefreshTokenRequest(string grant_type, string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", grant_type);
        form.AddField("refresh_token", token); // here token = refresh_token

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.gustave.pro/oauth2/token", form))
        {
            // Adding in the header this -> ("Authorization", "Basic base64(client_id:client_secret)")
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("923ab2dc-c291-4a15-b9db-2d3b9c84cb9c:3JIzAHCKpoDtrVC1GoOHahdxrBAiKQeLtEbutxtnOg");
            www.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete! Refresh Token");
                tokens = getTokensFromJson(www.downloadHandler.text);
            }
        }
    }
}
