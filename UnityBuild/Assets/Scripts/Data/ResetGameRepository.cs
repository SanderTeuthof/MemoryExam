using MemoryGame.Data.Model;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;

namespace MemoryGame.Data
{
    public class ResetGameRepository : Singleton<ResetGameRepository>
    {
        private string urlMemoryResetGame = "https://localhost:44378/ResetGame";

        public void ResetGame(string resetTime, string resetPlayer)
        {
            DBResetGame resetGame = new DBResetGame();
            resetGame.ResetTime = resetTime;
            resetGame.ResetPlayer = resetPlayer;

            StartCoroutine(PostResetGame(resetGame));
        }

        private IEnumerator PostResetGame(DBResetGame resetGame)
        {
            string json = JsonConvert.SerializeObject(resetGame);
            UnityWebRequest uwr = UnityWebRequest.Put(urlMemoryResetGame, json);
            uwr.SetRequestHeader("content-type", "application/json");
            uwr.method = "POST";
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                string returnJson = uwr.downloadHandler.text;
                DBResetGame returnResetGame = JsonConvert.DeserializeObject<DBResetGame>(returnJson);
            }
        }
    }
}
