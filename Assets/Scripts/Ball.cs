using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IColorable colorable = collision.gameObject.GetComponent<IColorable>();
        if (colorable != null)
        {
            colorable.ColorizeRandom();
        }
    }
}
