using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public enum Hand { Rock, Paper, Scissors, Block } //возможный вариант выбора руки

    public Hand playerHand; //рука игрока
    public Hand computerHand; //рука бота

    public int health = 5; //здоровье игрока
    public int enemyhealth = 5; //здоровье бота

    public Text resultText; //текст результата раунда
    public Text textgameresult; //текст результата игры
    public Text playerHealthText; //текст количества оставшегося здоровья игрока
    public Text enemyHealthText; //текст количества оставшегося здоровья бота

    public GameObject panelResult; //Панель результата раунда
    public GameObject gameResult; //Панель результата игры
    public GameObject playerObject; //Игрок
    public GameObject enemyObject; //Бот
    public GameObject defenseObject; //Кнопка Блока

    private PlayerScript playerScript; //Обращение к классу игрока
    private EnemyScript enemyScript; //Обращение к классу бота

    [SerializeField] private int blockCounterEnemy = 0; //Количество ходов после выбора блока у бота
    [SerializeField] private int blockCounterPlayer = 0; //Количество ходов после выбора блока у игрока
    private const int blockIntervalEnemy = 3; //Через сколько ходов должно пройти, чтобы можно было использовать блок боту
    private const int blockIntervalPlayer = 3; //Через сколько ходов должно пройти, чтобы можно было использовать блок игроку

    private void Start() //При запуске сцены
    {
        gameResult.SetActive(false);
        defenseObject.SetActive(false);
        playerScript = playerObject.GetComponent<PlayerScript>();
        enemyScript = enemyObject.GetComponent<EnemyScript>();
    }

    public void SetPlayerHand(int handIndex) //Выбор руки игроком и запуск раунда
    {
        playerHand = (Hand)handIndex;
        PlayRound();
    }

    private void PlayRound() //Запуск раунда
    {
        computerHand = GetComputerHand(); //Бот выбирает удар

        if (playerHand == computerHand) //Ничья
        {
            resultText.text = "50/50";
            if (computerHand == Hand.Block) //Оба в блоке
            {
                resultText.text = "Together Block!";
                blockCounterEnemy = 0;
            }
        }
        else if (IsPlayerWin(playerHand, computerHand)) //Победа в раунде игрока
        {
            resultText.text = "Enemy -1 HP";
            enemyhealth--;
        }
        else if (playerHand == Hand.Block && IsComputerWin(computerHand, playerHand)) //Игрок в блоке
        {
            resultText.text = "Block!";
        }
        else if (computerHand == Hand.Block && IsComputerWin(computerHand, playerHand)) //Бот в блоке
        {
            resultText.text = "Enemy Block!";
            blockCounterEnemy = 0;
        }
        else if (IsComputerWin(computerHand, playerHand)) //Победа в раунде бота
        {
            resultText.text = "-1 HP";
            health--;
            playerScript.HitT();
        }

        PlayEnemyAnimation(computerHand); //Анимация бота
    }

    private bool IsPlayerWin(Hand player, Hand computer)
    {
        return (player == Hand.Rock && computer == Hand.Scissors) ||
               (player == Hand.Paper && computer == Hand.Rock) ||
               (player == Hand.Scissors && computer == Hand.Paper);
    }

    private bool IsComputerWin(Hand computer, Hand player)
    {
        return IsPlayerWin(computer, player);
    }

    private void PlayEnemyAnimation(Hand hand) //Запуск анимации бота
    {
        switch (hand)
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

    public void GetBlockPlayer() //Включается кнопка блока
    {
        if (blockCounterPlayer >= blockIntervalPlayer)
        {
            defenseObject.SetActive(true);
        }
    }

    public void DefenseButton() //Выключается кнопка блока
    {
        blockCounterPlayer = 0;
        defenseObject.SetActive(false);
    }

    public void CounterPlayer() //Подсчёт ходов без блока
    {
        blockCounterPlayer++;
    }

    private Hand GetComputerHand() //Бот выбирает наугад руку
    {
        Hand selectedHand;

        if (blockCounterEnemy >= blockIntervalEnemy)
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

    private void Update() //Постоянное обновление информации
    {
        playerHealthText.text = health.ToString();
        enemyHealthText.text = enemyhealth.ToString();

        if (health <= 0)
        {
            panelResult.SetActive(false);
            gameResult.SetActive(true);
            textgameresult.text = "You're too weak! It's over.";
        }
        else if (enemyhealth <= 0)
        {
            panelResult.SetActive(false);
            gameResult.SetActive(true);
            textgameresult.text = "You're the new KING!";
        }
    }
}