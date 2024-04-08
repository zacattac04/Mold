using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    private string ENEMY_TAG = "Enemy";

    [SerializeField]
    float outwardDamage = 1;

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.CompareTag(ENEMY_TAG)){
            collision.gameObject.TryGetComponent<Enemy>(out Enemy curr_enemy);
            if(transform.position.y > curr_enemy.transform.position.y){
                curr_enemy.takeDamage(outwardDamage);   
                anim.SetBool("isBroken", true);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}
