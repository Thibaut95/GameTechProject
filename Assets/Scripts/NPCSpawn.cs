using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;



public class NPCSpawn : NetworkBehaviour
{

    [SerializeField]
    private GameObject NPC;

    private float saved_time;
    // Start is called before the first frame update

    void Start()
    {
        if (!isServer)
            return;

        saved_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        if (Time.time - saved_time > 10)
        {

            GameObject npc = Instantiate(NPC);
            npc.GetComponent<Animator>().SetBool("Moving",true);
            // TODO position player

            npc.GetComponent<NavMeshAgent>().enabled = false;

            npc.transform.position = this.transform.position;
            // npc.transform.parent = transform;

            npc.GetComponent<NavMeshAgent>().enabled = true;

            NetworkServer.Spawn(npc);
            saved_time = Time.time;


        }
    }
}
