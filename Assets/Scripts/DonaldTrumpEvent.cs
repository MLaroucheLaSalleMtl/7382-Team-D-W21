using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonaldTrumpEvent : MonoBehaviour
{
    [SerializeField] private DonaldTrumpAI donaldTrump;
    [SerializeField] private GameObject trumpWall;
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text text;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DonaldTrumpSpeech());
        }
    }

    private IEnumerator DonaldTrumpSpeech()
    {
        if(!activated)
        {
            activated = true;
            textBox.SetActive(true);
            text.text = "TRUMP: BRAVE HERO! I THANK YOU FOR SAVING AMERICA!";
            yield return new WaitForSeconds(4f);
            text.text = "TRUMP: HOWEVER, YOUR EXISTENCE IS A THREAT TO MY PRESIDENCY";
            yield return new WaitForSeconds(4f);
            text.text = "TRUMP: WHICH IS WHY... YOU MUST DIE! MAGA!";
            yield return new WaitForSeconds(4f);
            textBox.SetActive(false);
            donaldTrump.Activated = true;
            Destroy(gameObject);
        }
    }
}
