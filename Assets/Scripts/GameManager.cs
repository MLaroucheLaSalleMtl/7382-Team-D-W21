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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Defeat()
    {
        if (player.IsDead())
        {
            SceneManager.LoadScene(0);
        }
    }

    private void VictoryScreen()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Defeat();
    }
}
