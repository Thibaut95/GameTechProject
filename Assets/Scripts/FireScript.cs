using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{

    [SerializeField]
    private GameObject explosionPrefabs;
    // Start is called before the first frame update
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "PlayerTag")
        {
            Debug.Log("trigger");
            GameObject explosion = Instantiate(explosionPrefabs);
            explosion.transform.position = transform.position;
            Destroy(this.gameObject);
            Destroy(explosion.gameObject,1);
        }

    }
}
