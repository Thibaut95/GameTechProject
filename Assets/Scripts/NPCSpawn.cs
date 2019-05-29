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
        saved_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - saved_time > 30)
        {

            GameObject npc = Instantiate(NPC);
            npc.transform.GetChild(0).GetComponent<Animator>().SetBool("Moving",true);
            // TODO position player

            Debug.Log("npc spawned");
            npc.GetComponent<NavMeshAgent>().enabled = false;

            npc.transform.position = this.transform.position;
            // npc.transform.parent = transform;

            npc.GetComponent<NavMeshAgent>().enabled = true;

            NetworkServer.Spawn(npc);
            saved_time = Time.time;


        }
    }
}
