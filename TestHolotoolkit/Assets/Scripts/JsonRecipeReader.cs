using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class JsonRecipeReader {
    private JSONNode _recette;

    // Use this for initialization

    public List<string> getJsonFilePaths()
    {
        //print("> getJsonFilePaths");
        DirectoryInfo levelDirectoryPath = new DirectoryInfo(Application.dataPath + "./Resources/JsonRecipes");
        FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.*");
        List<string> recipe_path = new List<string>();
        
        foreach (FileInfo file in fileInfo)
        {
            //print(file.Name);
            if (file.Extension == ".txt")
            {
                int end_position_extension = file.Name.Length - 4;
                recipe_path.Add("JsonRecipes/" + file.Name.Substring(0, end_position_extension));

            }
            if (file.Extension == ".json")
            {
                int end_position_extension = file.Name.Length - 5;
               recipe_path.Add("JsonRecipes/" + file.Name.Substring(0, end_position_extension));
            }
        }
        //print(recipe_path.Count);
        return recipe_path;
    }

    public string getFastRecipeTitle(string text)
    {
        /*TextAsset json_file = Resources.Load(path) as TextAsset;
        Debug.Log(path);*/
        return JSON.Parse(text)["description"]["name"].ToString().Trim('"');
    }

    public void setJsonRecipePath(string path)
    {
        //CERIC A COMMENTER
        TextAsset json_file = Resources.Load(path) as TextAsset;

        //CERIC A DECOMMENTER
        //string json_string = "{\"description\": { \"rank\": \"5\",\"price_rank\": \"3\",\"difficulty\": \"2\",\"name\": \"lotte lardée au thym\",\"cook_time\": \"20\",\"prepa_time\": \"20\",\"break_time\": \"0\",\"total_time\": \"40\",\"desc\": \"De délicieux filets de lotte enroulé dans du lard\",\"eater_nbr\": \"2\",\"tags\": [\"\"]},	\"tools\": [{\"name\": \"couteau\",\"desc\": \"un couteau\",\"icone\": \"l'image à l'addr\",\"quantity\": \"1\"}],\"ingredients\": [{\"name\": \"filet de lotte\",\"desc\": \"un filet de lotte\",\"icone\": \"image\",\"quantity\": \"2 filets (environ 200g chacun)\",\"unit\": \"g\"},{\"name\": \"tranches de lard\",\"desc\": \"des tranches de lard fumé ou pas\",\"icone\": \"image\",\"quantity\": \"4 à 6 tranches de lard\",\"unit\": \"unité\"},{\"name\": \"thym\",\"desc\": \"branches de thym\",\"icone\": \"image\",\"quantity\": \"2 branches\",\"unit\": \"unité\"}],\"steps\": [{\"id\": 0,\"info\": {\"icone\": \"addr\",\"desc\": \"Lotte lardée au thym\"},\"text\": \"Lotte lardée au thym\",\"tip\": \"\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\":0},{\"id\": 1,\"info\": {\"icone\": \"addr\",\"desc\": \"préparer le poisson\"},\"text\": \"Préparer les filets de lotte en retirant la peau les déchets\",\"tip\": \"\",\"video\": [\"gordon_ramsay\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 1},{\"id\": 2,\"info\": {\"icone\": \"addr\",\"desc\": \"Ajouter le thym.\"},\"text\": \"Inciser les filets de lotte et mettre le thym.\",\"tip\": \"Vous pouvez également poivrer si vous le désirez.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img2\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 3,\"info\": {\"icone\": \"addr\",\"desc\": \"Larder la lotte \"},\"text\": \"Enrouler la lotte avec le lard.\",\"tip\": \"Mettez des cures dents pour faire tenir le lard si besoin.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img3\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 4,\"info\": {\"icone\": \"addr\",\"desc\": \"Cuisson de la lotte\"},\"text\": \"Cuire la lotte au four à 185° pendant 20 minutes.\",\"tip\": \"Préchaffez votre four.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img4\",\"animation_gustave\": \"56\",\"timer_flag\": 1,\"timer\": {\"time\": 1200,\"callback\": {\"action\": \"Enlever les lottes du four.\",\"tips\": \"\"}},\"video_flag\":0}]}";

        // CERIC A REMPLACER PAR json_file
        _recette = JSON.Parse(json_file.text);
    }

    public void setJsonRecipe(string text)
    {
        //CERIC A COMMENTER
        //TextAsset json_file = Resources.Load(path) as TextAsset;

        //CERIC A DECOMMENTER
        //string json_string = "{\"description\": { \"rank\": \"5\",\"price_rank\": \"3\",\"difficulty\": \"2\",\"name\": \"lotte lardée au thym\",\"cook_time\": \"20\",\"prepa_time\": \"20\",\"break_time\": \"0\",\"total_time\": \"40\",\"desc\": \"De délicieux filets de lotte enroulé dans du lard\",\"eater_nbr\": \"2\",\"tags\": [\"\"]},	\"tools\": [{\"name\": \"couteau\",\"desc\": \"un couteau\",\"icone\": \"l'image à l'addr\",\"quantity\": \"1\"}],\"ingredients\": [{\"name\": \"filet de lotte\",\"desc\": \"un filet de lotte\",\"icone\": \"image\",\"quantity\": \"2 filets (environ 200g chacun)\",\"unit\": \"g\"},{\"name\": \"tranches de lard\",\"desc\": \"des tranches de lard fumé ou pas\",\"icone\": \"image\",\"quantity\": \"4 à 6 tranches de lard\",\"unit\": \"unité\"},{\"name\": \"thym\",\"desc\": \"branches de thym\",\"icone\": \"image\",\"quantity\": \"2 branches\",\"unit\": \"unité\"}],\"steps\": [{\"id\": 0,\"info\": {\"icone\": \"addr\",\"desc\": \"Lotte lardée au thym\"},\"text\": \"Lotte lardée au thym\",\"tip\": \"\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\":0},{\"id\": 1,\"info\": {\"icone\": \"addr\",\"desc\": \"préparer le poisson\"},\"text\": \"Préparer les filets de lotte en retirant la peau les déchets\",\"tip\": \"\",\"video\": [\"gordon_ramsay\"],\"image\": \"img1\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 1},{\"id\": 2,\"info\": {\"icone\": \"addr\",\"desc\": \"Ajouter le thym.\"},\"text\": \"Inciser les filets de lotte et mettre le thym.\",\"tip\": \"Vous pouvez également poivrer si vous le désirez.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img2\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 3,\"info\": {\"icone\": \"addr\",\"desc\": \"Larder la lotte \"},\"text\": \"Enrouler la lotte avec le lard.\",\"tip\": \"Mettez des cures dents pour faire tenir le lard si besoin.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img3\",\"animation_gustave\": \"56\",\"timer_flag\": 0,\"video_flag\": 0},{\"id\": 4,\"info\": {\"icone\": \"addr\",\"desc\": \"Cuisson de la lotte\"},\"text\": \"Cuire la lotte au four à 185° pendant 20 minutes.\",\"tip\": \"Préchaffez votre four.\",\"video\": [\"adresse d'une vidéo\",\"autre addr\"],\"image\": \"img4\",\"animation_gustave\": \"56\",\"timer_flag\": 1,\"timer\": {\"time\": 1200,\"callback\": {\"action\": \"Enlever les lottes du four.\",\"tips\": \"\"}},\"video_flag\":0}]}";

        // CERIC A REMPLACER PAR json_file
        _recette = JSON.Parse(text);
    }

    public string getCurrentRecipeTitle()
    {
        return _recette["description"]["name"].ToString().Trim('"');
    }

    public int getCurrentRecipeNbSteps()
    {
        return _recette["steps"].Count;
    }

    public string getCurrentRecipeStepText(int step_nb)
    {
        //print(_recette["steps"][step_nb]["text"].ToString().Trim('"'));
        return _recette["steps"][step_nb]["text"].ToString().Trim('"');
    }

    public int getCurrentRecipeNbIngredient()
    {
        return _recette["ingredients"].Count;
    }

    public string getCurrentRecipeIngredientName(int ingredient_nb)
    {
        return _recette["ingredients"][ingredient_nb]["name"].ToString().Trim('"');
    }

    public string getCurrentRecipeStepImage(int step_nb)
    {
        return _recette["steps"][step_nb]["image"].ToString().Trim('"');
    }

    public string getCurrentRecipeStepTimerFlag(int step_nb)
    {
        return _recette["steps"][step_nb]["timer_flag"].ToString().Trim('"');
    }

    public string getCurrentRecipeStepVideoFlag(int step_nb)
    {
        return _recette["steps"][step_nb]["video_flag"].ToString().Trim('"');

    }

    public string getCurrentRecipeStepVideoPath(int step_nb)
    {
        return _recette["steps"][step_nb]["video"][0].ToString().Trim('"');
    }

    public int getCurrentRecipeStepTimer(int step_nb)
    {
        return _recette["steps"][step_nb]["timer"]["time"].AsInt;
    }


}
