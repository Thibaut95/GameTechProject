using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColider : MonoBehaviour
{
    [SerializeField]
    private Terrain terrain;

    [SerializeField]
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float height = terrain.SampleHeight(this.transform.position);
        Vector3 position = this.transform.position;
        position.y = height + offset;
        this.transform.position = position;
    }
}
