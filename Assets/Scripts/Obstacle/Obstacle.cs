using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;

public class Obstacle : MonoBehaviour
{
    [Title("$GetInfoName")]
    public ObstacleInfo obstacleInfo;

    private string GetInfoName()
    {
        return obstacleInfo?.ObstacleName;
    }

    private void Update()
    {
        
    }

    public void OnClick()
    {
        if (obstacleInfo != null)
        {
            if (obstacleInfo.OnClick != null)
            {
                SoundManager.Instance.PlaySfx(obstacleInfo.OnClick);
            }

            if (obstacleInfo.click_conversation != "")
            {
                DialogueManager.StartConversation(obstacleInfo.click_conversation, null, null, -1);
            }
        }
    }

    public void OnTouch()
    {
        if (obstacleInfo != null)
        {
            if (obstacleInfo.touch_san != 0)
            {
                print(obstacleInfo.touch_san);
                SanUI.Instance.san += obstacleInfo.touch_san;
                SanUI.Instance.OnRefreshSan();

                //DialogueManager.StartConversation("µôsanÊ¾Àý¶Ô»°", null, null, -1);
                //DialogueManager.instance.conversationEnded += OnConversationEnd_Before;
            }

            if (obstacleInfo.OnTouch != null)
            {
                SoundManager.Instance.PlaySfx(obstacleInfo.OnTouch);
            }

            if (obstacleInfo.touch_conversation != "")
            {
                DialogueManager.StartConversation(obstacleInfo.touch_conversation, null, null, -1);
            }

            if (obstacleInfo.only_once)
            {
                this.enabled = false;
            }
        }
    }
}
