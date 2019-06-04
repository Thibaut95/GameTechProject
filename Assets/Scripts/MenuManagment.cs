using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuManagment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager networkManager = this.gameObject.GetComponent<NetworkManager>();

        networkManager.StartServer();
        networkManager.StartHost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
