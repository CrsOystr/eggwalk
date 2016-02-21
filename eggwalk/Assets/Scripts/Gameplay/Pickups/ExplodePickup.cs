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

    private float amplitude = 0.1f;
    private float frequency = 1.0f;
    private float speedup = 0.0f;
    private float initalPhase = Mathf.PI / 2.0f;
    private float noiseInfluence = 0.001f;
    private bool hasExploded = false;
    private bool glidingDown = false;
    private Transform target;
    private float speed = 2.0f;
    private float dampening;
    private int crackLevel = 0;

	// Use this for initialization
	void Start () {
        if (this.explosionEffect != null)
        {
            explosionEffect.SetActive(false);
        }

        fragmentRigidBodies = new List<GameObject>();
        BoxCollider[] col = GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < col.Length; i++)
        {
            fragmentRigidBodies.Add(col[i].gameObject);
        }

        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<Collider>().enabled = destroy;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = !destroy;
        }

        this.gameObject.GetComponent<Renderer>().material.SetInt("_CrackingLevel", crackLevel);
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
            float roll = amplitude * Mathf.Sin(1.0f * Mathf.PI * frequency * Time.time + initalPhase);
            this.transform.Rotate(Vector3.forward, roll + noise);
            this.transform.Translate(Vector3.up * 0.001f * Mathf.Sin(2.0f * Mathf.PI * 1.1f * Time.time + Mathf.PI / 2.0f));
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

    public void onHurtAction()
    {
        this.crackLevel += 1;
        this.gameObject.GetComponent<Renderer>().material.SetInt("_CrackingLevel", crackLevel);
    }

    public void onDropAction()
    {
        this.hasExploded = true;

        this.GetComponent<MeshRenderer>().enabled = false;

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
