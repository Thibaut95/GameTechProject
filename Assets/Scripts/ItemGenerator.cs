using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class ItemGenerator : NetworkBehaviour
{
    [SerializeField]
    private int nbItem;

    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private float offset;
    // Start is called before the first frame update

    private float saved_time;

    void Start()
    {
        if (!isServer)
            return;


        System.Random random = new System.Random();
        int i = 0;
        while (i < nbItem)
        {
            float x = 500;
            float z = 500;

            float posX = (float)random.NextDouble() * x - 250;
            float posZ = (float)random.NextDouble() * z - 250;

            RaycastHit hit;
            Vector3 positionInit = new Vector3(posX, 100, posZ);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(positionInit, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                // Debug.DrawRay(positionInit, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
                float height = positionInit.y - hit.distance;
                if (height <= transform.position.y)
                {
                    GameObject item;
                    if (i < nbItem / 2)
                    {
                        item = Instantiate(items[0]);
                        item.GetComponent<IdItem>().id=i;
                    }
                    else
                    {
                        item = Instantiate(items[1]);
                    }

                    Vector3 position = new Vector3(posX, height + offset, posZ);
                    item.transform.position = position;
                    i++;
                    item.transform.parent = transform;
                    
                    NetworkServer.Spawn(item);
                }
            }
        }
    }

    public int getMaxCollectible()
    {
        return (int)nbItem / 2;
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
