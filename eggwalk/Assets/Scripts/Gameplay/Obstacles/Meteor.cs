using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private ParticleSystem impactExplosion;
    [SerializeField] private float speed;
    [SerializeField] private GameObject impactMesh;
	[SerializeField] private AudioSource ApproachingSound;
	[SerializeField] private AudioSource ImpactSound;

    private float lastStep;
    private bool hasLaunched = false;
    private bool hasExploded;
	
    void Start()
    {
        hasExploded = false;

        if (target == null)
        {
            print("Assign Meteor GameObject target: " + gameObject.name);
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

        float step = speed * Time.deltaTime;
        if (Vector3.MoveTowards(transform.position, target.position, step) - target.position == Vector3.zero)
        {
            Explode();
        }

        if (target != null)
        {
            this.transform.position =
                Vector3.MoveTowards(transform.position, target.position, step);
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

    public bool HasExploded
    {
        get { return this.hasExploded; }
    }

    public bool HasLaunched
    {
        get { return this.hasLaunched; }
    }
}
