using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadingDecal : MonoBehaviour
{
    [SerializeField] bool isVisible;
    Color initialColor;
    [SerializeField] SpriteRenderer sprite;
    private void Start()
    {
        if (sprite == null) sprite = this.GetComponent<SpriteRenderer>();
        initialColor = sprite.color;
        if (!isVisible)
        {
            sprite.color = Color.clear;
            gameObject.SetActive(false);
        }

    }
    public void FadeIn()
    {
        if (isVisible) return;
        isVisible = true;
        sprite.DOColor(initialColor, 1f).onComplete += () => gameObject.SetActive(true);
    }
    public void FadeOut()
    {
        if (!isVisible) return;
        isVisible = false;
        sprite.DOColor(Color.clear, 1f).onComplete += () => gameObject.SetActive(false);
    }
}
