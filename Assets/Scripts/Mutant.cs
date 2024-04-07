using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
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
    private bool facingRight = true;
    private bool hasSeenPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(hasSeenPlayer){
            Walk(facingRight);
        }
        else{
            bool test;
            test = CanSeePlayer(sightRange);
        }
    }


    void Walk(bool isFacingRight){

        if(isFacingRight){
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * movespeed;
        }
        else{
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * movespeed;
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
            GameObject obj = hit.collider.gameObject;
            if(obj.CompareTag("Player") && !obj.GetComponent<playerMovement>().isHiding()){
                result = true;
                hasSeenPlayer = true;
                Debug.DrawLine(rayOrigin.position, hit.point, Color.red);
            }   
        }
        else{
            Debug.DrawLine(rayOrigin.position, endPos, Color.black);
        }
        return result;
    }
}