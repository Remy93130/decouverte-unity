using UnityEngine;

public class Colorable : MonoBehaviour
{
    public void ColorizeRandom()
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        if (mr)
        {
            mr.material.color = Random.ColorHSV();
        }
    }
}
