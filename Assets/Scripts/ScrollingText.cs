using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private Transform text;
    private float speed = 75f;
    private float multiplier = 1f;
    private Vector2 input;
    private bool space;
    private Vector3 originalPosition;

    [SerializeField] private string sceneToLoad;
    
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
        if(text.transform.localPosition.y > 2200)
        {
            text.transform.position = originalPosition;
        }
    }

    public void SkipText()
    {
        if(space)
        {
            SceneManager.LoadScene(sceneToLoad);
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
