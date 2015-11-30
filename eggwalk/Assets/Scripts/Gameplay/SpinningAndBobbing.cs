using UnityEngine;
using System.Collections;

public class SpinningAndBobbing : MonoBehaviour {

    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float phase;
    [SerializeField] private float spinningSpeed;

	void FixedUpdate ()
    {
        Vector3 verticalBobbing = Vector3.up * amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time + phase);
        this.transform.Translate(verticalBobbing, Space.World);
        this.transform.Rotate(Vector3.up, spinningSpeed * Time.deltaTime, Space.World);
	}
}
