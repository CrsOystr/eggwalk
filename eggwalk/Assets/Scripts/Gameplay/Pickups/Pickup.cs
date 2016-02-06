using UnityEngine;
using System.Collections.Generic;

namespace GameEnumerations
{
    public enum PlayerModifier
    {
        Player_Mod_Speed,
        Player_Mod_Balance,
        Gameplay_Mod_Timed
    }
}

public interface Pickup {
    int getId();
    string getName();
    int getMass();
    Transform getCenterOfMass();
    GameObject getTargetItem();
    void onRotateAction(float rotation);
    void onPickupAction(Transform target);
    void onDropAction();
    int getScoreValue();
    List<PickupModifier> getModifiers();
}

[System.Serializable]
public struct PickupModifier
{
    public GameEnumerations.PlayerModifier modifier;
    public float value;
}