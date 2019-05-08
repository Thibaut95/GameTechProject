using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlameThrower : NetworkBehaviour
{

    [SerializeField]
    private GameObject firePrefabs;



    [SerializeField]
    private float offsetHeight;

    [SerializeField]
    private float offset;

    [SerializeField]
    private float thrust;



    // Start is called before the first frame update
    void Start()
    {

    }

    [Command]
    void CmdFire()
    {
        GameObject fire = Instantiate(firePrefabs);
        Vector3 position = transform.position;
        position.y += offsetHeight;
        position += transform.forward * offset;
        fire.transform.position = position;

        // Debug.Log(new Vector3(1,0,0)*thrust);

        // rigidbody.AddForce(new Vector3(1,0,0)*thrust,ForceMode.Impulse);

        fire.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
        // // This [Command] code is run on the server!

        // // create the bullet object locally
        // var bullet = (GameObject)Instantiate(firePrefabs, transform.position - transform.forward, Quaternion.identity);

        // bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;

        // spawn the bullet on the clients
        NetworkServer.Spawn(fire);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(fire, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            CmdFire();
        }
    }
}
