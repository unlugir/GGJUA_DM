using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PickItemAnimator : MonoBehaviour
{
    public static PickItemAnimator Instance { get; private set; }
    [SerializeField] RectTransform inventoryView;
    [SerializeField] Image itemImg;
    Color defaultColor;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        itemImg.enabled = false;
        defaultColor = itemImg.color;
    }
    public void Animate(Item item, Vector3 wordPos)
    {
        itemImg.sprite = item.Icon;
        transform.position = Camera.main.WorldToScreenPoint(wordPos);
        itemImg.enabled = true;
        itemImg.color = defaultColor;
        itemImg.DOColor(Color.clear, 1);
        transform.DOMove(inventoryView.position, 1).onComplete += () => itemImg.enabled = false;
    }
}
