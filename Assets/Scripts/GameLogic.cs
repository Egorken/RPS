using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public enum Hand { Rock, Paper, Scissors, Block }
    public Hand playerHand;
    public Hand computerHand;
    
    public int health = 5;
    public int enemyhealth = 5;

    public Text result;
    public Text textgameresult;
    public Text HP;
    public Text EnemyHP;
    public Text GameOver;

    public GameObject PanelResult;
    public GameObject GameResult;
    public GameObject PanelHP;
    public GameObject playerObject;
    public GameObject EnemyObject;
    public GameObject DefenseObject;

    private PlayerScript playerScript;
    private EnemyScript enemyScript;

   [SerializeField] private int blockCounterEnemy = 0;
   [SerializeField] private int blockCounterPlayer = 0;
    private const int blockIntervalEnemy = 3;
    private const int blockIntervalPlayer = 3;


    // Скрипты

    private void Start()
    {
        GameResult.SetActive(false);
        DefenseObject.SetActive(false);
        playerScript = playerObject.GetComponent<PlayerScript>();
        enemyScript = EnemyObject.GetComponent<EnemyScript>();
    }

    public void SetPlayerHand(int handIndex)
    {
        playerHand = (Hand)handIndex;
        PlayRound();
    }

    private void PlayRound()
    {
        computerHand = GetComputerHand();

        if (playerHand == computerHand) //Ничья
        {
            result.text = "50/50";
            if (computerHand == Hand.Block)
            {
                result.text = "Together Block!";
                blockCounterEnemy = 0;
            }
        }

        else if ( //Игрок победил
            (playerHand == Hand.Rock && computerHand == Hand.Scissors) ||
            (playerHand == Hand.Paper && computerHand == Hand.Rock) ||
            (playerHand == Hand.Scissors && computerHand == Hand.Paper)
        )
        {
            result.text = "Enemy -1 HP";
            enemyhealth--;
        }

        else if (
            (playerHand == Hand.Block && computerHand == Hand.Scissors) ||
            (playerHand == Hand.Block && computerHand == Hand.Rock) ||
            (playerHand == Hand.Block && computerHand == Hand.Paper)
        )
        {
            result.text = "Block!";
        }

        else if (
            (computerHand == Hand.Block && playerHand == Hand.Scissors) ||
            (computerHand == Hand.Block && playerHand == Hand.Rock) ||
            (computerHand == Hand.Block && playerHand == Hand.Paper)
        )
        {
            result.text = "Enemy Block!";
            blockCounterEnemy = 0;
        }

        else if ( //Бот победил
            (computerHand == Hand.Rock && playerHand == Hand.Scissors) ||
            (computerHand == Hand.Paper && playerHand == Hand.Rock) ||
            (computerHand == Hand.Scissors && playerHand == Hand.Paper)
            )
        {
            result.text = "-1 HP";
            health--;
            playerScript.HitT();
        }

        
        switch (computerHand) // Вызов анимации у врага в зависимости от выпавшей руки
        {
            case Hand.Rock:
                enemyScript.EnemyRock();
                break;
            case Hand.Scissors:
                enemyScript.EnemyScissors();
                break;
            case Hand.Paper:
                enemyScript.EnemyPapper();
                break;
        }
    }

    public void GetBlockPlayer()
    {
        if (blockCounterPlayer >= blockIntervalPlayer) // Если количество ходов с последнего блока >= 3 ходов
        {
            DefenseObject.SetActive(true);
        }
    }

    public void DefenseButton()
    {
        blockCounterPlayer = 0;
        DefenseObject.SetActive(false);
    }

    public void CounterPlayer()
    {
        blockCounterPlayer++;
    }

    private Hand GetComputerHand()
    {
        Hand selectedHand;

        if (blockCounterEnemy >= blockIntervalEnemy) // Если количество ходов с последнего блока >= 3 ходов
        {
            selectedHand = (Hand)Random.Range(0, 4);
        }
        else
        {
            selectedHand = (Hand)Random.Range(0, 3);
            blockCounterEnemy++;
        }
        return selectedHand;
    }

    private void Update()
    {
        HP.text = health.ToString();
        EnemyHP.text = enemyhealth.ToString();
        if (health <= 0)
        {
            PanelResult.SetActive(false);
            GameResult.SetActive(true);
            textgameresult.text = "You're too weak! It's over.";
        }
        else if (enemyhealth <= 0)
        {
            PanelResult.SetActive(false);
            GameResult.SetActive(true);
            textgameresult.text = "You're the new KING!";
        }
    }
}