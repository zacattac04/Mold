/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedFollowPlayer : MonoBehaviour
{

    private Transform player;

    private Vector3 tempPos;

    [SerializeField] private Transform Target;

    [SerializeField]
    private bool bounds;
    private GameObject[] boundaries;
    private Bounds[] allBounds;
    private Bounds targetBounds;
    private BoxCollider2D camBox;
    public float xBias;
    public float yBias;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;   
        Target = Player.controller.camTarget;
        FindLimits();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPos = transform.position;

        tempPos.x = player.position.x;
        tempPos.y = player.position.y;

        transform.position = tempPos;
        
    }

    public void FindLimits() {
        boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        allBounds = new Bounds[boundaries.Length];
        for (int i = 0; i < allBounds.Length; i++) {
            allBounds[i] = boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
        }
    }

    void SetOneLimit()
    {
        bool first = true;
        for (int i = 0; i < boundaries.Length; i++) {
            if (withinBounds(boundaries[i])) {
                if (first)
                {
                    targetBounds = boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
                    xBias = boundaries[i].gameObject.GetComponent<CameraBounds>().GetXBias();
                    yBias = boundaries[i].gameObject.GetComponent<CameraBounds>().GetYBias();
                    first = false;
                }
                else {
                    combineLimits(boundaries[i]);
                }
            }
        }

    }

    void combineLimits(GameObject newBounds) {
        // print("combining limits");
        float x2 = newBounds.gameObject.GetComponent<CameraBounds>().GetXBias();
        float y2 = newBounds.gameObject.GetComponent<CameraBounds>().GetYBias();
        Bounds box = newBounds.gameObject.GetComponent<BoxCollider2D>().bounds;
        float xMin = targetBounds.min.x;
        float xMax = targetBounds.max.x;
        float yMin = targetBounds.min.y;
        float yMax = targetBounds.max.y;
        if (x2 > xBias)
        {
            xMin = box.min.x;
            xMax = box.max.x;
        }
        else if (x2 == xBias) {
            if (box.min.x <= targetBounds.min.x) {
                xMin = box.min.x;
            }

            if (box.max.x >= targetBounds.max.x) {
                xMax = box.max.x;
            }
        }
        if (y2 > yBias)
        {
            yMin = box.min.y;
            yMax = box.max.y;
        }
        else if (y2 == yBias)
        {
            if (box.min.y <= targetBounds.min.y)
            {
                yMin = box.min.y;
            }

            if (box.max.y >= targetBounds.max.y)
            {
                yMax = box.max.y;
            }
        }

        targetBounds.min = new Vector3(xMin, yMin, -10);
        targetBounds.max = new Vector3(xMax, yMax, -10);
    }

    bool withinBounds(GameObject boundary) {
        Bounds box = boundary.gameObject.GetComponent<BoxCollider2D>().bounds;
        Vector3 oldLocalPos = Target.localPosition;
        Target.localPosition = new Vector3(Target.localPosition.x, 1.0f, 0.0f);
        bool success = (Target.position.x > box.min.x && Target.position.x < box.max.x && Target.position.y > box.min.y && Target.position.y < box.max.y);
        Target.localPosition = oldLocalPos;
        return success;
    }
}
*/