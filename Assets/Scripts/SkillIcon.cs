using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject[] icons;
    private int iconIndex = 0;

    private void ChangeIcon()
    {
        if (player.SkillSwitch)
        {
            icons[iconIndex].SetActive(false);
            if (iconIndex + 1 < icons.Length)
            {
                iconIndex++;
            }
            else
            {
                iconIndex = 0;
            }
            icons[iconIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeIcon();
    }
}
