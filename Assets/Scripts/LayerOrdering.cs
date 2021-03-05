using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrdering : MonoBehaviour
{
    private Renderer renderer;
    [SerializeField] private bool sortOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sortingOrder = 1000 - (int)transform.position.y;
        if (sortOnce) Destroy(this);
    }
}
