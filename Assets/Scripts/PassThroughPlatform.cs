using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{

    private Collider2D collider;
    private bool playerOnPlatform;

    [SerializeField]
    private float waitTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0) {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        } 
    }

    private IEnumerator EnableCollider() {
        yield return new WaitForSeconds(waitTime);
        collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value) {
        GameObject player = other.gameObject;
        if (player != null && player.CompareTag("Player")) {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other) {
        SetPlayerOnPlatform(other, false);
    }
}
