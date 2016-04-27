using UnityEngine;
using System.Collections.Generic;

public class Turning : MonoBehaviour {

    public float speed;
    private float currentTurn;
    private bool turnRight;
    private bool isTurning;
    private bool lastInputWasRight;
    private bool lastInputWasLeft;
    private bool reverse;
    private float trajectory = 90.0f;
    private Stack<int> stack = new Stack<int>();

    // Update is called once per frame
    void Update ()
    {
        bool turnRightInput = Input.GetButtonDown("TurnRight");
        bool turnLeftInput = Input.GetButtonDown("TurnLeft");

		if (turnRightInput && Time.timeScale == 1.0f)
        {
            turnRight = true;
            isTurning = true;
            lastInputWasRight = true;

            if (stack.Count == 0)
            {
                stack.Push(2);
            }

            if (stack.Peek() != 2 && Mathf.Abs(currentTurn) > 0.0f)
            {
                reverse = true;
            }
        }

		if (turnLeftInput && Time.timeScale == 1.0f)
        {
            turnRight = false;
            isTurning = true;
            lastInputWasLeft = true;

            if (stack.Count == 0)
            {
                stack.Push(1);
            }

            if (stack.Peek() != 1 && Mathf.Abs(currentTurn) > 0.0f)
            {
                reverse = true;
            }
        }

        if (reverse)
        {
            currentTurn = 90.0f - Mathf.Abs(currentTurn);
            reverse = false;
        }
			
        lastInputWasRight = false;
        lastInputWasLeft = false;

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
            this.transform.Rotate(this.transform.up, deltaTurn);
        } else
        {
            float compRight = 90.0f - (this.transform.eulerAngles.y % 90);
            float compLeft = this.transform.eulerAngles.y % 90;

            this.transform.Rotate(this.transform.up, Mathf.Sign(deltaTurn) * Mathf.Min(compRight, compLeft));
            isTurning = false;
            currentTurn = 0.0f;
            stack = new Stack<int>();
        }
    }
}
