using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonFlameWall : MonoBehaviour
{
    [SerializeField] private GameObject flameWallPrefab;
    private Animator animator;

    [SerializeField] private bool activated = false;
    [SerializeField] private float wallSpeed = 50f;

    public bool Activated { get => activated; set => activated = value; }

    private IEnumerator SpawnFlameWall()
    {
        Activated = false;
        GameObject flameWall = Instantiate(flameWallPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rigid = flameWall.GetComponent<Rigidbody2D>();
        animator.SetTrigger("Activate");
        yield return new WaitForSeconds(2f);
        rigid.AddForce(Vector3.down * wallSpeed, ForceMode2D.Impulse);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Activated) StartCoroutine(SpawnFlameWall());
    }
}
