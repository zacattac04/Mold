using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrawlSpace : MonoBehaviour
{
    [SerializeField]
    private bool entrance;

    [SerializeField]
    private BoxCollider2D col;

    [SerializeField]
    private Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isEntrance() {
        return entrance;
    }

    public Transform getPos() {
        return pos;
    }
}
