using UnityEngine;
using System.Collections;
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
    [SerializeField] private GameObject explosionEffect;

    private float amplitude = 0.5f;
    private float frequency = 1.0f;
    private float speedup = 0.0f;
    private float initalPhase = Mathf.PI / 2.0f;
    private float noiseInfluence = 0.01f;
    private bool hasExploded = false;
    private bool glidingDown = false;
    private Transform target;
    private float speed = 3.5f;
    private float dampening;

	// Use this for initialization
	void Start () {
        if (this.explosionEffect != null)
        {
            explosionEffect.SetActive(false);
        }

        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<Collider>().enabled = destroy;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = !destroy;
        }

        this.gameObject.GetComponent<Renderer>().material.SetInt("_CrackingLevel", 3);
    }

    void FixedUpdate()
    {
        if (glidingDown)
        {
            dampening += Time.deltaTime;
            this.transform.position = 
                Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime / dampening);
            return;
        }

        if (!hasExploded)
        {
            float noise = 2 * noiseInfluence * Random.Range(0.0f, 1.0f) - noiseInfluence;
            float roll = amplitude * (1) * Mathf.Sin(1.0f * Mathf.PI * frequency * Time.time + initalPhase);
            this.transform.Rotate(Vector3.forward, roll + noise);
            this.transform.Translate(Vector3.up * 0.05f * Mathf.Sin(2.0f * Mathf.PI * 1.1f * Time.time + Mathf.PI / 2.0f));
        }
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
        this.speedup = Mathf.Abs(rotation) / 90.0f;
    }

    public void onPickupAction(Transform target)
    {
        glidingDown = true;
        this.target = target;
        StartCoroutine(dropDown(3.0f));
    }

    public void onDropAction()
    {
        this.hasExploded = true;

        if (this.explosionEffect != null)
        {
            explosionEffect.SetActive(true);
        }

        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<Collider>().enabled = true;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = false;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().useGravity = true;
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

    private IEnumerator dropDown(float time)
    {
        yield return new WaitForSeconds(time);
        this.glidingDown = false;
    }
}
