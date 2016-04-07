using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

    public float speed;
    private float currentTurn;
    private bool turnRight;
    private bool isTurning;
	
	// Update is called once per frame
	void Update ()
    {
        bool turnRightInput = Input.GetButtonDown("TurnRight");
        bool turnLeftInput = Input.GetButtonDown("TurnLeft");

        if (turnRightInput)
        {
            turnRight = true;
            isTurning = true;

            if (Mathf.Abs(currentTurn) > 0.0f)
            {
                currentTurn = 90.0f - Mathf.Abs(currentTurn);
            }
        }

        if (turnLeftInput)
        {
            turnRight = false;
            isTurning = true;

            if (Mathf.Abs(currentTurn) > 0.0f)
            {
                currentTurn = 90.0f - Mathf.Abs(currentTurn);
            }
        }

        if (isTurning)
        {
            float direction = (turnRight) ? 1.0f : -1.0f;
            Turn(direction * speed);
        }
    }

    public void Turn(float rotationSpeed)
    {
        float deltaTurn = rotationSpeed * Time.deltaTime;

        if (Mathf.Abs(currentTurn) + Mathf.Abs(deltaTurn) < 90.0f)
        {
            currentTurn += Mathf.Abs(deltaTurn);
            print(currentTurn);
            this.transform.Rotate(this.transform.up, deltaTurn);
        } else
        {
            float compRight = 90.0f - (this.transform.eulerAngles.y % 90);
            float compLeft = this.transform.eulerAngles.y % 90;

            print("Comp Right: " + compRight + "Comp Left: " + compLeft + " Sign: " + Mathf.Sign(deltaTurn));
            this.transform.Rotate(this.transform.up, Mathf.Sign(deltaTurn) * Mathf.Min(compRight, compLeft));
            isTurning = false;
            currentTurn = 0.0f;
        }
    }
}
