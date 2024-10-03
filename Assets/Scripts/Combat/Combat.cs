using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Combat : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject EnemiesHandler;
    [SerializeField] GameObject CharactersHandler;

    [Header("EnemySpawn")]
    [SerializeField] GameObject[] Bestiary;

    [Tooltip("0 = Random between 1-4\n1-4 = defalut values\n5+ or less than 0 = correct to 4")]
    [SerializeField] byte EnemyCounter;

    Vector3[] EnemyPositions = new Vector3[4];

    /// <summary>
    /// Lists
    /// Characters - contains all alive characters
    /// Enemies - contains all alive enemies
    /// Queue - contains all alive units ordered by Spd
    /// </summary>
    List<Character> Characters = new List<Character>();
    List<Enemy> Enemies = new List<Enemy>();

    List<Unit> Queue = new List<Unit>();
    int ActualUnitIndex = 0;

    void Start()
    {
        EnemyCounterCorrector();

        Characters = CharactersHandler.GetComponentsInChildren<Character>().ToList<Character>();

    }

    IEnumerator Master()
    {
        while (true)
        {
            Unit unit = Queue[ActualUnitIndex];

            yield return StartCoroutine(unit.TakeAction());

            ActualUnitIndex++;
        }
    }


    #region Enemies Prepare
    void EnemyCounterCorrector()
    {
        if (EnemyCounter == 0)
        {
            EnemyCounter = (byte)Random.Range(1, 4);
        }
        else if (EnemyCounter < 0 || EnemyCounter > 4)
        {
            EnemyCounter = 4;
        }
    }

    void InitalizeEnemies()
    {

    }

    void SetEnemiesPositions()
    {
        if(Characters.Count < EnemyPositions.Length)
        {
            Debug.Log("Err: SetEnemiesPositions() - Characters count is less than Enemies count");
            return;
        }

        for(int i = 0; i < EnemyPositions.Length; i++)
        {
            // Get Character position from list and flip from X to -X
            EnemyPositions[i] = Characters[i].gameObject.transform.position;
            EnemyPositions[i].x *= -1;
        }

    }

    #endregion

}
