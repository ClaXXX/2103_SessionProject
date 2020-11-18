using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private List<PlayerManager> _players = new List<PlayerManager>();
    private const int MAXPlayerNbr = 4;
    public int PlayerNbr { get; protected set; }
    public int PlayerIndex { get; protected set; }

    public GameObject playerPrefabs;
    public Camera mainCamera;
    public GameObject gameOverObj;

    public enum GameMode
    {
        Running,
        Paused,
        Finished
    }

    public GameMode GameModeVar { get; protected set; }
    
    void Start()
    {
        LaunchGame(2);
    }

    // Update is called once per frame
    void Update()
    {
        // Game paused
    }

    void LaunchGame(int playerNbr)
    {
        if (playerNbr >= MAXPlayerNbr)
        {
            Debug.LogError("Une erreur est survenue avec le nombre de joueur, le nombre maximal est 4");
            playerNbr = 4;
        }
        
        GameModeVar = GameMode.Running;
        PlayerNbr = playerNbr;
        PlayerIndex = 0;
        mainCamera.enabled = false;
        Next();
    }

    public void Next()
    {
        if (GameModeVar != GameMode.Running)
        {
            return;
        }
        if (PlayerIndex + 1 > PlayerNbr)
        {
            PlayerIndex = 0;
        } else if (PlayerIndex >= _players.Count)
        {
            GameObject go = Instantiate(playerPrefabs);
            _players.Add(go.GetComponent<PlayerManager>());
        }
        
        _players[PlayerIndex].Play();
        PlayerIndex++;
    }

    public void GameOver()
    {
        GameModeVar = GameMode.Finished;
        _players.ForEach(manager => Destroy(manager.gameObject));
        mainCamera.enabled = true;
        gameOverObj.SetActive(true);
        
    }

    public void Replay()
    {
        LoadingData.sceneToLoad = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Loading");
    }

    public void Quit()
    {
        LoadingData.sceneToLoad = "Main";
        SceneManager.LoadScene("Loading");
    }
}
