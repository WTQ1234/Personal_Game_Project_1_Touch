using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuAndGuide : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] private bool menuKeys = true;//�ж�Menu״̬

    public GameObject guide;
    [SerializeField] private bool guideKeys = true;//�ж�guide״̬
    void Start()
    {
        
    }
    public void MenuOpen()//���ò˵���
    {
        if (menuKeys)
        {
             menu.SetActive(true);
           //menu.transform.DOScale(new Vector3(100, 100, 100),0.5f);
           // menu.transform.localScale=new Vector3(1,1,1);
            menuKeys = false;
            Time.timeScale = 0;//ʱ����ͣ
            Debug.Log("1");
            
        }
        else
        {
            menu.SetActive(false);
            //menu.transform.DOScale(new Vector3(0,0,0), 0.5f);
            //menu.transform.localScale = new Vector3(0, 0, 0);
            menuKeys = true;
            Time.timeScale = 1;//ʱ��ָ�
            Debug.Log("2");
        }
    }

    public void Restart()//���¿�ʼ��ť
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���¼��س���
    }

    public void Backhome()//�˳���Ϸ���ص���ҳ
    {
        SceneManager.LoadScene(0);//���س���0����ҳ����
    }

    public void GuideOpen()//��ָ��ҳ
    {
        if (guideKeys)
        {
            guide.SetActive(true);
            guideKeys = false;
            Time.timeScale = 0;//ʱ����ͣ
        }
        else
        {
            guide.SetActive(false);
            guideKeys = true;
            Time.timeScale = 1;//ʱ��ָ�
        }
    }


    void Update()//ESC������ҳ��
    {
        //if (menuKeys)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        menu.SetActive(true);
        //        menuKeys = false;
        //        Time.timeScale = 0;//ʱ����ͣ
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    menu.SetActive(false);
        //    menuKeys = true;
        //    Time.timeScale = 1;//ʱ��ָ�
        //}
    }
}
