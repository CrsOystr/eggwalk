using UnityEngine;
using System.Collections.Generic;

public class IntersectionBehavior : MonoBehaviour {

	public IntersectionBehavior toNorth, toSouth, toEast, toWest; // the intersections adjacent to this intersection

    public Transform NorthEast, NorthWest, SouthEast, SouthWest; // the four points inside the intersection;


}
