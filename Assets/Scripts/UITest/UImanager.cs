using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject ButtonList;//�˵���ť�б�
    public GameObject ClickStartButton;//�����ʼ��ť��������ҳ
    public GameObject MenuSetting;//�˵�����ҳ��
    public GameObject Guide;//�̳�ҳ��
    public AudioSource audioSource;


    void Start()
    {
        
    }
    public void ClickStart()
    {
        ClickStartButton.SetActive(false);
        ButtonList.SetActive(true);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("nextscene");
    }
    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

   
   

    void Update()
    {
        
    }
}
