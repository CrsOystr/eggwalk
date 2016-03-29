using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

    [SerializeField] private bool isEndless;
    [SerializeField] private bool imposeTimeLimit;
    [SerializeField] private bool imposeScoreToClearLevel;
    [SerializeField] private bool resetLivesAfterObjectiveComplete;
    [SerializeField] private bool canTurnAnywhere;
    [SerializeField] private bool canPauseGame = true;
    [SerializeField] private float maximumTimeAllowed;
    [SerializeField] private int initialTimeToStartLevel;
    [SerializeField] private int scoreRequiredToClearLevel;
    [SerializeField] private string levelUnlock;

    public bool IsEndless
    {
        get { return this.isEndless; }
    }

    public bool ImposeTimeLimit
    {
        get { return this.ImposeTimeLimit; }
    }

    public float MaximumTimeAllowed
    {
        get { return this.maximumTimeAllowed; }
    }

    public int InitialTimeToStartLevel
    {
        get { return this.initialTimeToStartLevel; }
    }

    public bool ImposeScoreToClearLevel
    {
        get { return this.imposeScoreToClearLevel; }
    }

    public bool ResetLivesAfterObjectiveComplete
    {
        get { return this.resetLivesAfterObjectiveComplete; }
    }

    public int ScoreRequiredToClearLevel
    {
        get { return this.scoreRequiredToClearLevel; }
    }

    public bool CanPauseGame
    {
        get { return this.canPauseGame; }
    }

    public bool CanTurnAnywhere
    {
        get { return this.canTurnAnywhere; }
    }
}
