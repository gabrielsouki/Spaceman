using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovingPlatform : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;
    PauseMenu m_pauseMenu;

    private void Start()
    {
        m_pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
    }

    void Update()
    {
        if (!m_pauseMenu.isPaused)
        {
            transform.RotateAround(this.transform.parent.position, Vector3.back, rotationSpeed);
            transform.up = Vector3.up;
        }
        
    }
}
