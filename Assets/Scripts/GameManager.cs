using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private Player player;
    [SerializeField] private CombatEntity logBoss;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
