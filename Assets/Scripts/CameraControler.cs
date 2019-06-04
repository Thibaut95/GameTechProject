using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    [SyncVar] private bool gameOver = false;

    private GameObject gameOverCanvas = null;

    private int lastTrigger = -1;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int score = 0;


    [SerializeField]
    private int MaxCollectible;

    private Slider healthBar;
    private Text playerscore_txt;
    private Text opponent_txt;
    private HashSet<int> itemsCollected;

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
        gameOverCanvas = GameObject.Find("tamere");
        gameOverCanvas.SetActive(false);
        Debug.Log(gameOverCanvas);

        healthBar=GameObject.Find("HUD").transform.Find("Health Bar").GetComponent<Slider>();
        healthBar.value = health;
        playerscore_txt=GameObject.Find("HUD").transform.Find("player score").GetComponent<Text>();
        opponent_txt=GameObject.Find("HUD").transform.Find("opponent score").GetComponent<Text>();
        itemsCollected= new HashSet<int>();

        MaxCollectible=25;

    }




    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (gameOver){
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
            return;
        }

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

    void FixedUpdate()
    {
         if (!isLocalPlayer)
            return;

        opponent_txt.text = "Opponent score : "+(MaxCollectible - GameObject.FindGameObjectsWithTag("item_collectible").Length - score);
    }

    [Command]
    private void CmdDestroyItem(GameObject item)
    {
        NetworkServer.Destroy(item);
    }

    public void SetGameOver(bool value)
    {
        if (isServer) {
            gameOver = gameOver;
        } else {
            CmdSetGameOver (value);
        }
    }
    
    [Command]
    void CmdSetGameOver(bool value)
    {
        SetGameOver(value);
    }


    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
            return;

        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "weapon" || other.gameObject.tag == "weapon_skeleton")
        {
            bool isPlayerDead = !removeHealth();
            
            CmdSetGameOver(isPlayerDead || gameOver);
            
        }
        else if (other.gameObject.tag == "item_health" || other.gameObject.tag == "item_collectible")
        {

            // if (other.GetInstanceID() != lastTrigger){

                itemsCollected.Add(other.GetInstanceID());
                bool isAllCollected = !increaseItem(other.gameObject.tag);
                
                CmdDestroyItem(other.gameObject);

                CmdSetGameOver(isAllCollected || gameOver);
               
                

                lastTrigger = other.GetInstanceID();

            // }

            
        }
        

    }

    public bool removeHealth()
    {
        this.health -= 5;
        healthBar.value = this.health;

        return health > 0;
    }

    public bool increaseItem(string type)
    {
        if (type == "item_health")
        {
            this.health += 10;
            if (this.health > 100) this.health = 100;
            healthBar.value = this.health;
        }
        else if (type == "item_collectible")
        {
            

            score=itemsCollected.Count;
            
            playerscore_txt.text = "Player score : "+score;
            
            return (GameObject.FindGameObjectsWithTag("item_collectible").Length) > 0;

        }
        return this.health > 0;
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
