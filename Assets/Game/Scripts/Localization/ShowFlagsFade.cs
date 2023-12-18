using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ShowFlagsFade : MonoBehaviour
{
    [SerializeField] private RectTransform rectMoveTransform;
    [SerializeField] private RectTransform rectHideToTransform;

    [SerializeField] private float moveDuration;
    [SerializeField] private float scaleDuration;
    
    private bool isHidded = true;
    
    private Vector2 moveTo;
    private Vector2 backTo;
    
    private Vector2 scaleTo;
    private Vector2 scaleFrom;

    
    private void Start() {
        moveTo = rectMoveTransform.localPosition;
        backTo = rectHideToTransform.localPosition;
        scaleTo = Vector2.zero;
        scaleFrom = rectMoveTransform.localScale;
        
        FadeFlags();
    }

    public void FadeFlags() {
        isHidded = !isHidded;
        
        StopAllCoroutines();
        StartCoroutine(Fading());
    }

    private IEnumerator Fading() {
        var moveToLocal = isHidded ? moveTo : backTo;
        var scaleToLocal = isHidded ? scaleFrom : scaleTo;
        
        rectMoveTransform.DOLocalMove(moveToLocal,moveDuration);
        rectMoveTransform.DOScale(scaleToLocal, scaleDuration);

        yield return new WaitForSeconds(moveDuration);
        
        rectMoveTransform.gameObject.SetActive(true);
    }

}
