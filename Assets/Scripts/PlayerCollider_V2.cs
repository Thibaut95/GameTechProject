using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider_V2 : MonoBehaviour
{
    [SerializeField]
    private float offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 positionInit = new Vector3(transform.position.x,100,transform.position.z);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(positionInit,  transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            Debug.Log(hit.distance);
            Debug.DrawRay(positionInit, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            float height = positionInit.y - hit.distance;
            Vector3 position = this.transform.position;
            position.y = height + offset;
            this.transform.position = position;
        }

    }
}

