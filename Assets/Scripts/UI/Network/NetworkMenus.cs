using System;
using UnityEngine;

namespace UI.Network
{
    public class NetworkMenus : MonoBehaviour
    {
        public GameObject hostObj;
        public GameObject clientObj;

        public void SetMenuActive(bool setActive, String type)
        {
            Debug.Log("LOLOLOL");
            (type == "Client" ? clientObj : hostObj).SetActive(setActive);
        }
    }
}