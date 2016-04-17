using UnityEngine;
using System.Collections.Generic;

public class ScatterMeteor : MonoBehaviour
{                               
    [SerializeField] private float baseRate;
    [SerializeField] private float multiplier;
    [SerializeField] private GameObject meteor;
    [SerializeField] private List<MeshRenderer> area;
    [SerializeField] private Transform origin;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("launchMeteor", 5.0f, 8.0f);

    }

    public void launchMeteor()
    {
        int r = Random.Range(0, area.Count - 1);
        MeshRenderer localArea = area[r];

        float x = Random.Range(localArea.bounds.min.x, localArea.bounds.max.x);
        float y = Random.Range(localArea.bounds.min.y, localArea.bounds.max.y);
        float z = Random.Range(localArea.bounds.min.z, localArea.bounds.max.z);
        GameObject m = Instantiate(meteor, origin.position, Quaternion.identity) as GameObject;
        m.GetComponent<Meteor>().Target = new Vector3(x, y, z);
        m.GetComponent<Meteor>().Launch();
        Destroy(m, 20.0f);
    }

    public static float comp(Vector3 vec)
    {
        return 0.0f;
    }
}
