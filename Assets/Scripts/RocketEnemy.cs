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
            case "Player": collision.GetComponent<PlayerController>().CollectHealth(-rocketDamage);
                break;
            case "Coin":
                break;
            default: m_proyectile.facingRight = !m_proyectile.facingRight;
                break;
        }
    }
}
