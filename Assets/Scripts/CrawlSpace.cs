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

    [SerializeField]
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<SpriteRenderer>().enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            text.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            text.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
