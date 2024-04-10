using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locker : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

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

    public void playerEnter(){
        anim.SetBool("isHiding", !anim.GetBool("isHiding"));
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
