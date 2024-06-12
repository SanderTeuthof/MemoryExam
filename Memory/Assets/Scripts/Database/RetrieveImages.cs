using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FetchImages : MonoBehaviour
{
    [SerializeField]
    private string apiUrl = "https://localhost:5001/api/imagedata/images"; // Adjust the URL to match your API endpoint
    [SerializeField]
    private MemoryGame memoryGame;

    private void Start()
    {
        StartCoroutine(FetchImagesFromApi());
    }

    private IEnumerator FetchImagesFromApi()
    {
        Debug.Log("Starting API request to: " + apiUrl);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error during API request: " + webRequest.error);
            }
            else
            {
                Debug.Log("API request successful. Response length: " + webRequest.downloadHandler.text.Length);

                // Parse the JSON response
                string json = webRequest.downloadHandler.text;

                try
                {
                    List<ImageData> imageList = JsonHelper.FromJson<ImageData>(json);
                    Debug.Log("Parsed JSON successfully. Number of images: " + imageList.Count);

                    List<Texture2D> textures = ParseImageResponse(imageList);
                    Debug.Log("Parsed images successfully. Number of textures: " + textures.Count);

                    memoryGame.SetUpMemoryGame(textures);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON response: " + e.Message);
                }
            }
        }
    }

    private List<Texture2D> ParseImageResponse(List<ImageData> imageList)
    {
        List<Texture2D> textures = new List<Texture2D>();

        foreach (ImageData imageData in imageList)
        {
            if (!string.IsNullOrEmpty(imageData.imageData))
            {
                try
                {
                    byte[] imageBytes = System.Convert.FromBase64String(imageData.imageData);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);
                    textures.Add(texture);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error converting base64 string to texture for image: " + imageData.imageName + ". Error: " + e.Message);
                }
            }
            else
            {
                Debug.LogWarning("ImageData is null or empty for image: " + imageData.imageName);
            }
        }

        return textures;
    }
}

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        // Ensure the JSON string is wrapped correctly
        string newJson = "{ \"Items\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}


