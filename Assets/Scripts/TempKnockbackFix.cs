using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempKnockbackFix : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void FixKnockback()
    {
        if(!rigid.isKinematic)
        {
            rigid.isKinematic = true;
            //rigid.velocity = Vector2.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        InvokeRepeating("FixKnockback", 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
