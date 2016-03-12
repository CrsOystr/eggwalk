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
    [SerializeField] private Material mat;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private bool incAnim;

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
        if (incAnim && glidingDown)
        {
            dampening += Time.deltaTime;
            this.transform.position =
                Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime / dampening);
            return;
        }
    }

    public int getId()
    {
        return this.id;
    }

    public void setName(string name)
    {
        this.pickupName = name;
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

    public Material getEggMaterial()
    {
        return this.mat;
    }

    public void onRotateAction(float rotation)
    {
        this.speedup = Mathf.Abs(rotation) / 90.0f;
    }

    public void onPickupAction(Transform target)
    {
        glidingDown = true;
        this.target = target;
    }

    public void onHurtAction()
    {
        this.crackLevel += 1;
        this.gameObject.GetComponent<Renderer>().material.SetInt("_CrackingLevel", crackLevel);
    }

    public void onReturnAction()
    {
        this.crackLevel = 0;
    }

    public void onDropAction()
    {
        this.hasExploded = true;

        particles.Play();

        if (this.GetComponent<MeshRenderer>() != null)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        } else if (this.GetComponent<SkinnedMeshRenderer>() != null)
        {
            this.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

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
}
