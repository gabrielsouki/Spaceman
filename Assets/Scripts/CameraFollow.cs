using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The transform element that this component parent will follow")]
    public Transform target;
    [Tooltip("Distance than this component parent will be from the target")]
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    [Tooltip("Delay to start the movement. Cinematic effect.")]
    public float dampingTime = 0.3f;
    [Tooltip("")]
    public Vector3 velocity = Vector3.zero;
    public static CameraFollow sharedInstance;

    private void Awake()
    {
        // Framerate that the game will try to run constantly
        Application.targetFrameRate = 60;
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }


    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    /* 
     * Define if the movement of the camera is instantly or smooth.
     * This will, for example, allow us to chose if the camera must move instantly in the dead-and-restart or move smoothly through the gameplay.
     */
    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);
        if(smooth)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampingTime);
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
