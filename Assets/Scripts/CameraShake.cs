using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private GameObject player;
    private float ShakingDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ShakingDuration >= 0f)
        {
            ShakingDuration -= Time.fixedDeltaTime;
            float x = Random.Range(-0.1f, 0.1f);
            transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x , player.transform.position.y, transform.position.z);
        }
    }
}
