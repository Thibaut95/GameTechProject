using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int mana = 30;


    private Slider healthBar;


    private Slider manaBar;


    private void Start()
    {
        healthBar=GameObject.Find("HUD").transform.Find("Health Bar").GetComponent<Slider>();
        manaBar=GameObject.Find("HUD").transform.Find("Mana Bar").GetComponent<Slider>();
        healthBar.value = health;
        manaBar.value = mana;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "item_health")
        {
            this.health += 10;
            if (this.health > 100) this.health = 100;
            Destroy(other.gameObject);
            healthBar.value = this.health;
        }
        else if (other.gameObject.tag == "item_mana")
        {
            this.mana += 10;
            if (this.mana > 100) this.mana = 100;
            Destroy(other.gameObject);
            manaBar.value = this.mana;
        }

    //    else if (other.gameObject.tag == "weapon")
    //    {
    //        this.health -= 20;
    //        healthBar.value = this.health;
    //    }

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
        this.health -= 20;
        healthBar.value = this.health;

        return health > 0;
    }

}
