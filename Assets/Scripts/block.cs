using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    private string ENEMY_TAG = "Enemy";
    private string GROUND_TAG = "Ground";

    [SerializeField]
    float outwardDamage = 1;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Rigidbody2D myBody;

    [SerializeField]
    private float maximumVerticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myBody.velocity.y);
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
        else if(collision.gameObject.CompareTag(GROUND_TAG)){
            Debug.Log(myBody.velocity.y);
            if(Mathf.Abs(myBody.velocity.y) > maximumVerticalSpeed){
                anim.SetBool("isBroken", true);
                Destroy(gameObject, 1.0f);
                Debug.Log("Destroyed due to speed being " + myBody.velocity.y);
            }
        }
    }
}
