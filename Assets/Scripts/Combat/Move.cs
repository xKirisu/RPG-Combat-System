using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StatType
{
    STR, DEX, INT
}
enum DamageType
{
    Phisical, Magical
}
public class Move : MonoBehaviour
{
    [SerializeField] AnimationClip animation;
    [SerializeField] float Factor;
    [SerializeField] float Cost;
    [SerializeField] bool IsOffensive;
    static float MoveAwaiter = 0.5f;
    private void Start()
    {
        animation = GetComponent<AnimationClip>();
    }
    static short DamageCalc()
    {
        return 0;
    }
    IEnumerator Cast(Unit caster, Unit target, List<Unit> queue)
    {
        // Check if the caster is null (not assigned)
        if (caster == null)
        {
            yield break; // Exit the coroutine if the caster is invalid
        }

        // Offensive spell casting logic
        if (IsOffensive)
        {
            // Check if the target is null (not assigned)
            if (target == null)
            {
                yield break; // Exit the coroutine if the target is invalid
            }

            yield return new WaitForSeconds(MoveAwaiter);
        }
        else // Defensive spell casting logic
        {
            // Check if the queue is null (not assigned)
            if (queue == null)
            {
                yield break; // Exit the coroutine if the queue is invalid
            }

            // Take all alive allays from list
            List<Unit> allays = new List<Unit>();
            foreach (Unit unit in queue) 
            {
                if(unit.GetType() == caster.GetType())
                {
                    allays.Add(unit);
                }
            }

            // Code for casting a spell on multiple units in the queue
            foreach (var unit in allays)
            {

                yield return new WaitForSeconds(MoveAwaiter);
            }
        }
    }
}
