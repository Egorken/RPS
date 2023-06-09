using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoYou : MonoBehaviour
{
    public Text questionText; //Поле вопроса. Хочешь поиграть?
    public int Menu; // Смена на сцену
    public GameObject Panel; //Панель с вопросом. Хочешь поиграть?
    public GameObject GameButtons; //Игровые кнопки
    public GameObject HPanel;
    public GameObject PanelResult;
    public float delay = 1f; // Задержка 1 секунда

    private void Start()
    {
        Panel.SetActive(false); //Выкл панель
        GameButtons.SetActive(false); //Выкл игровые кнопки
        HPanel.SetActive(false);
        PanelResult.SetActive(false);
        StartCoroutine(ShowButtonWithDelay()); //Запуск функции для запуска панели
        questionText.text = "Do you want fight with KING?"; // При запуске сцены задаем текст вопроса
    }
    private IEnumerator ShowButtonWithDelay()
    {
        yield return new WaitForSeconds(delay); //Включить задержку
        Panel.SetActive(true); //Активировать панель
    }

    public void YesButton() // Действия при нажатии кнопки "да"
    {
        Panel.SetActive(false); //Выкл панель
        PanelResult.SetActive(true);
        GameButtons.SetActive(true);
        HPanel.SetActive(true);
    }

    public void NoButton() // Действия при нажатии кнопки "нет"
    {
        SceneManager.LoadScene(Menu); //Выход в меню
    }
}
