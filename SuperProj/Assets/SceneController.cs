using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] CanvasGroup loadCanvas;
    public void LoadLocation1() 
    {
        loadCanvas.DOFade(1, 2f).onComplete +=() => SceneManager.LoadScene(0);
    }
    public void LoadLocation2()
    {
        loadCanvas.DOFade(1, 2f).onComplete += () => SceneManager.LoadScene(1);
    }
    public void LoadLocation3()
    {
        loadCanvas.DOFade(1, 2f).onComplete += () => SceneManager.LoadScene(2);
    }
}
