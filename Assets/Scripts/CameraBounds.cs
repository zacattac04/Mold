using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField]
    private float xBias, yBias;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetXBias(){
        return xBias;
    }

    public float GetYBias(){
        return yBias;
    }
}
