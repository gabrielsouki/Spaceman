using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField]
    float velocity;
    Rigidbody2D m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.parent.name == "PlatformSpawner_Down")
        {
            m_rigidBody.velocity = Vector2.up * velocity * Time.deltaTime;
        }
        if(this.transform.parent.name == "PlatformSpawner_Up")
        {
            m_rigidBody.velocity = Vector2.down * velocity * Time.deltaTime;
        }
    } 
}
