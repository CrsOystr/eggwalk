using UnityEngine;
using System.Collections;

public interface Pickup {
    string getName();
    int getMass();
    Transform getCenterOfMass();
    GameObject getTargetItem();
}
