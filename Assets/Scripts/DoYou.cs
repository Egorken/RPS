using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoYou : MonoBehaviour
{
    public Text questionText; //���� �������. ������ ��������?
    public int Menu; // ����� �� �����
    public GameObject Panel; //������ � ��������. ������ ��������?
    public GameObject GameButtons; //������� ������
    public GameObject HPanel;
    public GameObject PanelResult;
    public float delay = 1f; // �������� 1 �������

    private void Start()
    {
        Panel.SetActive(false); //���� ������
        GameButtons.SetActive(false); //���� ������� ������
        HPanel.SetActive(false);
        PanelResult.SetActive(false);
        StartCoroutine(ShowButtonWithDelay()); //������ ������� ��� ������� ������
        questionText.text = "Do you want fight with KING?"; // ��� ������� ����� ������ ����� �������
    }
    private IEnumerator ShowButtonWithDelay()
    {
        yield return new WaitForSeconds(delay); //�������� ��������
        Panel.SetActive(true); //������������ ������
    }

    public void YesButton() // �������� ��� ������� ������ "��"
    {
        Panel.SetActive(false); //���� ������
        PanelResult.SetActive(true);
        GameButtons.SetActive(true);
        HPanel.SetActive(true);
    }

    public void NoButton() // �������� ��� ������� ������ "���"
    {
        SceneManager.LoadScene(Menu); //����� � ����
    }
}
