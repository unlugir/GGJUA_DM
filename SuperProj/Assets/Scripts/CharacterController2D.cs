using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] Transform avatar;
    [SerializeField] float moveSpeed;
    public bool stayGrounded = true;
    [SerializeField] LayerMask groundLayer;
    Animator animator;
    Selector selector;
    float colliderHeight;
    private bool isFlipped = false;
    private Vector3 flippedScale;
    private Vector3 originalScale;

    public bool IsMoving { get 
        {
            if (movementCoroutine == null) return false;
            return true;
        } }
    Coroutine movementCoroutine;
    [SerializeField] bool isPlayer = true;

    private void Start()
    {
        if (isPlayer) 
        {
            selector = GetComponent<Selector>();
            selector.tooFarEvent.AddListener(() => MoveTo(selector.CurrentUsable));
            selector.onClickedUsable.AddListener((usable) => Flip(usable.transform.position));
            selector.onUsedUsable.AddListener((usable) => StopMove());

        }
        animator = GetComponent<Animator>();

        colliderHeight = GetComponent<CapsuleCollider2D>().size.y;
        originalScale = avatar.localScale;
        flippedScale = originalScale;
        flippedScale.x *= -1;
        var groudedPosition = transform.position;
        groudedPosition.y = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y
               + colliderHeight / 2;
        transform.position = groudedPosition;
    }

    public void MoveTo(Vector3 point, System.Action onReach = null)
    {
        if (!this.enabled) return;
        if (movementCoroutine != null) StopMove();
        movementCoroutine = StartCoroutine(StartMove(point, onReach));
    }
    public void MoveTo(Vector3 point, float maxDistance)
    {
        if (!this.enabled) return;

        if (movementCoroutine != null) StopMove();
        
        movementCoroutine = StartCoroutine(StartMove(point, maxDistance));
    }

    public void MoveTo(Usable usable) 
    {
        if (!this.enabled) return;
        if (movementCoroutine != null) StopMove();
        movementCoroutine = StartCoroutine(StartMove(usable));
    }
    public void StopMove() 
    {
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        animator.SetBool("IsWalking", false);
    }

    IEnumerator StartMove(Usable target) 
    {
        var spos = transform.position;
        float time = Vector3.Distance(spos, target.transform.position) / moveSpeed;
        float timer = 0;
        Flip(target.transform.position);
        animator.SetBool("IsWalking", true);
        while (Vector3.Distance(transform.position, target.transform.position) > target.maxUseDistance)
        {
            timer += Time.deltaTime;
            var targetPosition = Vector3.Lerp(spos, target.transform.position, timer / time);
            
            targetPosition.y = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y
                + colliderHeight / 2;
            transform.position = targetPosition;
            yield return null;
        }
        animator.SetBool("IsWalking", false);
        target.OnUseUsable();
        target.gameObject.BroadcastMessage("OnUse", this.transform, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator StartMove(Vector3 point, System.Action onReach)
    {
        var spos = transform.position;
        float time = Vector3.Distance(spos, point) / moveSpeed;
        float timer = 0;
        Flip(point);
        animator.SetBool("IsWalking", true);
        while (timer < time) 
        {
            timer += Time.deltaTime;
            var targetPosition = Vector3.Lerp(spos, point, timer / time);
            targetPosition.y = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y 
                + colliderHeight / 2;
            transform.position = targetPosition;
            yield return null;
        }
        animator.SetBool("IsWalking", false);
        onReach?.Invoke();
    }

    IEnumerator StartMove(Vector3 point, float maxDistance)
    {
        var spos = transform.position;
        float time = Vector3.Distance(spos, point) / moveSpeed;
        float timer = 0;
        Flip(point);
     
        while (timer < time)
        {
            timer += Time.deltaTime;
            var targetPosition = Vector3.Lerp(spos, point, timer / time);
            targetPosition.y = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y
                + colliderHeight / 2;
            transform.position = targetPosition;
            if (Vector3.Distance(targetPosition,transform.position) <= maxDistance)
            {
                timer = time;
                
                break;
            }
            yield return null;
        }

    }

    private void Flip(Vector3 point)
    {
        isFlipped = transform.position.x - point.x > 0;
        avatar.localScale = isFlipped? flippedScale : originalScale; 
    }

}
