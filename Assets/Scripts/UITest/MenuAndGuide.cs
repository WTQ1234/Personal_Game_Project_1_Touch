using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuAndGuide : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] private bool menuKeys = true;//判断Menu状态

    public GameObject guide;
    [SerializeField] private bool guideKeys = true;//判断guide状态
    void Start()
    {
        
    }
    public void MenuOpen()//设置菜单打开
    {
        if (menuKeys)
        {
             menu.SetActive(true);
           //menu.transform.DOScale(new Vector3(100, 100, 100),0.5f);
           // menu.transform.localScale=new Vector3(1,1,1);
            menuKeys = false;
            Time.timeScale = 0;//时间暂停
            Debug.Log("1");
            
        }
        else
        {
            menu.SetActive(false);
            //menu.transform.DOScale(new Vector3(0,0,0), 0.5f);
            //menu.transform.localScale = new Vector3(0, 0, 0);
            menuKeys = true;
            Time.timeScale = 1;//时间恢复
            Debug.Log("2");
        }
    }

    public void Restart()//重新开始按钮
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载场景
    }

    public void Backhome()//退出游戏，回到首页
    {
        SceneManager.LoadScene(0);//加载场景0，首页场景
    }

    public void GuideOpen()//打开指导页
    {
        if (guideKeys)
        {
            guide.SetActive(true);
            guideKeys = false;
            Time.timeScale = 0;//时间暂停
        }
        else
        {
            guide.SetActive(false);
            guideKeys = true;
            Time.timeScale = 1;//时间恢复
        }
    }


    void Update()//ESC打开设置页面
    {
        //if (menuKeys)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        menu.SetActive(true);
        //        menuKeys = false;
        //        Time.timeScale = 0;//时间暂停
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    menu.SetActive(false);
        //    menuKeys = true;
        //    Time.timeScale = 1;//时间恢复
        //}
    }
}
