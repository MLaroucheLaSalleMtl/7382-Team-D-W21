using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject pauseMenu;

    public void OpenControlPanel()
    {
        controlPanel.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseControlPanel()
    {
        controlPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
