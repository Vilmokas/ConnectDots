using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope
{
    public float distance;
    public RectTransform ropeTransform;

    public Rope(float distance, RectTransform ropeTransform)
    {
        this.distance = distance;
        this.ropeTransform = ropeTransform;
    }
}
