using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof (Rigidbody))]

public class BasicPickup : MonoBehaviour, Pickup {

    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private Transform centerOfMass;
	public float amplitude;
	public float rate;
    private bool canBob = true;

    void Start()
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
    }

	void FixedUpdate() 
	{
        if (canBob)
        {
            Vector3 BobbingVector = Vector3.up * amplitude * rate * Mathf.Sin(rate * Time.time) * Time.deltaTime;
            this.transform.parent.Translate(BobbingVector, Space.Self);
        }
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
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
        this.canBob = false;
    }
}
