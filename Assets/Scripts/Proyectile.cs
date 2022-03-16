using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float proyectileSpeed;
    float currentProyectileSpeed;

    Rigidbody2D m_ridigBody;

    public bool facingRight = false;

    Vector3 startPosition;


    private void Awake()
    {
        m_ridigBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        SetProyectileDirection();
        ProyectileMovement();
    }

    void SetProyectileDirection()
    {
        if (facingRight)
        {
            currentProyectileSpeed = proyectileSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentProyectileSpeed = -proyectileSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }
    }

    void ProyectileMovement()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            m_ridigBody.velocity = Vector2.right * currentProyectileSpeed;
        }
    }
}
