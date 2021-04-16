using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private GameObject player;
    //private CinemachineBrain brain;
    private float shakingDuration = 0f;

    public float ShakingDuration { get => shakingDuration; set => shakingDuration = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //brain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ShakingDuration >= 0f)
        {
            //if(brain.enabled) brain.enabled = !brain.enabled;
            ShakingDuration -= Time.fixedDeltaTime;
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(-0.2f, 0.2f);
            transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, transform.position.z);
        }
        else
        {
            //if (!brain.enabled) brain.enabled = !brain.enabled;
            this.enabled = !this.enabled;
        }
    }
}
