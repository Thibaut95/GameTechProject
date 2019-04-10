using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject fire = Instantiate(firePrefabs);
            Vector3 position = transform.position;
            position.y += offsetHeight;
            position += transform.forward * offset;
            fire.transform.position = position;

            // Debug.Log(new Vector3(1,0,0)*thrust);

            // rigidbody.AddForce(new Vector3(1,0,0)*thrust,ForceMode.Impulse);

            fire.GetComponent<Rigidbody>().AddForce(transform.forward*thrust,ForceMode.Impulse);

        }
    }
}
