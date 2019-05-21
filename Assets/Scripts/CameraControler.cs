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
    private NetworkAnimator networkAnimator;

    private Camera camera3rd;

    private Camera cameraFPS;
    // Start is called before the first frame update

    private bool isgrounded = false;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        animator = GetComponent<Animator>();
        networkAnimator = GetComponent<NetworkAnimator>();
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

        this.transform.Translate(0, 0, translation);
        this.transform.Rotate(0, rotation, 0);

        animator.SetBool("Moving", Input.GetAxis("Vertical") != 0);

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            networkAnimator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            networkAnimator.SetTrigger("Slash");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            networkAnimator.SetTrigger("Death");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            cameraFPS.enabled = !cameraFPS.enabled;
            camera3rd.enabled = !camera3rd.enabled;
        }
    }

    [Command]
    private void CmdDestroyItem(GameObject item)
    {
        NetworkServer.Destroy(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (!isLocalPlayer)
        //     return;

        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "weapon" && isLocalPlayer)
        {
            this.GetComponent<ItemCollector>().removeHealth();
        }
        else if (other.gameObject.tag == "item_health" || other.gameObject.tag == "item_collectible")
        {
            if (isLocalPlayer){
                this.GetComponent<ItemCollector>().increaseItem(other.gameObject.tag);
                CmdDestroyItem(other.gameObject);
            }
            else{
                CmdDestroyItem(other.gameObject);
            }
            
        }
        

    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "terrain")
        {
            isgrounded = true;
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "terrain")
        {
            isgrounded = false;
        }
    }

}
