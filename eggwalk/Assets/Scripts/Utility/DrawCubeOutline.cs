using UnityEngine;
using System.Collections;

public class DrawCubeOutline : MonoBehaviour {

    public Color BoxOutlineColor;

    void OnDrawGizmos()
    {
        Gizmos.color = BoxOutlineColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
