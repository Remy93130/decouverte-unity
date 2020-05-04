using System.Collections;
using UnityEngine;
using Tools;

public class TestCoroutine : MonoBehaviour
{
    void Start()
    {
        // StartCoroutine(WaitCoroutine(2));
        // StartCoroutine(UpdateCoroutine());
        // StartCoroutine(AnimationTools.MultiTranslateCoroutine(this, 20));
    }

    IEnumerator WaitCoroutine(float duration)
    {
        MDebug.Log("ENTER WaitCoroutine");
        yield return new WaitForSeconds(duration);
        MDebug.Log("EXIT WaitCoroutine");
    }

    IEnumerator UpdateCoroutine()
    {
        MDebug.Log("ENTER UpdateCoroutine");
        while (true)
        {
            MDebug.Log("UPDATE UpdateCoroutine");
            yield return null; // On attend la prochaine frame
        }
    }

    /// <summary>
    /// Methode deprecie pour faire du debug
    /// Permet d afficher des bouttons a l ecran
    /// </summary>
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "RESIZE"))
        {
            StartCoroutine(AnimationTools.ResizeCoroutine(transform, transform.localScale * 2, 2f));
        }
    }
}
