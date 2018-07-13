using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Tokens
{
    public string access_token;
    public string refresh_token;
    public string token_type;
}

public class UserInformations
{
    protected int id;
    protected string email;
    public string username;
    protected string password;
    protected string token;
    protected string createdAt;
    protected string updatedAt;
}

[System.Serializable]
public class UserRecipe
{
    protected int id;
    protected string createdAt;
    protected string updatedAt;
    protected int UserId;
    public string RecipeId;
}

public class Server_login : MonoBehaviour, IInputClickHandler
{
    UserInformations userInformations;
    public Tokens tokens;
    public InputField emailField;
    public InputField passwordField;
    public List<string> jsonStrings = new List<string>();

    // Use this for initialization
    void Start()
    {

    }

    public Tokens getTokensFromJson(string jsonString)
    {
        return JsonUtility.FromJson<Tokens>(jsonString);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        StartCoroutine(LogInRequest("password", emailField.text, passwordField.text));
    }

    // GameObject parent = GameObject.Find("Menu Manager");

    public IEnumerator LogInRequest(string grant_type, string email, string password)
    {
        WWWForm form = new WWWForm();
        email = "romain.chateigner@epitech.eu";
        password = "bibica25*";
        form.AddField("grant_type", grant_type);
        form.AddField("username", email);
        form.AddField("password", password);

        GameObject parent = GameObject.Find("Menu Manager");

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.gustave.pro/oauth2/token", form))
        {
            // Adding in the header this -> ("Authorization", "Basic base64(client_id:client_secret)")
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("923ab2dc-c291-4a15-b9db-2d3b9c84cb9c:3JIzAHCKpoDtrVC1GoOHahdxrBAiKQeLtEbutxtnOg");
            www.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.downloadHandler.text);
                find_object.FindObject(parent, "Error Popup").SetActive(true);
                find_object.FindObject(parent, "User not found").SetActive(false);
                find_object.FindObject(parent, "Incorrect password").SetActive(false);
                find_object.FindObject(parent, "Missing Email").SetActive(false);
                find_object.FindObject(parent, "Missing Password").SetActive(false);
                
                if (email == "")
                {
                    find_object.FindObject(parent, "Missing Email").SetActive(true);
                }
                else if (password == "")
                {
                    find_object.FindObject(parent, "Missing Password").SetActive(true);
                }
                else if (www.downloadHandler.text.Contains("User not found"))
                {
                    find_object.FindObject(parent, "User not found").SetActive(true);
                }
                else if (www.downloadHandler.text.Contains("Incorrect credentials"))
                {
                    find_object.FindObject(parent, "Incorrect password").SetActive(true);
                }
            }   
            else
            {
                Debug.Log("Form upload complete! LogIn");
                tokens = getTokensFromJson(www.downloadHandler.text);
                UserTokens.tokens = tokens;
                Debug.Log("Access token >>>>>>>>>>>>>>>>" + UserTokens.tokens.access_token + "<<<<<<<<<<<<<<<<<<<<");
                StartCoroutine(GetUserInfos());
            }
        }
    }

    public UserInformations getUserInformationsFromJson(string jsonString)
    {
        return JsonUtility.FromJson<UserInformations>(jsonString);
    }

    IEnumerator GetUserInfos()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.gustave.pro/user/infos"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + UserTokens.tokens.access_token);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete! UserInfos");
                Debug.Log(www.downloadHandler.text);
                userInformations = getUserInformationsFromJson(www.downloadHandler.text);
                StartCoroutine(UserRecipe());
            }
        }
    }

    public IEnumerator GetRecipe(string id)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.gustave.pro/recipe/info?id=" + id))
        {
            www.SetRequestHeader("Authorization", "Bearer " + UserTokens.tokens.access_token);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Here is the file >>>>>>" + www.downloadHandler.text);
                //System.IO.File.WriteAllText(@"Assets\Resources\JsonRecipes\" + id + "_recipe.json", www.downloadHandler.text);
                jsonStrings.Add(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator UserRecipe()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.gustave.pro/user/recipes"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + UserTokens.tokens.access_token);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete! UserRecipe");
                UserRecipe[] userRecipe = JsonHelper.FromJson<UserRecipe>("{\"Items\":" + www.downloadHandler.text + "}");

                for (int i = 0; i < userRecipe.Length; i++)
                {
                    Debug.Log("RecipeId numéro //////////" + userRecipe[i].RecipeId);
                    StartCoroutine(GetRecipe(userRecipe[i].RecipeId));
                }
                yield return new WaitForSeconds(3);
                ClientManager.CM.setUserInfo(userInformations);
                Debug.Log("dans serveur - avant de le mettre dans cm : " +jsonStrings[0]);
                string json_brut = "{\"description\": { \"rank\": \"5\",\"price_rank\": \"3\",\"difficulty\": \"2\",\"name\": \"lotte lardée au thym\",\"cook_time\": \"20\",\"prepa_time\": \"20\",\"break_time\": \"0\",\"total_time\": \"40\",\"desc\": \"De délicieux filets de lotte enroulé dans du lard\",\"eater_nbr\": \"2\",\"tags\": [\"\"]},	\"tools\": [{\"name\": \"couteau\",\"desc\": \"un couteau\",\"icone\": \"l'image à l'addr\",\"quantity\": \"1\"}],\"ingredients\": [{\"name\": \"filet de lotte\",\"desc\": \"un filet de lotte\",\"icone\": \"image\",\"quantity\": \"2 filets (environ 200g chacun)\",\"unit\": \"g\"},{\"name\": \"tranches de lard\",\"desc\": \"des tranches de lard fumé ou pas\",\"icone\": \"image\",\"quantity\": \"4 à 6 tranches de lard\",\"unit\": \"unité\"},{\"name\": \"thym\",\"desc\": \"branches de thym\",\"icone\": \"image\",\"quantity\": \"2 branches\",\"unit\": \"unité\"}],\"steps\": [{\"id\": 0,\"info\": {\"icone\": \"addr\",\"desc\": \"Lotte lardée au thym\"},\"text\": \"Lotte lardée au thym\",\"tip\": \"\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\":0},{\"id\": 1,\"info\": {\"icone\": \"addr\",\"desc\": \"préparer le poisson\"},\"text\": \"Préparer les filets de lotte en retirant la peau les déchets\",\"tip\": \"\",\"video\": [\"gordon_ramsay\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 1},{\"id\": 2,\"info\": {\"icone\": \"addr\",\"desc\": \"Ajouter le thym.\"},\"text\": \"Inciser les filets de lotte et mettre le thym.\",\"tip\": \"Vous pouvez également poivrer si vous le désirez.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img2\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 3,\"info\": {\"icone\": \"addr\",\"desc\": \"Larder la lotte \"},\"text\": \"Enrouler la lotte avec le lard.\",\"tip\": \"Mettez des cures dents pour faire tenir le lard si besoin.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img3\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 4,\"info\": {\"icone\": \"addr\",\"desc\": \"Cuisson de la lotte\"},\"text\": \"Cuire la lotte au four à 185° pendant 20 minutes.\",\"tip\": \"Préchaffez votre four.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img4\",\"animation_gustave\": \"56\",\"timer_flag\": 1,\"timer\": {\"time\": 1200,\"callback\": {\"action\": \"Enlever les lottes du four.\",\"tips\": \"\"}},\"video_flag\":0}]}";
                jsonStrings.Add(json_brut);
                ClientManager.CM.setJsonStringList(jsonStrings);
                Debug.Log("dans serveur - après : " + ClientManager.CM.jsonStrings[0]);
                ClientManager.CM.LoadMenuScene();
            }
        }
    }

}

// Here is a class for JSon serializer and stuff like that

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}