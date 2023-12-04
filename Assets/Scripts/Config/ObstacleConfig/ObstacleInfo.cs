using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;
using HRL;

[SerializeField]
public class ObstacleInfo : BasicInfo
{
    public string ObstacleName;

    public ObstacleType obstacleType;

    [Title("����sanֵ�ı�")]
    public float touch_san = -1;

    [Title("������ϰ��������")]
    public AudioClip OnClick;

    [Title("�����ϰ�������������")]
    public AudioClip OnTouch;


    [Title("�Ƿ�Ӵ�ֻ����һ��")]
    public bool only_once = true;

    [Title("���ʱ������Ķ���")]
    public string click_conversation = "";

    [Title("�Ӵ�ʱ������Ķ���")]
    public string touch_conversation = "";
}

public enum ObstacleType
{
    Green,
    Yellow,
    Red,
}
