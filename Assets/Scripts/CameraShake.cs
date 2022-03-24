using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnimator;
    const string STATE_CAMERA_SHAKE = "shake";
    public void ShakeCamera()
    {
        camAnimator.SetTrigger(STATE_CAMERA_SHAKE);
    }
}
