using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLJavaScriptHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [SerializeField] private MemoryGame.View.MemoryGame _memoryGame;

    private void Awake()
    {
        _memoryGame = FindObjectOfType<MemoryGame.View.MemoryGame>();
    }

    public void UpdatePlayerOne(string name)
    {
        _memoryGame.UpdatePlayerOne(name);
    }

    public void UpdatePlayerTwo(string name)
    {
        _memoryGame.UpdatePlayerTwo(name);
    }

    public void RetrieveNames()
    {
        string json = StringReturnValueFunction();

        // Parse the JSON string
        PlayerNames playerNames = JsonUtility.FromJson<PlayerNames>(json);

        // Assign the retrieved names
        string name1 = playerNames.playerOneName;
        string name2 = playerNames.playerTwoName;

        // Update player names
        UpdatePlayerOne(name1);
        UpdatePlayerTwo(name2);

    }

    [System.Serializable]
    public class PlayerNames
    {
        public string playerOneName;
        public string playerTwoName;
    }
}
