using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private ParticleSystem impactExplosion;
    [SerializeField] private float speed;
    [SerializeField] private GameObject impactMesh;
	[SerializeField] private AudioSource ApproachingSound;
	[SerializeField] private AudioSource ImpactSound;

    private float lastStep;
    public Vector3 target;
    private bool hasLaunched = false;
    private bool hasExploded;
	
    void Start()
    {
        hasExploded = false;

        if (targetTransform != null)
        {
            target = targetTransform.position;
        }

        if (impactMesh != null)
        {
            this.impactMesh.GetComponent<MeshRenderer>().enabled = false;
        }
    }

	void Update ()
    {
        if (hasExploded || !hasLaunched)
        {
            return;
        }

        if (target != null)
        {
            float step = speed * Time.deltaTime;

            if (Vector3.MoveTowards(transform.position, target, step) - target == Vector3.zero)
            {
                Explode();
            }

            this.transform.position =
                Vector3.MoveTowards(transform.position, target, step);
        }
	}

    public void Launch()
    {
        hasLaunched = true;
		ApproachingSound.Play ();

    } 

    private void Explode()
    {
        hasExploded = true;
        impactExplosion.Play();
		ImpactSound.Play ();

        if (impactMesh != null)
        {
            this.impactMesh.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.GetComponent<VehicleBehavior>() != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }
    }

    public Vector3 Target
    {
        get { return this.target; }
        set { this.target = value; }
    }

    public bool HasExploded
    {
        get { return this.hasExploded; }
    }

    public bool HasLaunched
    {
        get { return this.hasLaunched; }
    }
}
