using UnityEngine;
using System.Collections;

public class DrawCubeOutline : MonoBehaviour {

    public BoxCollider boxCollider;

    void OnDrawGizmos()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }
}
