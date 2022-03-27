using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemy : MonoBehaviour
{
    [SerializeField]
    int rocketDamage;
    Proyectile m_proyectile;

    // Start is called before the first frame update
    void Start()
    {
        m_proyectile = this.gameObject.GetComponent<Proyectile>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                break;
            case "Coin":
                break;
            case "ExitZone":
                break;
            case "MovingPlatform":
                break;
            case "Potion":
                break;
            default: m_proyectile.facingRight = !m_proyectile.facingRight;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().CollectHealth(-rocketDamage);
        }
    }
}
