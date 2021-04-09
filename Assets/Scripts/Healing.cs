using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;
    public Player PlayerStat;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && PlayerStat.Hp < 100 || PlayerStat.Mp < 100)
        {
            PlayerStat.Hp = PlayerStat.MaxHp;
            PlayerStat.Mp = PlayerStat.MaxMp;
        }
        Debug.Log("Healed");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
