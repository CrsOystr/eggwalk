using UnityEngine;
using System.Collections;

public class DrawSphereOutline : MonoBehaviour {

    public SphereCollider sphereCollider;

    void OnDrawGizmos() {
		sphereCollider = gameObject.GetComponent<SphereCollider> ();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius);
    }
}
