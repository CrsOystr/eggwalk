using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (SphereCollider))]

public class ExplodePickup : MonoBehaviour, Pickup {

    [SerializeField] private int id;
    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private bool destroy;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private List<PickupModifier> modifiers;
    [SerializeField] private List<GameObject> fragmentRigidBodies;
    [SerializeField] private int scoreValue;

    private float amplitude = 0.1f;
    private float frequency = 0.5f;
    private float initalPhase = Mathf.PI / 2.0f;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<BoxCollider>().enabled = destroy;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = !destroy;
        }
    }

    void FixedUpdate()
    {
        float roll = amplitude * Mathf.Sin(2.0f * Mathf.PI * frequency * Time.time + initalPhase);
        this.transform.Rotate(Vector3.forward, roll, Space.World);
        //this.transform.Translate(BobbingVector);
    }

    public int getId()
    {
        return this.id;
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

    public void onRotateAction(float rotation)
    {
        float speedup = frequency * Mathf.Abs(rotation) / 2 < 0.5f ? Mathf.Abs(rotation) / 2 : 0.5f;
        this.frequency = speedup;
    }

    public void onPickupAction()
    {
    }

    public void onDropAction()
    {
        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<BoxCollider>().enabled = true;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public int getScoreValue()
    {
        return this.scoreValue;
    }

    public List<PickupModifier> getModifiers()
    {
        return this.modifiers;
    }
}
