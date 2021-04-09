using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrdering : MonoBehaviour
{
    private Renderer render;
    [SerializeField] private bool sortOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        render.sortingOrder = 1000 - (int)transform.position.y;
        if (sortOnce) Destroy(this);
    }
}
