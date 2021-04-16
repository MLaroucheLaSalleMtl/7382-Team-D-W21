using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private AudioSource healSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player.Hp < player.MaxHp || player.Mp < player.MaxMp)
            {
                player.Hp = player.MaxHp;
                player.Mp = player.MaxMp;
                GameObject heal = Instantiate(healPrefab, transform.position, Quaternion.identity);
                Heal script = heal.GetComponent<Heal>();
                script.UserTag = "Player";
                healSound.Play();
            }
        }
    }
}
