using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuManagment :  MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
       
    }
}
