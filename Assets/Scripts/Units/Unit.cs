using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //[Header("Objects")]
    Animator Animator;

    Unit Target;

    [Header("Statistics")]
    [SerializeField] protected short STR;
    [SerializeField] protected short DEX;
    [SerializeField] protected short INT;

    static short MaxArm = 400;
    [SerializeField] protected short END;
    [SerializeField] protected short SPR;

    [SerializeField] protected short SPD;
    [SerializeField] protected short LCK;

    [SerializeField] protected short FHP;
    [SerializeField] protected short HP;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    public IEnumerator Hurt(int Damage)
    {
        return null;
    }
    public virtual IEnumerator TakeAction()
    {
        return null;
    }
}
