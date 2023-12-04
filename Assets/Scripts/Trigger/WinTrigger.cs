using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public bool is_end = false;

    public string next_scene_name;

    public string dialog_name;

    public SpriteRenderer fog;

    public List<Image> SpriteRendererList = new List<Image>();

    public int index = 0;

    public Button btn;

    public float fadetime = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.name == "Player")
            {
                print("Ӯ��");

                index = -1;

                GameController.Instance.is_end = true;

                var tween = fog.DOFade(0, fadetime);
                tween.onComplete = ShowImgList;

                // ��ʼ�Ի�
                DialogueManager.StartConversation(dialog_name, null, null, -1);
                // ���ذ�ť
                DialogueManager.instance.conversationEnded += ShowButton;
            }
        }
    }

    private void ShowImgList()
    {
        index++;
        if (index >= 0 && index < SpriteRendererList.Count)
        {
            Image spriteRenderer = SpriteRendererList[index];
            // ��ʾ��
            var tween = spriteRenderer.DOFade(1, fadetime);
            tween.onComplete = ShowImgList;
        }
    }

    private void ShowButton(Transform t)
    {
        DialogueManager.instance.conversationEnded -= ShowButton;
        btn.gameObject.SetActive(true);
        btn.GetComponent<Image>().DOFade(1, fadetime);
        btn.enabled = true;
    }

    public void NextScene()
    {
        print("xxx");
        SceneManager.LoadScene(next_scene_name);
    }

}
