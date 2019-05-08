using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraControler : NetworkBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jump;

    [SerializeField]
    private float rotationSpeed;

    private Animator animator;

    private Camera camera3rd;

    private Camera cameraFPS;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
            return; 

        animator = GetComponent<Animator>();
        camera3rd = transform.Find("Camera3rd").GetComponent<Camera>();
        cameraFPS = transform.Find("Camera2").GetComponent<Camera>();
        cameraFPS.enabled = false;
        camera3rd.enabled = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Debug.Log(translation);

        this.transform.Translate(0,0,translation);
        this.transform.Rotate(0, rotation, 0);

        animator.SetBool("Moving",Input.GetAxis("Vertical")!=0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0,jump,0),ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Slash");
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Death");
        }
        
        if(Input.GetKeyDown(KeyCode.G))
        {   
            cameraFPS.enabled = !cameraFPS.enabled;
            camera3rd.enabled = !camera3rd.enabled;
        }
    }


}
