using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class SanUI : MonoSingleton<SanUI>
{
    public float san;

    public float sanMax = 100;

    public Slider slider;

    public float walking_cost_san_speed = 1;

    public float fail_cost_san = 10;

    public Text text;

    public int curState;
    public List<int> sans = new List<int>();

    public List<AudioClip> bgms = new List<AudioClip>();

    public List<bool> popMaps = new List<bool>();

    [Title("每次点击鼠标消耗的san")]
    public float click_cost_san = 1;

    public string cur_scene_name;

    public string cur_scene_fail_talk;

    public UIScreen_Map ui_map;

    private void Start()
    {
        san = sanMax;
        text = slider.transform.Find("Text").GetComponent<Text>();
        OnRefreshSan();
        slider.interactable = false;
    }

    public void NextScene(Transform t)
    {
        print("zzzz");
        DialogueManager.instance.conversationEnded -= NextScene;
        SceneManager.LoadScene(cur_scene_name);
    }

    public void PopScreen(Transform t)
    {
        DialogueManager.instance.conversationEnded -= PopScreen;

        if (!ui_map.gameObject.activeInHierarchy)
        {
            ui_map.gameObject.SetActive(true);
            ui_map.OnPopScreen(false, null);
        }
    }

    public void OnRefreshSan()
    {
        slider.value = (float)san / sanMax;
        text.text = $"{(int)san}/{(int)sanMax}";

        if (san <= 0)
        {
            // 开始对话
            DialogueManager.StartConversation(cur_scene_fail_talk, null, null, -1);
            DialogueManager.instance.conversationEnded += NextScene;
            return;
        }

        var temp_state = GetCurState();
        if (curState != temp_state)
        {
            curState = temp_state;
            int vaildState = curState + 1;
            AudioClip audioClip = bgms[vaildState];
            SoundManager.Instance.SwitchBGM(audioClip);

            if (popMaps[vaildState])
            {
                // 开始对话
                DialogueManager.StartConversation("焦虑对话", null, null, -1);
                DialogueManager.instance.conversationEnded += PopScreen;
            }
        }
    }

    private int GetCurState()
    {
        int state = 0;
        for(int i = 0; i < sans.Count; i++)
        {

            var cur_san = sans[i];
            if (san >= cur_san)
            {
                state = i;
            }
        }
        return state;
    }
}
