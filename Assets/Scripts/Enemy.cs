using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float maxHP = 1;

    float health = 0;

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHP;
        Physics2D.IgnoreLayerCollision(2, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void takeDamage(float damageAmount){
        
        //origin position moved to gazer
        //Debug.Log(origin_position + " Origin " + transform.position + " Current position ");
        health -= damageAmount;
        //transform.position = origin_position;

        if(health <= 0){
            anim.SetBool("Death", true);
            Destroy(gameObject, 0.4f);
        }
    }
}
