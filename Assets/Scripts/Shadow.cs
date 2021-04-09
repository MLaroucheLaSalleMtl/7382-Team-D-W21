using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform caster;
    private Vector3 startingPosition;
    private SpriteRenderer sprite;
    private Color color;
    private float startingDistance;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startingPosition = caster.position;
        startingDistance = Vector3.Distance(startingPosition, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3(distance / startingDistance, distance / startingDistance, 0f);
        distance = Vector3.Distance(caster.position, startingPosition);
        if (distance / startingDistance <= 0.95f) color.a = (distance / startingDistance) * 0.8f;
        else color.a = 0f;
        sprite.color = color;
    }
}
