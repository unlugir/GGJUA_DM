using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerNpc : MonoBehaviour
{
    [SerializeField] Transform objToFollow;
    [SerializeField] float maxDistance;
    CharacterController2D characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Vector3.Distance(objToFollow.position, transform.position) > maxDistance ) 
        {
            characterController.MoveTo(objToFollow.position, 1f);
        }
    }

}
