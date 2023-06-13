using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public enum Hand { Rock, Paper, Scissors, Block } //��������� ������� ������ ����

    public Hand playerHand; //���� ������
    public Hand computerHand; //���� ����

    public int health = 5; //�������� ������
    public int enemyhealth = 5; //�������� ����

    public Text resultText; //����� ���������� ������
    public Text textgameresult; //����� ���������� ����
    public Text playerHealthText; //����� ���������� ����������� �������� ������
    public Text enemyHealthText; //����� ���������� ����������� �������� ����

    public GameObject panelResult; //������ ���������� ������
    public GameObject gameResult; //������ ���������� ����
    public GameObject playerObject; //�����
    public GameObject enemyObject; //���
    public GameObject defenseObject; //������ �����

    private PlayerScript playerScript; //��������� � ������ ������
    private EnemyScript enemyScript; //��������� � ������ ����

    [SerializeField] private int blockCounterEnemy = 0; //���������� ����� ����� ������ ����� � ����
    [SerializeField] private int blockCounterPlayer = 0; //���������� ����� ����� ������ ����� � ������
    private const int blockIntervalEnemy = 3; //����� ������� ����� ������ ������, ����� ����� ���� ������������ ���� ����
    private const int blockIntervalPlayer = 3; //����� ������� ����� ������ ������, ����� ����� ���� ������������ ���� ������

    private void Start() //��� ������� �����
    {
        gameResult.SetActive(false);
        defenseObject.SetActive(false);
        playerScript = playerObject.GetComponent<PlayerScript>();
        enemyScript = enemyObject.GetComponent<EnemyScript>();
    }

    public void SetPlayerHand(int handIndex) //����� ���� ������� � ������ ������
    {
        playerHand = (Hand)handIndex;
        PlayRound();
    }

    private void PlayRound() //������ ������
    {
        computerHand = GetComputerHand(); //��� �������� ����

        if (playerHand == computerHand) //�����
        {
            resultText.text = "50/50";
            if (computerHand == Hand.Block) //��� � �����
            {
                resultText.text = "Together Block!";
                blockCounterEnemy = 0;
            }
        }
        else if (IsPlayerWin(playerHand, computerHand)) //������ � ������ ������
        {
            resultText.text = "Enemy -1 HP";
            enemyhealth--;
        }
        else if (playerHand == Hand.Block && IsComputerWin(computerHand, playerHand)) //����� � �����
        {
            resultText.text = "Block!";
        }
        else if (computerHand == Hand.Block && IsComputerWin(computerHand, playerHand)) //��� � �����
        {
            resultText.text = "Enemy Block!";
            blockCounterEnemy = 0;
        }
        else if (IsComputerWin(computerHand, playerHand)) //������ � ������ ����
        {
            resultText.text = "-1 HP";
            health--;
            playerScript.HitT();
        }

        PlayEnemyAnimation(computerHand); //�������� ����
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

    private void PlayEnemyAnimation(Hand hand) //������ �������� ����
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

    public void GetBlockPlayer() //���������� ������ �����
    {
        if (blockCounterPlayer >= blockIntervalPlayer)
        {
            defenseObject.SetActive(true);
        }
    }

    public void DefenseButton() //����������� ������ �����
    {
        blockCounterPlayer = 0;
        defenseObject.SetActive(false);
    }

    public void CounterPlayer() //������� ����� ��� �����
    {
        blockCounterPlayer++;
    }

    private Hand GetComputerHand() //��� �������� ������ ����
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

    private void Update() //���������� ���������� ����������
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