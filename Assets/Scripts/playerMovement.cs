using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    //speed for horizontal movement
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float crawlSpeed = 2.5f;

    //how high the character will jump
    [SerializeField]
    private float jumpforce = 10;

    
    [SerializeField]
    private Rigidbody2D myBody;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private MonoBehaviour shadow;

    private int hp = 1;

    //variables that may be used multiple times or frequnetly
    private float xMov, yMov;


    //Positive direction (right) = true, negative direction (left) = false
    private bool direction = true;
    private bool onGround = true;

    private bool canHide = false;
    private bool hiding = false;

    //All tags the the player will be interacting with
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private string HIDING_PLACE_TAG = "HidingPlace";

    [SerializeField]
    private Animator anim;

    private void awake(){
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        Hide();
    }

    //Player's x and y movement not including special movement options
    void PlayerMovement(){
        
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");

        if(xMov > 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            direction = true;
        }
        else if(xMov < 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            direction = false;
        }

        //Debug.Log(xMov);
        if(canMove())
        //transform.position += new Vector3(xMov, 0, 0) * Time.deltaTime * speed;

        if(xMov != 0){
            if(Input.GetButton("Crawl") && onGround){
                anim.SetBool("IsCrawling", true);
                anim.SetBool("IsWalking", false);
                transform.position += new Vector3(xMov, 0, 0) * Time.deltaTime * crawlSpeed;
            }
            else{
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsCrawling", false);
                transform.position += new Vector3(xMov, 0, 0) * Time.deltaTime * speed;
            }
        }
        else{
            if(Input.GetButton("Crawl") && onGround){
                anim.SetBool("IsCrawling", true);
                anim.SetBool("IsWalking", false);
            }
            else{
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsCrawling", false);
            }
        }
    }

    //Handles jumping from the ground
    void PlayerJump(){
        //make sure the player is only able to jump while on the ground
        if(Input.GetButtonDown("Jump") && onGround && canMove()){
            onGround = false;
            myBody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            anim.SetBool("IsJumping", true);
        }
    }

    void Hide(){
        if(Input.GetButtonDown("Hide") && canHide && onGround) {
            hiding = !hiding;
            sprite.enabled = !hiding;
            shadow.enabled = !hiding;
        }
    }

    bool canMove() {
        return (!hiding);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag(GROUND_TAG)){
            onGround = true;
            if(anim.GetBool("IsJumping")){
                anim.SetBool("IsJumping", false);
            }
        }
        else if(collision.gameObject.CompareTag(ENEMY_TAG)){
            Death();
        } 
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag(HIDING_PLACE_TAG)) {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag(HIDING_PLACE_TAG)) {
            canHide = false;
        }
    }

    public bool isHiding() {
        return hiding;
    }
    void Death(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
