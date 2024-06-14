using MemoryGame.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace MemoryGame.Data
{
    public class ImageRepository : Singleton<ImageRepository>
    {
        private string urlMemoryImages = "https://localhost:44378/Image";

        public void ProcessImageIds(Action<List<int>> processIds)
        {
            StartCoroutine(GetImageIDs(processIds));
        }

        public void GetProcessTexture(int imageId, Action<Texture2D> processTexture)
        {
            StartCoroutine(GetTextures(imageId, processTexture));
        }

        private IEnumerator GetImageIDs(Action<List<int>> prcessIds)
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(urlMemoryImages);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ImageRepository.GetImageIDs: " + unityWebRequest.error);
            }
            else
            {
                string json = unityWebRequest.downloadHandler.text;
                List<DBImage> images = JsonConvert.DeserializeObject<List<DBImage>>(json);
                //images = images.Where(i => !i.IsBack).ToList();
                List<int> imagedbids = images.Select(i => i.Id).ToList();
                prcessIds(imagedbids);
            }
        }

        private IEnumerator GetTextures(int imageId, Action<Texture2D> processTexture)
        {
            UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture($"{urlMemoryImages}/{imageId}");
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ImageRepository.GetProcessMaterials: " + unityWebRequest.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                processTexture(texture);
            }
        }
    }
}