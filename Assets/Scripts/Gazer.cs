using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazer : MonoBehaviour //Enemy
{
    [SerializeField]
    Transform player; //player object

    [SerializeField]
    Transform rayOrigin;  //origin for raycasting (line of sight)

    [SerializeField]
    float movespeed;  //enemy's movespeed

    [SerializeField]
    float sightRange;  //range that enemy can see the player (agroRange)
    
    [SerializeField]
    Vector3 origin_position;  //original position that the creature starts at

    [SerializeField]
    float waitTime;  //time to wait between surveying and going back to idle

    [SerializeField]
    float positionBias;  //how close the object should be to its start position
    
    [SerializeField]
    private bool facingRight = true;
    private bool isAgro = false;

    private bool AtOrigin = true;
    private float currentTime;
    private float previousTime;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        origin_position = transform.position;
        currentTime = Time.time;
        previousTime = Time.time;
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        //Debug.Log(facingRight);
        //float playerdist = Vector2.Distance(transform.position, player.position);
        
        //make sure enemy is in range of seeing the player
        if(CanSeePlayer(sightRange)){
            AtOrigin = false;
            ChasePlayer();
            anim.Play("Base Layer.run", 0, 0);
        }
        else{
            //if the enemy was just following the player
            if(isAgro){

                //Invoke("ReturnToOrigin", waitTime);
                isAgro = false;
            }
            //has not been following the player for awhile, so return to original spot
            else{
                //if(origin_position != transform.position){
                if(transform.position.x > origin_position.x + positionBias || transform.position.x < origin_position.x - positionBias){
                    AtOrigin = false;
                    //Invoke("ReturnToOrigin", waitTime);
                    //ReturnToOrigin();
                }
                /*
                else{
                    if(AtOrigin){
                        if((currentTime - previousTime) > 200.00000)
                        Invoke("IdleScan", waitTime);
                        Debug.Log(currentTime);
                        Debug.Log(previousTime);
                        Debug.Log(currentTime - previousTime);
                        Debug.Log(waitTime);
                        previousTime = currentTime;
                    }
                    else{
                        AtOrigin = true;
                    }
                }*/
                //}
                /*
                else{
                    if(AtOrigin){
                        Invoke("IdleScan", waitTime);
                        Debug.Log("Swapped direction?");
                    }
                }*/
                //Debug.Log(origin_position.x);
                //Debug.Log(transform.position.x);
            }
            
        }
    }

    void IdleScan(){

        if(facingRight){
            facingRight = false;
        }
        else{
            facingRight = true;
        }
    }
    
    //returning to original spot in level
    void ReturnToOrigin(){
        float tempPos = origin_position.x - transform.position.x;
        if(tempPos < 0){
            facingRight = false;
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * movespeed;
        }
        else{
            facingRight = true;
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * movespeed;
        }
    }

    //follow/chase the player
    void ChasePlayer(){

        float tempPos = player.position.x - transform.position.x;
        if(tempPos < 0){
            facingRight = false;
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * movespeed;
        }
        else{
            facingRight = true;
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * movespeed;
        }
    }

    //check to see if enemy can see the player
    bool CanSeePlayer(float castDistance){

        bool result = false;

        Vector2 endPos = rayOrigin.position;
        if(!facingRight){
            endPos.x -= castDistance;
        }
        else{
            endPos.x += castDistance;
        }

        RaycastHit2D hit = Physics2D.Linecast(rayOrigin.position, endPos, 1 << LayerMask.NameToLayer("Default"));
        if(hit.collider != null){
            if(hit.collider.gameObject.CompareTag("Player")){
                result = true;
                isAgro = true;
                Debug.DrawLine(rayOrigin.position, hit.point, Color.red);
            }   
        }
        else{
            Debug.DrawLine(rayOrigin.position, endPos, Color.black);
        }
        return result;
    }
}
