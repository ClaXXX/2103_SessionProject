using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Mirror;
using UI.Network;
using UnityEngine;
using UnityEngine.Rendering;

public class ServerManager : NetworkBehaviour
{
    public NetworkMenus NetworkMenus { get; protected set; }
    private GameManager _gameManager;
    public List<NetworkConnection> Connections { get; protected set; }
    private static event Action Test;

    private void Start()
    {
        InitNetworkMenu();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        Test += CleanMenu;
        Debug.Log("HELOCSBCLEJZBCL");
    }

    public void CleanMenu()
    {
        NetworkMenus.SetMenuActive(false, "Client");
    }

    private void OnDestroy()
    {
        Test -= ClientLaunchGame;
    }

    #region clientRPC

    [ClientRpc]
    public void RpcLaunchGame()
    {
        Debug.Log("Hello worlldazkjebajzevbjhvzevb");
        Test?.Invoke();
    }

    
    [Command]
    public void CommandLaunchGame()
    {
        Debug.Log("Hello worlldazkjebajzevbjhvzevb");
        RpcLaunchGame();
    }

    [Client]
    public void ClientLaunchGame()
    {
        CommandLaunchGame();
    }

    public void LaunchGame()
    {
        Debug.Log("Zeerzetjcmoijhhhzay");
        _gameManager.LaunchGame(2);
    }

    #endregion

    #region ServerManager

    public void AddClient(NetworkConnection connection)
    {
        if (Connections == null)
        {
            Connections = new List<NetworkConnection>();
        }

        if (!_gameManager)
        {
            InitGameManager();
        }
        Connections.Add(connection);
        _gameManager.ClientConnect();
    }

    [ClientRpc]
    public void End()
    {
        _gameManager.Quit();
    }

    #endregion
    
    void InitGameManager()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager not found");
        }
    }
    
    void InitNetworkMenu()
    {
        NetworkMenus = FindObjectOfType<NetworkMenus>();
        if (NetworkMenus == null)
        {
            Debug.LogError("Game Manager not found");
        }
    }

}
