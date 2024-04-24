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

    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;
    public LayerMask detectorLayerMask;

    [Header("Gizmo parameters")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;

    // Start is called before the first frame update
    void Start()
    {
        if (facingRight) {
            detectorOriginOffset.x = -detectorOriginOffset.x;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(hasSeenPlayer){
            Walk(facingRight);
        }
        else{
            bool test;
            //test = CanSeePlayer(sightRange);
            test = BoxDetect();
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
        if (result) {
            Debug.Log("Player detected");
        }
        return result;
    }

    private void OnDrawGizmos() {
        if (showGizmos && detectorOrigin != null) {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }

    bool BoxDetect() {
        bool result = false;

        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0f, detectorLayerMask);
        if (collider != null) {
            if (collider.gameObject.CompareTag("Player")) {
                result = true;
                hasSeenPlayer = true;
            }
        }

        return result;
    }
}