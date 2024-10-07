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
    [SerializeField] PanelManager PanelManager;

    [Header("EnemySpawn")]
    [SerializeField] GameObject[] Bestiary;

    [Tooltip("0 = Random between 1-4\n1-4 = defalut values\n5+ or less than 0 = correct to 4")]
    [SerializeField] byte EnemyCounter;

    [SerializeField] float EnemyPositionOffsetX;
    [SerializeField] float EnemyPositionOffsetY;

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

    int TurnCounter = 1;

    void Start()
    {
        EnemyCounterCorrector();

        Characters = CharactersHandler.GetComponentsInChildren<Character>().ToList<Character>();

        InitalizeEnemies();


        Queue.AddRange(Characters);
        Queue.AddRange(Enemies);
        Queue.Sort((u1, u2) => u1.Spd.CompareTo(u2.Spd));
        Queue.Reverse();

        StartCoroutine(Master());
    }

    IEnumerator Master()
    {
        while (true)
        {
            Unit unit = Queue[ActualUnitIndex];

            yield return StartCoroutine(unit.TakeAction(Queue, PanelManager));

            ActualUnitIndex++;

            if(ActualUnitIndex >= Queue.Count)
            {
                ActualUnitIndex = 0;
                TurnCounter++;
            }
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
        SetEnemiesPositions();

        float LastY = EnemyPositions[0].y;

        for (int i = 0; i < EnemyCounter; i++)
        {
            int index = Random.Range(0, Bestiary.Length);
            GameObject enemyObj = Bestiary[index];

            if (LastY > EnemyPositions[i].y)
            {
               enemyObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            else
            {
               enemyObj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }

            Enemy enemy = Instantiate(enemyObj, EnemyPositions[i], Quaternion.identity).gameObject.GetComponent<Enemy>();
            LastY = EnemyPositions[i].y;

            enemy.transform.SetParent(EnemiesHandler.transform, true);

            if (enemy != null)
            {
               Enemies.Add(enemy);
            }
            
        }
    }

    void SetEnemiesPositions()
    {
        if (Characters.Count < EnemyPositions.Length)
        {
            Debug.Log("Err: SetEnemiesPositions() - Characters count is less than Enemies count");
            return;
        }
        
        for (int i = 0; i < EnemyPositions.Length; i++)
        {
            // Get Character position from list and flip from X to -X
            Vector3 newPosition = Characters[i].gameObject.transform.position;
            newPosition.x *= -1;  // Flip X-axis

            newPosition += new Vector3(EnemyPositionOffsetX, EnemyPositionOffsetY, 0);

            EnemyPositions[i] = newPosition; 
        }
    }

    #endregion

}
