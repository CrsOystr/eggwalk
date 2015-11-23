using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
[RequireComponent (typeof (Rigidbody))]

public class BasicPickup : MonoBehaviour, Pickup {

    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private Transform centerOfMass;
	public float amplitude;
	public float rate;

    void Start()
    {
        this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
    }

	void FixedUpdate() 
	{
		Vector3 BobbingVector = Vector3.up * amplitude * rate * Mathf.Sin (rate * Time.time) * Time.deltaTime;
		this.transform.parent.Translate (BobbingVector, Space.Self);
	}

	void OnTriggerEnter(Collider col)
	{

	}

    public string getName()
    {
        return this.pickupName;
    }

    public int getMass()
    {
        return this.mass;
    }

    public Transform getCenterOfMass()
    {
        return this.centerOfMass;
    }

    public GameObject getTargetItem()
    {
        return this.gameObject;
    }

    public void pickupAction()
    {
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
    }
}
