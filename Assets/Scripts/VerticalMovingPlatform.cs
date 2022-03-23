using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField]
    float velocity, timer;
    Rigidbody2D m_rigidBody;
    GameObject m_player;

    void Start()
    {
        m_player = GameObject.Find("Player");
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();

        if (this.transform.parent.name == "PlatformSpawner_Down")
        {
            m_rigidBody.velocity = Vector2.up * velocity * Time.deltaTime;
        }
        if(this.transform.parent.name == "PlatformSpawner_Up")
        {
            m_rigidBody.velocity = Vector2.down * velocity * Time.deltaTime;
        }
    }

    void Destroy()
    {
        Timer();
        if(timer <= 0)
        {
            
            if(m_player.transform.parent == this.gameObject.transform)
            {
                m_player.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }

    void Timer()
    {
        timer -= Time.deltaTime;
    }
}
