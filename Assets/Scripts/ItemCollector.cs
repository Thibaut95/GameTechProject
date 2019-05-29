using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int score = 0;


    [SerializeField]
    private int MaxCollectible;

    private Slider healthBar;
    private Text playerscore_txt;
    private Text opponent_txt;

    private bool stateIncrase = false;

    private void Start()
    {
        healthBar=GameObject.Find("HUD").transform.Find("Health Bar").GetComponent<Slider>();
        healthBar.value = health;
        playerscore_txt=GameObject.Find("HUD").transform.Find("player score").GetComponent<Text>();
        opponent_txt=GameObject.Find("HUD").transform.Find("opponent score").GetComponent<Text>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.tag);

        // if (other.gameObject.tag == "item_health")
        // {
        //     this.health += 10;
        //     if (this.health > 100) this.health = 100;
        //     Destroy(other.gameObject);
        //     healthBar.value = this.health;
        // }
        // else if (other.gameObject.tag == "item_mana")
        // {
        //     this.score += 10;
        //     if (this.score > 100) this.score = 100;
        //     Destroy(other.gameObject);
        //     manaBar.value = this.score;
        // }

    //    else if (other.gameObject.tag == "weapon")
    //    {
    //        this.health -= 20;
    //        healthBar.value = this.health;
    //    }

    }

    void FixedUpdate()
    {
        if(stateIncrase)
        {
            this.score += 1;
            playerscore_txt.text = "Player score : "+score;
            opponent_txt.text = "Opponent score : "+(MaxCollectible - GameObject.FindGameObjectsWithTag("item_collectible").Length - score);
            stateIncrase = false;
        }
    }

    public int getHealth()
    {
        return this.health;
    }


    public void setHealth(int health)
    {
        this.health = health;
    }

    public bool removeHealth()
    {
        this.health -= 5;
        healthBar.value = this.health;

        return health > 0;
    }

    public int getScore(){
        return score;
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
            
            stateIncrase = true;
            return (GameObject.FindGameObjectsWithTag("item_collectible").Length) > 0;

        }
        return this.health > 0;
    }

}
