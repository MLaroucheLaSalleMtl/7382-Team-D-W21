using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private string axisTag;
    private Transform axis;

    private Vector3 position;
    [SerializeField] private float distance;
    private float angle;
    [SerializeField] private float travelSpeed;
    [SerializeField] private float orbitSpeed;

    public float Angle { get => angle; set => angle = value; }

    // Start is called before the first frame update
    void Start()
    {
        axis = GameObject.FindWithTag(axisTag).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = new Vector3(0f, distance, 0f);
        position = Quaternion.AngleAxis(Angle, Vector3.forward) * position;
        Angle += orbitSpeed * Time.fixedDeltaTime;
        distance += travelSpeed * Time.fixedDeltaTime;
        transform.position = axis.position + position;
    }
}
