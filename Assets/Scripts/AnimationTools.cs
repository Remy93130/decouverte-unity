using System.Collections;
using UnityEngine;

namespace Tools
{
    public static class AnimationTools
    {

        public static IEnumerator ResizeCoroutine(MonoBehaviour component, Vector3 targetScale, float duration)
        {
            float elapsedTime = 0;
            Vector3 startScale = component.transform.localScale;
            while (elapsedTime < duration)
            {
                float k = elapsedTime / duration; // [0, 1]
                component.transform.localScale = Vector3.Lerp(startScale, targetScale, k);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            component.transform.localScale = targetScale;
        }

        public static IEnumerator TranslateCoroutine(MonoBehaviour component, Vector3 targetPosition, float duration, EasingFunctionDelegate easingFunction)
        {
            float elapsedTime = 0;
            Vector3 startPosition = component.transform.position;
            while (elapsedTime < duration)
            {
                float k = elapsedTime / duration; // [0, 1]
                k = easingFunction != null ? easingFunction(k) : k;
                component.transform.position = Vector3.Lerp(startPosition, targetPosition, k);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            component.transform.localScale = targetPosition;
        }

        public static IEnumerator MultiTranslateCoroutine(MonoBehaviour component, int nTranslation, EasingFunctionDelegate easingFunction)
        {
            for (int i = 0; i < nTranslation; i++)
            {
                yield return component.StartCoroutine(
                    AnimationTools.TranslateCoroutine(
                        component,
                        component.transform.position + Random.insideUnitSphere * 4, .5f,
                        easingFunction
                    )
                );
            }
        }
    }
}
