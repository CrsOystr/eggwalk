using UnityEngine;
using System.Collections;

public class HandAnimEvent : MonoBehaviour
{
    [SerializeField] GameObject egg;
    [SerializeField] ParticleSystem particles;
    private Material materialToAssign;

    public void setMaterialToAssign(Material mat)
    {
        this.materialToAssign = mat;
    }

    public void OnEggRiseEnter()
    {
        if (this.particles != null)
        {
            this.particles.Play();
        }

    }

    public void OnEggRiseExit()
    {
        //CurrentEggMaterial = materialToAssign;
    }

    public void OnEggDropEnter()
    {
        if (this.particles != null)
        {
            this.particles.Play();
        }

        CurrentEggMaterial = materialToAssign;

        this.GetComponent<Animator>().SetBool("EggIsFalling", true);
    }

    public void OnEggDropExit()
    {
        if (this.particles != null)
        {
            this.particles.Stop();
        }

        this.GetComponent<Animator>().SetBool("EggIsFalling", false);
    }

    Material CurrentEggMaterial
    {
        get { return this.egg.GetComponent<Renderer>().material; }
        set{ this.egg.GetComponent<Renderer>().material = value; }
    }
}
