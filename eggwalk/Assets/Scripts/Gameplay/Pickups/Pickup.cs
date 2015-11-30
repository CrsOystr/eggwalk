using UnityEngine;
using System.Collections;

public interface Pickup {
    int getId();
    string getName();
    int getMass();
    Transform getCenterOfMass();
    GameObject getTargetItem();
    void onPickupAction();
    void onDropAction();
}
