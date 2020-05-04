using System.Collections;
using UnityEngine;

namespace Tools
{
    public static class AnimationTools
    {
        public static IEnumerator ResizeCoroutine(Transform objTransform, Vector3 targetScale, float duration)
        {
            float elapsedTime = 0;
            Vector3 startScale = objTransform.localScale;
            while (elapsedTime < duration)
            {
                float k = elapsedTime / duration; // [0, 1]
                objTransform.localScale = Vector3.Lerp(startScale, targetScale, k);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            objTransform.localScale = targetScale;
        }

        public static IEnumerator TranslateCoroutine(Transform objTransform, Vector3 targetPosition, float duration)
        {
            float elapsedTime = 0;
            Vector3 startPosition = objTransform.position;
            while (elapsedTime < duration)
            {
                float k = elapsedTime / duration; // [0, 1]
                objTransform.position = Vector3.Lerp(startPosition, targetPosition, k);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            objTransform.localScale = targetPosition;
        }

        public static IEnumerator MultiTranslateCoroutine(MonoBehaviour component, int nTranslation)
        {
            for (int i = 0; i < nTranslation; i++)
            {
                yield return component.StartCoroutine(
                    AnimationTools.TranslateCoroutine(
                        component.transform,
                        component.transform.position + Random.insideUnitSphere * 4, .5f
                    )
                );
            }
        }
    }
}
