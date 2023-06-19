using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Range(0, 1)] public float BlendValue;
    float blendSpeed = .3f;

    public GameObject RightArmTarget;
    public GameObject LeftArmTarget;

    public float RightArmWeigth;
    public float LeftArmWeigth;

    void AnimationRun()
    {
        if(Input.GetKey(KeyCode.A))
        {
            BlendValue = Mathf.Clamp01(BlendValue + blendSpeed * Time.deltaTime);
        }
        else
        {
            BlendValue = Mathf.Clamp01(BlendValue - blendSpeed * Time.deltaTime);
        }

        animator.SetFloat("Blend", BlendValue);

        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Uppercut");
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftArmWeigth);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftArmWeigth);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftArmTarget.transform.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftArmTarget.transform.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, RightArmWeigth);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RightArmWeigth);
        animator.SetIKPosition(AvatarIKGoal.RightHand, RightArmTarget.transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, RightArmTarget.transform.rotation);
    }
    private void Update()
    {
        AnimationRun();
    }
}
