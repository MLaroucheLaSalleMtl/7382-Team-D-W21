using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private int deadBoss = 0;

    public int DeadBoss { get => deadBoss; set => deadBoss = value; }

    // Update is called once per frame
    void Update()
    {
        if(deadBoss >= 3)
        {
            this.gameObject.SetActive(false);
        }
    }
}
