using UnityEngine;
using System.Collections;

/**
 * Basis for objectives in the Game
 * Examples:
 * Collect and Return an item 
 */
public interface Objective {
    string getObjectiveName();
    int getObjectiveID();
    void startObjective();
    void completeObjective();
    bool hasStartedObjective();
    bool hasCompletedObjective();
}
