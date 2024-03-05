using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    string PLAYER_TAG = "Player";

    [SerializeField]
    spawner_block spawner;

    [SerializeField]
    Transform player; //player object

    [SerializeField]
    float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.CompareTag(PLAYER_TAG)){
            //if(transform.position.y < player.transform.position.y){

            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * movespeed;
            spawner.SpawnObject();
            //}
        }
    }
}
