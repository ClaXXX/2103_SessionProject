using DefaultNamespace;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Transform[] PlayerSpawns;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject botPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        var playerConfig = ConfigManager.instance.getPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfig.Length; i++)
        {
            var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<Player>().initializeConfigs(playerConfig[i]);
        }

        if (playerConfig.Length == 1) {
            var bot = Instantiate(botPrefab, PlayerSpawns[1].position, PlayerSpawns[1].rotation, gameObject.transform);
            
            
            bot.GetComponent<Player>(); // TODO : Revoir  ici
            
            // On pourrait avoir un singleplayer turnManager et un multiplayer turnmanager, à voir
        }
        
    }
}
