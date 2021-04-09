using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    public Transform text;
    public float speed = 50f;
    public float multiplier = 1f;
    public Vector2 input;
    public bool space;
    public Vector3 originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = text.transform.position;
    }

    public void ReadKey(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void ReadSpace(InputAction.CallbackContext context)
    {
        space = context.performed;
    }

    public void ResetText()
    {
        if(text.transform.localPosition.y > 2000)
        {
            text.transform.position = originalPosition;
        }
    }

    public void SkipText()
    {
        if(space)
        {
            SceneManager.LoadScene("World");
        }
    }

    void Scroll()
    {
        text.transform.position += Vector3.up * (speed * Time.fixedDeltaTime * multiplier);
        if (input.y > 0)
        {
            multiplier = 2f;
        }
        else if (input.y < 0)
        {
            multiplier = -2f;
        }
        else 
        {
            multiplier = 1f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Scroll();
        ResetText();
        SkipText();
    }
}
