using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemPlacement : MonoBehaviour
{
    [SerializeField]
    private float offset;
    void Awake()
    {
       
    }


    void OnMouseDown()
    {
        Debug.Log("Mouse");    
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
       
        Vector3 positionInit = new Vector3(transform.position.x,100,transform.position.z);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(positionInit, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            //Debug.DrawRay(positionInit, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            float height = positionInit.y - hit.distance;
            Vector3 position = this.transform.position;
            position.y = height + offset;
            this.transform.position = position;
        }
        
        
    }
}
