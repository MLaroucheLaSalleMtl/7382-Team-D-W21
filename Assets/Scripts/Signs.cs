using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signs : MonoBehaviour
{
    public GameObject textBox;
    public string textContent;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        textBox.CompareTag("Sign");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            text.text = textContent;
            textBox.SetActive(true);
        }
        //Debug.Log("Sign");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            textBox.SetActive(false);
        }
        //Debug.Log("Left sign");
    }
}
