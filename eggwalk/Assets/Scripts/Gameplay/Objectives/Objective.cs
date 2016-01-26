using UnityEngine;
using System.Collections;

namespace GameEnumerations
{
    public enum ObjectiveType
    {
        Objective_Return,
        Objective_Search_Return,
        Objective_Collect
    }
}

/**
* Basis for objectives in the Game
* Examples:
* Collect and Return an item 
*/
public interface Objective {
    string getObjectiveName();
    int getObjectiveID();
    Transform getObjectiveDestination();
    GameEnumerations.ObjectiveType getObjectiveType();
    GameObject getObjectiveItem();
    void initializeObjective();
    void startObjective();
    void completeObjective();
    bool hasStartedObjective();
    bool hasCompletedObjective();
}
