using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using DG.Tweening;

public class TagsController : MonoBehaviour
{
    int[,] tagsData;
    [SerializeField] Texture2D image;
    [SerializeField] List<Button> tagsButtons;
    [SerializeField] UnityEvent onWin;
    [SerializeField] float tweenSpeed = 0.1f;
    [SerializeField] int size = 3; // NOT TESTED ^_^
    [SerializeField] int positionStep = 100;
    [SerializeField] bool isActiveOnStart;
    public UnityEvent onClose = new UnityEvent();

    CanvasGroup group;
    public bool isSolved = false;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        tagsData = new int[size, size];
        int partSize = image.width / size;
        if (image.width != image.width) Debug.LogWarning("Image is not square!!! Be careful");
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int btnId = (i * size) + j + 1;
                tagsData[i, j] = btnId;
                if (j == size - 1 && i == size - 1) continue;
                tagsButtons[(i * size) + j].GetComponent<RawImage>().texture = GetCroppedTexture(image,
                    new RectInt(j * partSize, (size - i - 1) * partSize, partSize, partSize));

                tagsButtons[(i * size) + j].onClick.AddListener(() => TryMove(btnId));
            }
        }
        tagsData[size - 1, size - 1] = 0;
        Mix(300);
        this.gameObject.SetActive(isActiveOnStart);

    }
    bool TryMove(int btnId, bool isMixing = false)
    {
        if (isSolved)
        {
            Hide();
            return false;
        }
        int i;
        int j = 0;
        for (i = 0; i < size; i++)
            for (j = 0; j < size; j++)
                if (tagsData[i, j] == 0) goto boobas0_0;
                boobas0_0:

        int y;
        int x = 0;
        for (y = 0; y < size; y++)
            for (x = 0; x < size; x++)
                if (tagsData[y, x] == btnId) goto boobas_030;
                boobas_030:

        if (!((Mathf.Abs(x - j) == 1 && y == i) ^
            (Mathf.Abs(y - i) == 1 && x == j))) return false;

        RectTransform btn = tagsButtons[btnId - 1].GetComponent<RectTransform>();

        if (isMixing)
            btn.anchoredPosition = new Vector2(j * positionStep + 50, (size - i) * positionStep - 50);
        else
            btn.DOAnchorPos(new Vector2(j * positionStep + 50, (size - i) * positionStep - 50), tweenSpeed);
        tagsData[y, x] = 0;
        tagsData[i, j] = btnId;

        if (!isMixing && CheckWin())
        {
            isSolved = true;
            Hide(1);
            StartCoroutine(DelayedInvoke(onWin.Invoke, 1));
        }
        return true;
    }
    IEnumerator DelayedInvoke(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    public void Mix(int moves)
    {
        while (moves != 0)
        {
            if (TryMove(Random.Range(1, 9), true)) moves--;
        }
        isSolved = false;
    }
    public void Show()
    {
        group.alpha = 0;
        this.gameObject.SetActive(true);
        group.DOFade(1, 0.5f);
    }
    public void Hide(float delay = 0)
    {
        onClose.Invoke();
        var s = DOTween.Sequence();
        s.AppendInterval(delay);
        s.Append(group.DOFade(0, 0.5f));
        s.onComplete += () => this.gameObject.SetActive(false);
    }
    bool CheckWin()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                if (i == size - 1 && j == size - 1) return true;
                if ((i * size) + j + 1 != tagsData[i, j]) return false;
            }
        return false;
    }

    Texture2D GetCroppedTexture(Texture2D text, RectInt rect)
    {
        Color[] c = text.GetPixels(rect.x, rect.y, rect.width, rect.height);
        Texture2D m2Texture = new Texture2D(rect.width, rect.height);

        m2Texture.SetPixels(c);
        m2Texture.Apply();
        return m2Texture;
    }

}
