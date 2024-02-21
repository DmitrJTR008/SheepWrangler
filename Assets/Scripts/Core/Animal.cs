using UnityEngine;
using System;

public abstract class Animal : MonoBehaviour
{
    public bool isMovable = true;
    public AnimalAnimationController AnimalAnimationController { get; protected set; }
    
    protected Animator AnimalAnimator;

}
