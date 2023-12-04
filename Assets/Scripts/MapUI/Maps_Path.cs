using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps_Path : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    public Transform playerpoint;

    private void Update()
    {
        print(IsPlayerInPathRect(point1.transform.position, point2.transform.position, playerpoint.transform.position, 5));
    }


    Vector2 p1, p2, p3, p4 = Vector2.zero;
    bool IsPlayerInPathRect(Vector2 point1, Vector2 point2, Vector2 playerPoint, float width)
    {
        // 计算路径方向和长度
        Vector2 direction = (point2 - point1).normalized;
        float distance = Vector2.Distance(point1, point2);

        // 计算路径构成的矩形的四个顶点
        p1 = point1 + new Vector2(-direction.y, direction.x) * width / 2f;
        p2 = point2 + new Vector2(-direction.y, direction.x) * width / 2f;
        p3 = point2 + new Vector2(direction.y, -direction.x) * width / 2f;
        p4 = point1 + new Vector2(direction.y, -direction.x) * width / 2f;



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
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            print(collision.name);
            if (collision.tag == "PlayerHead")
            {
                print("llllllllllll");
            }
        }
    }
}
