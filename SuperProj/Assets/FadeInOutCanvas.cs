using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeInOutCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fade() 
    {
        var s = DOTween.Sequence();
        s.Append(GetComponent<CanvasGroup>().DOFade(1, 2f));
        s.AppendInterval(2);
        s.Append(GetComponent<CanvasGroup>().DOFade(0, 2f));
    }
}
