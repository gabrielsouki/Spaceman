using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwnPlatforms : MonoBehaviour
{
    [SerializeField]
    float timer = 0, maxTimer;
    [SerializeField]
    GameObject platform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(platform, this.transform);
            timer = maxTimer;
        }
    }
}
