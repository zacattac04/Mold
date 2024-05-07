using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundType : MonoBehaviour
{

    
    public enum Type {Stone, Wood, Grass, Metal}
    [SerializeField]
    public Type type;
}
