using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject PauseMenuUI;
    private bool pause = false;

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        pause = context.performed;
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("Joystick button 7"))
        if (pause)
        {
            pause = false;
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
