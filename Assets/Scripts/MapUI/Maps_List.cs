using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Maps_List : MonoBehaviour
{
    public List<Image> points = new List<Image>();

    public List<Lines> lines_list = new List<Lines>();

    public Transform player_head;

    public Sprite sprite_no;
    public Sprite sprite_yes;

    public float width = 3f;

    Image point_1 = null;
    public Image point_2 = null;

    public int cur_choosen_index = -1;
    private void Update()
    {
        lines_list.Clear();

        for (int i = 0; i < points.Count; i++)
        {
            var cur_point = points[i];
            cur_point.sprite = sprite_no;
        }
        for (int i = 0; i < points.Count - 1; i++)
        {
            var cur_point = points[i];
            var cur_point2 = points[i + 1];
            if (IsPlayerInPathRect(cur_point.transform.position, cur_point2.transform.position, player_head.position, width))
            {
                point_1 = cur_point;
                point_2 = cur_point2;
                cur_choosen_index = i;
                cur_point.sprite = sprite_yes;
                cur_point2.sprite = sprite_yes;
                break;
            }
        }
    }

    bool IsPlayerInPathRect(Vector2 point1, Vector2 point2, Vector2 playerPoint, float width)
    {
        // 计算路径方向和长度
        Vector2 direction = (point2 - point1).normalized;
        float distance = Vector2.Distance(point1, point2);

        // 计算路径构成的矩形的四个顶点
        Vector2 p1 = point1 + new Vector2(-direction.y, direction.x) * width / 2f;
        Vector2 p2 = point2 + new Vector2(-direction.y, direction.x) * width / 2f;
        Vector2 p3 = point2 + new Vector2(direction.y, -direction.x) * width / 2f;
        Vector2 p4 = point1 + new Vector2(direction.y, -direction.x) * width / 2f;
        lines_list.Add(new Lines(p1, p2));
        lines_list.Add(new Lines(p2, p3));
        lines_list.Add(new Lines(p3, p4));
        lines_list.Add(new Lines(p4, p1));

        // 检查玩家是否在矩形内
        bool isInRect = false;
        Vector2[] rectVertices = new Vector2[] { p1, p2, p3, p4 };
        for (int i = 0, j = rectVertices.Length - 1; i < rectVertices.Length; j = i++)
        {
            if ((rectVertices[i].y > playerPoint.y) != (rectVertices[j].y > playerPoint.y) &&
                (playerPoint.x < (rectVertices[j].x - rectVertices[i].x) * (playerPoint.y - rectVertices[i].y) / (rectVertices[j].y - rectVertices[i].y) + rectVertices[i].x))
            {
                isInRect = !isInRect;
            }
        }
        return isInRect;
    }

    private void OnDrawGizmos()
    {
        foreach(var line in lines_list)
        {
            Gizmos.DrawLine(line.p1, line.p2);
        }
    }
}

public class Lines
{
    public Vector2 p1, p2;

    public Lines(Vector2 _p1, Vector2 _p2)
    {
        p1 = _p1;
        p2 = _p2;
    }
}
