using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SmoothAlphaObjects : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
    [SerializeField] private float timeOnSwamp = 5f;
    [Range(0.1f,1f)] [SerializeField] private float waitingTimeDuration = 0.8f;
    [SerializeField] private float duration = 2f;
    private int currentIndexCanvasOn = 0;
    
    private void Start() {
        if (canvasGroups.Count > 0) currentIndexCanvasOn = 0;
        else this.enabled = false;

        foreach (var canvasGroup in canvasGroups) 
            canvasGroup.alpha = 0;
    }

    private void OnEnable() {
        StartCoroutine(SwampAlphaImage());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator SwampAlphaImage() {
        yield return new WaitForSeconds(1f);
        while (true) {
            yield return canvasGroups[currentIndexCanvasOn].DOFade(1,duration);
            yield return new WaitForSeconds(duration * waitingTimeDuration);
            yield return new WaitForSeconds(timeOnSwamp);
            yield return canvasGroups[currentIndexCanvasOn].DOFade(0,duration);
            yield return new WaitForSeconds(duration * waitingTimeDuration);
            SetNextIndex();
        }
    }

    private void SetNextIndex() {
        var nextIndex = currentIndexCanvasOn + 1;
        if (nextIndex <= canvasGroups.Count - 1) {
            currentIndexCanvasOn++;
            return;
        }
        currentIndexCanvasOn = 0;
    }
}
