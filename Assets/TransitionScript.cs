using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TransitionScript : MonoBehaviour
{
    [SerializeField] ManagerScript manager;

    public void AnimationEnd()
    {
        manager.LoadLevel();
    }

}
