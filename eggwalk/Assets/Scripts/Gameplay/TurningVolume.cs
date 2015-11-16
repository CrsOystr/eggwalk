using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class TurningVolume : MonoBehaviour {

	public BoxCollider boxCollider;

    public bool northLaneOpen;
    public bool eastLaneOpen;
    public bool southLaneOpen;
    public bool westLaneOpen;

    private bool isPlayerTurning;

	// Use this for initialization
	void Start () {
		boxCollider = this.gameObject.GetComponent<BoxCollider> ();
	}

    /**
     * canTurnRight - Determine based on the open lanes, if the player is allowed to turn right
     * On Input:
     *     localForwardVector (Vector3) - The local forward vector of the player
     * On Output:
     *     If the player is allowed to turn right
     * Call:
     *     PlayerMotor player = ...;
     *     Vector3 localForwardVector = player.transform.forward;
     *     TurningVolume vol = ...;
     *     if (vol.canTurnRight(localForwardVector)) {
     *       ...
     *     }
     */
    public bool canTurnRight(Vector3 localForwardVector)
    {
        float flfwDot = Vector3.Dot(localForwardVector, Vector3.forward);
        float flrwDot = Vector3.Dot(localForwardVector, Vector3.right);
        float epsilon = 0.0001f;

        Debug.Log(flfwDot + ", " + flrwDot);

        if (southLaneOpen && NearlyEqual(flfwDot, 0.0f, epsilon) && NearlyEqual(flrwDot, 1.0f, epsilon))
        {
            return true;
        }

        if (westLaneOpen && NearlyEqual(flfwDot, -1.0f, epsilon) && NearlyEqual(flrwDot, 0.0f, epsilon))
        {
            return true;
        }

        if (northLaneOpen && NearlyEqual(flfwDot, 0.0f, epsilon) && NearlyEqual(flrwDot, -1.0f, epsilon))
        {
            return true;
        }

        if (eastLaneOpen && NearlyEqual(flfwDot, 1.0f, epsilon) && NearlyEqual(flrwDot, 0.0f, epsilon))
        {
            return true;
        }

        return false;
    }

    /**
     * canTurnLeft - Determine based on the open lanes, if the player is allowed to turn right
     * On Input:
     *     localForwardVector (Vector3) - The local forward vector of the player
     * On Output:
     *     If the player is allowed to turn left
     * Call:
     *     PlayerMotor player = ...;
     *     Vector3 localForwardVector = player.transform.forward;
     *     TurningVolume vol = ...;
     *     if (vol.canTurnLeft(localForwardVector)) {
     *       ...
     *     }
     */
    public bool canTurnLeft(Vector3 localForwardVector)
    {
        float flfwDot = Vector3.Dot(localForwardVector, Vector3.forward);
        float flrwDot = Vector3.Dot(localForwardVector, Vector3.right);
        float epsilon = 0.0001f;

        Debug.Log(flfwDot + ", " + flrwDot);

        if (northLaneOpen && NearlyEqual(flfwDot, 0.0f, epsilon) && NearlyEqual(flrwDot, 1.0f, epsilon))
        {
            return true;
        }

        if (eastLaneOpen && NearlyEqual(flfwDot, -1.0f, epsilon) && NearlyEqual(flrwDot, 0.0f, epsilon))
        {
            return true;
        }

        if (southLaneOpen && NearlyEqual(flfwDot, 0.0f, epsilon) && NearlyEqual(flrwDot, -1.0f, epsilon))
        {
            return true;
        }

        if (westLaneOpen && NearlyEqual(flfwDot, 1.0f, epsilon) && NearlyEqual(flrwDot, 0.0f, epsilon))
        {
            return true;
        }

        return false;
    }

    public bool NearlyEqual(float a, float b, float epsilon)
    {
        double absA = Mathf.Abs(a);
        double absB = Mathf.Abs(b);
        double diff = Mathf.Abs(a - b);

        if (a == b)
        {
            return true;
        } else if (a == 0 || b == 0 || diff < double.Epsilon)
        {
            return diff < epsilon;
        }
        else
        {
            return diff / (absA + absB) < epsilon;
        }

    }

    public bool IsPlayerTurning
    {
        get { return isPlayerTurning; }
        set { isPlayerTurning = value; }
    }
}