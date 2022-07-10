using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerNpc : MonoBehaviour
{
    [SerializeField] Transform objToFollow;
    [SerializeField] Transform outOfViewPoint;
    [SerializeField] float maxDistance;
    CharacterController2D characterController;
    bool isFollowing = true;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (!isFollowing) return;
        if (characterController.IsMoving) return;
        if (Vector3.Distance(objToFollow.position, transform.position) > maxDistance)
        {
            animator.SetBool("IsWalking", true);
            characterController.MoveTo(objToFollow.position, 1f);
        }
        else 
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void GoFromScene() 
    {
        isFollowing = false;
        characterController.MoveTo(outOfViewPoint.position, 0.1f);
    }

    public void ComeToScene() 
    {
        characterController.MoveTo(objToFollow.position, 0.1f);
    }
}
