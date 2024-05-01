using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vent : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject ventCovering;

    [SerializeField]
    private GameObject ventEntrance;

    // Start is called before the first frame update
    void Start()
    {
        ventEntrance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player")) {
            anim.SetBool("IsOpen", true);
            ventCovering.SetActive(false);
            ventEntrance.SetActive(true);
        }
    }
}
