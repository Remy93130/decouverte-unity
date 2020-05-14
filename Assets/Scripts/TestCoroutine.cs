using System.Collections;
using UnityEngine;
using Tools;
using System;
using UnityEngine.Networking;

public class TestCoroutine : MonoBehaviour
{
    [SerializeField]
    Easings.Functions m_EasingFunction;

    IEnumerator m_TranslateCoroutine;

    bool m_MustStopUpdateCoroutine = false;

    void Start()
    {
        // StartCoroutine(WaitCoroutine(2));
        // StartCoroutine(UpdateCoroutine());
        // StartCoroutine(AnimationTools.MultiTranslateCoroutine(this, 5, Easings.EasingFunctionDelegate(m_EasingFunction)));
        StartCoroutine(InOutActionCoroutine(
            () => { MDebug.Log("in");  },
            () => { MDebug.Log("out"); },
            AnimationTools.MultiTranslateCoroutine(this, 5, Easings.EasingFunctionDelegate(m_EasingFunction))
        ));
    }

    IEnumerator WaitCoroutine(float duration)
    {
        MDebug.Log("ENTER WaitCoroutine");
        yield return new WaitForSeconds(duration);
        MDebug.Log("EXIT WaitCoroutine");
    }

    IEnumerator UpdateCoroutine()
    {
        if (m_MustStopUpdateCoroutine)
        {
            yield break;
        }
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
        GUILayout.BeginArea(new Rect(0, 0, Screen.width * .5f, Screen.height * .5f));
        if (GUILayout.Button("RESIZE"))
        {
            StartCoroutine(AnimationTools.ResizeCoroutine(transform, transform.localScale * 2, 5f));
        }
        if (GUILayout.Button("START TRANSLATE"))
        {
            m_TranslateCoroutine = AnimationTools.MultiTranslateCoroutine(this, 40, Easings.EasingFunctionDelegate(m_EasingFunction));
            StartCoroutine(m_TranslateCoroutine);
        }
        if (GUILayout.Button("STOP TRANSLATE") && m_TranslateCoroutine != null)
        {
            StopCoroutine(m_TranslateCoroutine);
            m_TranslateCoroutine = null;
        }
        if (GUILayout.Button("STOP Update"))
        {
            m_MustStopUpdateCoroutine = true;
        }
        GUILayout.EndArea();
    }

    public IEnumerator InOutActionCoroutine(Action inAction, Action outAction, IEnumerator coroutine)
    {
        inAction?.Invoke();
        yield return StartCoroutine(coroutine);
        outAction?.Invoke();
    }

    public IEnumerator GetFileFromServer(string url)
    {
        UnityWebRequest request = new UnityWebRequest(url);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();
        yield return async;
        if (string.IsNullOrEmpty(request.error))
        {
            string text = request.downloadHandler.text;
        }
    }
}
