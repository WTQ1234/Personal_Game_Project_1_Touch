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

    [Title("碰触san值改变")]
    public float touch_san = -1;

    [Title("鼠标点击障碍物的声音")]
    public AudioClip OnClick;

    [Title("人与障碍物碰触的声音")]
    public AudioClip OnTouch;


    [Title("是否接触只触发一次")]
    public bool only_once = true;

    [Title("点击时候的内心独白")]
    public string click_conversation = "";

    [Title("接触时候的内心独白")]
    public string touch_conversation = "";
}

public enum ObstacleType
{
    Green,
    Yellow,
    Red,
}
