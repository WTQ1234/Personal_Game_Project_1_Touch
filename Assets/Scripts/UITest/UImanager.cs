using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject ButtonList;//菜单按钮列表
    public GameObject ClickStartButton;//点击开始按钮，进入首页
    public GameObject MenuSetting;//菜单设置页面
    public GameObject Guide;//教程页面
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
