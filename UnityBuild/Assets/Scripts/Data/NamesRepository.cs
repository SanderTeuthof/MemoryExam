using MemoryGame.Data.Model;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace MemoryGame.Data
{
    public class NamesRepository : Singleton<NamesRepository>
    {
        private string urlPostNames = "https://localhost:44378/api/Names"; // Update this URL to your actual endpoint

        public void PostNames(string name1, string name2)
        {
            DBNames names = new DBNames
            {
                Name1 = name1,
                Name2 = name2
            };

            StartCoroutine(PostNamesCoroutine(names));
        }

        private IEnumerator PostNamesCoroutine(DBNames names)
        {
            string json = JsonConvert.SerializeObject(names);
            UnityWebRequest uwr = UnityWebRequest.Put(urlPostNames, json);
            uwr.SetRequestHeader("content-type", "application/json");
            uwr.method = "POST";
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                string returnJson = uwr.downloadHandler.text;
                DBNames returnNames = JsonConvert.DeserializeObject<DBNames>(returnJson);
                // Handle the response if needed
            }
            else
            {
                Debug.LogError("Error posting names: " + uwr.error);
            }
        }
    }
}


