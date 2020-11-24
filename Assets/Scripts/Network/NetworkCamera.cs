using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkCamera : NetworkBehaviour
{
    public Camera camera;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }

    public void OnEnable()
    {
        if (!isServer)
            return;
        RPCenableAll(true);
    }

    [ClientRpc]
    public void RPCenableAll(bool set)
    {
        enableAll(set);
    }

    [Client]
    public void enableAll(bool set)
    {
        this.enabled = set;
    }
}
