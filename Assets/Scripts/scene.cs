using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene : MonoBehaviour
{
    public float xCoordinateInitial;
    public float yCoordinateInitial;

    public void Start()
    {
        xCoordinateInitial = transform.position.x;
        yCoordinateInitial = transform.position.y;
    }
}
