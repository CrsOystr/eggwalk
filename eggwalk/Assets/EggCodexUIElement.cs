using UnityEngine;
using UnityEngine.UI;

public class EggCodexUIElement : MonoBehaviour {

    public Image eggSprite;
    public Text eggName;
    public GameObject eggObject;
    public float eggRotateSpeed;

    void Update()
    {
        // rotate egg, make it pretty
        if(eggObject != null)
        {
            eggObject.transform.Rotate(Vector3.up * eggRotateSpeed * Time.deltaTime);
        }
    }

}
