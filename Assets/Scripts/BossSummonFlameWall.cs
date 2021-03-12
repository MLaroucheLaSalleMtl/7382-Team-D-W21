using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonFlameWall : MonoBehaviour
{
    [SerializeField] private GameObject flameWallPrefab;
    private Rigidbody2D rigid;

    private float wallSpeed = 20f;

    private IEnumerator PushFlameWall()
    {
        yield return new WaitForSeconds(3f);
        rigid.AddForce(Vector3.down * wallSpeed, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Awake()
    {
        GameObject flameWall = Instantiate(flameWallPrefab, transform.position, Quaternion.identity);
        rigid = flameWall.GetComponent<Rigidbody2D>();
        StartCoroutine(PushFlameWall());
    }
}
