using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class NPCMove : NetworkBehaviour
{

    Transform _destination;

    NavMeshAgent _navMeshAgent;

    float saved_time;

    bool navMeshToggle = true;

    private NetworkAnimator networkAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent != null)
        {

            if (_destination == null)
            {
                _destination = FindClosestPlayer().transform;
            }

            setDestination();

            saved_time = Time.time;
        }

        networkAnimator = GetComponent<NetworkAnimator>();

    }

    void FixedUpdate()
    {
        if (!isServer)
            return;

        if (Time.time - saved_time > 2)
        {
            _destination = FindClosestPlayer().transform;
            setDestination();

            saved_time = Time.time;
        }
    }

    private void setDestination()
    {
        if (!isServer)
            return;

        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (!isServer)
            return;

        if (other.gameObject.tag == "weapon")
        {
            NetworkServer.Destroy(this.gameObject);
        }
    }

    private GameObject FindClosestPlayer()
    {

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if(distance<50)
        {
            networkAnimator.SetTrigger("Slash");
        }
        return closest;
    }
}
