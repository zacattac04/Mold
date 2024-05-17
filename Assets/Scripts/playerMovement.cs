using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public GameObject blackOutSquare;
    //speed for horizontal movement
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float crawlSpeed = 2.5f;

    //how high the character will jump
    [SerializeField]
    private float jumpforce = 10;

    [SerializeField]
    private float maxVerticalSpeed = 20;

    [SerializeField]
    private Rigidbody2D myBody;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private MonoBehaviour shadow;

    [SerializeField]
    private BoxCollider2D collider;

    [SerializeField]
    private bool crawlToggle;
    private bool currentlyCrawling = false;

    [SerializeField]
    private AudioSource footsteps;
    

    //variables that may be used multiple times or frequently
    private float xMov, yMov;


    //Positive direction (right) = true, negative direction (left) = false
    private bool direction = true;
    private bool onGround = true;

    private bool canHide = false;
    private bool hiding = false;
    private bool forceCrawl = false;
    private bool nearCrawlSpace;

    //All tags the the player will be interacting with
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private string HIDING_PLACE_TAG = "HidingPlace";
    private string CRAWL_SPACE_TAG = "crawlSpace";

    private string BLOCK_TAG = "Block";
    [SerializeField]
    private Animator anim;

    private Vector2 walkSize = new Vector2(0.4872923f, 0.781405f);
    private Vector2 walkOffset = new Vector2(0.03536217f, 0.3832514f);

    private Vector2 crawlSize = new Vector2(0.8823045f, 0.4854465f);
    private Vector2 crawlOffset = new Vector2(-0.01850334f, 0.2351222f);

    private Vector3 HidePosition;

    public locker currLocker;

    private CrawlSpace cSpace;
    private void awake(){
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeBlackOutSquare(false));
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused){
            PlayerMovement();
            PlayerJump();
            Hide();
            EnterCrawlSpace();
        }
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
            
            if(crawlInput()){
                if (footsteps.isPlaying) footsteps.Stop();
                anim.SetBool("IsCrawling", true);
                anim.SetBool("IsWalking", false);
                collider.size = crawlSize;
                collider.offset = crawlOffset;
                transform.position += new Vector3(xMov, 0, 0) * Time.deltaTime * crawlSpeed;
            }
            else{
                if (!footsteps.isPlaying && onGround) footsteps.Play();
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsCrawling", false);
                collider.size = walkSize;
                collider.offset = walkOffset;
                transform.position += new Vector3(xMov, 0, 0) * Time.deltaTime * speed;

            }
        }else{
            if (footsteps.isPlaying) footsteps.Stop();
            if(crawlInput()){
                anim.SetBool("IsCrawling", true);
                anim.SetBool("IsWalking", false);
                collider.size = crawlSize;
                collider.offset = crawlOffset;
            }
            else{
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsCrawling", false);
                collider.size = walkSize;
                collider.offset = walkOffset;
            }
        }
    }

    //Handles jumping from the ground
    void PlayerJump(){
        //make sure the player is only able to jump while on the ground
        if(Input.GetButtonDown("Jump") && onGround && canMove()){
            if (footsteps.isPlaying) footsteps.Stop();
            currentlyCrawling = false;
            onGround = false;
            myBody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            if(myBody.velocity.y >= maxVerticalSpeed){
                myBody.velocity = new Vector3(myBody.velocity.x, maxVerticalSpeed, 0f);
            }
            anim.SetBool("IsJumping", true);
        }
    }

    void Hide(){

        if(Input.GetButtonDown("Hide") && canHide && onGround) {
            hiding = !hiding;
            sprite.enabled = !hiding;
            shadow.enabled = !hiding;
            currLocker.playerEnter();
            if (footsteps.isPlaying) footsteps.Stop();

            if (hiding) {
                int hideLayer = LayerMask.NameToLayer("Hiding");
                gameObject.layer = hideLayer;
            } else {
                int PlayerLayer = LayerMask.NameToLayer("Player");
                gameObject.layer = PlayerLayer;
            }
        }
    }

    void EnterCrawlSpace(){
        if(Input.GetButtonDown("Hide") && nearCrawlSpace && onGround) {
            transform.position = cSpace.getPos().position;
            Debug.Log("Moved into Crawl Space");
            if (cSpace.isEntrance()) {
                forceCrawl = true;
                Debug.Log("Forcing crawl");
            } else {
                forceCrawl = false;
                Debug.Log("No longer forced to crawl");
            }
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
            if(!hiding){
                Death(1.5f);
            }
        }
        else if(collision.gameObject.CompareTag(BLOCK_TAG)){
            //if player is on top of the box, allow them to jump, but no push animation
            if(collision.gameObject.transform.position.y < transform.position.y){
                onGround = true;
                if(anim.GetBool("IsJumping")){
                    anim.SetBool("IsJumping", false);
                }
            }
            //1.233 is the buffer, since the center of their models are not at the same y position 
            //box is on player no animation change
            else if(collision.gameObject.transform.position.y > transform.position.y + 1.233){
                int temp = 0;
            }
            //player is in line with the box so play pushing animation
            else{
                anim.SetBool("IsPushing", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag(BLOCK_TAG)){
            anim.SetBool("IsPushing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag(HIDING_PLACE_TAG)) {
            canHide = true;
        }
        if (col.gameObject.CompareTag(CRAWL_SPACE_TAG)) {
            nearCrawlSpace = true;
            cSpace = col.gameObject.GetComponent<CrawlSpace>();
            Debug.Log("Entered Crawl Space");
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag(HIDING_PLACE_TAG)) {
            canHide = false;
        }
        if (col.gameObject.CompareTag(CRAWL_SPACE_TAG)) {
            nearCrawlSpace = false;
            Debug.Log("Exited Crawl Space");
        }
    }

    public bool isHiding() {
        return hiding;
    }
    void Death(float delay){
        StartCoroutine(DisableSpriteAfterDelay(0.3f));
        StartCoroutine(FadeBlackOutSquare());
        StartCoroutine(LoadLevelAfterDelay(delay));
    }
    IEnumerator LoadLevelAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DisableSpriteAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        sprite.enabled = false;
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2) {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack) {
            //yield return new WaitForSeconds(1f);
            while (blackOutSquare.GetComponent<Image>().color.a < 1) {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent <Image>().color = objectColor;
                yield return null;
            }
        } else {
            while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent <Image>().color = objectColor;
                yield return null;
            }
        }
    }

    private bool crawlInput() {
        if (crawlToggle) {
            if (Input.GetButtonDown("Crawl") && onGround) {
                currentlyCrawling = !currentlyCrawling;
            }
            if (currentlyCrawling && onGround) {
                return true;
            }
        } else {
            if (Input.GetButton("Crawl") && onGround) {
                return true;
            }
        }
        if (forceCrawl) {
            return true;
        }
        return false;
    }
}
