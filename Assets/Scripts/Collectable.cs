using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType {healthPotion, manaPotion, money}

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public int value;
    SpriteRenderer sprite;
    CircleCollider2D m_collider;
    bool hasBeenCollected = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<CircleCollider2D>();
    }

    /*void ShowItem()
    {
        sprite.enabled = true;
        m_collider.enabled = true;
        hasBeenCollected = false;
    }*/

    void HideItem()
    {
        sprite.enabled = false;
        m_collider.enabled = false;
    }

    void CollectItem()
    {
        HideItem();
        //hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.collectedCoins += this.value;
                GetComponent<AudioSource>().Play();
                break;

            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(this.value);
                break;

            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(this.value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CollectItem();
        }
    }
}
