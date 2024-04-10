using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locker : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject test;


    // Start is called before the first frame update
    void Start()
    {
        test.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerEnter(){
        anim.SetBool("isHiding", !anim.GetBool("isHiding"));
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            test.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            test.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
