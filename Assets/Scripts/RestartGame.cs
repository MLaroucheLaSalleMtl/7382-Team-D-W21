using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private bool space;
               
    public void GetSpace(InputAction.CallbackContext context)
    {
        space = context.performed;
    }

    public void GoBackToMenu()
    {
        if(space)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GoBackToMenu();
    }
}

