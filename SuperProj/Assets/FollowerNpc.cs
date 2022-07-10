﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerNpc : MonoBehaviour
{
    [SerializeField] Transform objToFollow;
    [SerializeField] Transform outOfViewPoint;
    [SerializeField] float maxDistance;
    CharacterController2D characterController;
    bool isFollowing = true;
    private void Start()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (!isFollowing) return;
        if (Vector3.Distance(objToFollow.position, transform.position) > maxDistance ) 
        {
            characterController.MoveTo(objToFollow.position, 1f);
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
