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
    void setName(string name);
    int getMass();
    Transform getCenterOfMass();
    GameObject getTargetItem();
    Material getEggMaterial();
    void onRotateAction(float rotation);
    void onPickupAction(Transform target);
    void onHurtAction();
    void onDropAction();
    void onReturnAction();
    int getScoreValue();
    List<PickupModifier> getModifiers();
}

[System.Serializable]
public struct PickupModifier
{
    public GameEnumerations.PlayerModifier modifier;
    public float value;
}