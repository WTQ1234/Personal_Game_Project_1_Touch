using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HRL;
using PixelCrushers.DialogueSystem;

public class UIScreen_Map : UIScreen
{
    [SerializeField] Button btn_Go;
    [SerializeField] Button btn_Close;

    [SerializeField] Image img_Map;
    [SerializeField] Image img_map_walk;
    [SerializeField] Image img_player_head;

    public Maps_List maps_List;

    public float move_speed = 1;

    public Vector3 init_img_pos;
    public Vector3 init_img_walk;
    public Vector3 init_img_player_head;
    public float max_x_radious = 8;
    public float max_y_radious = 4;
    protected override void Init()
    {
        base.Init();
        btn_Go.onClick.AddListener(OnClick_Go);
        btn_Close.onClick.AddListener(OnClick_Exit);
    }

    private void OnEnable()
    {
        Player.Instance.can_walk = false;
        init_img_pos = img_Map.transform.position;
        init_img_walk = img_map_walk.transform.position;
        init_img_player_head = img_player_head.transform.position;
    }

    private void OnDisable()
    {
        Player.Instance.can_walk = true;
        img_Map.transform.position = init_img_pos;
        img_map_walk.transform.position = init_img_walk;
        img_player_head.transform.position = init_img_player_head;
    }


    public void OnPopScreen(bool show_close_btn, Sprite sprite = null)
    {
        // TODO 地图配置 Level配置
        if (sprite != null)
        {
            img_Map.sprite = sprite;
        }
        btn_Close.gameObject.SetActive(show_close_btn);
        img_map_walk.sprite = FogController.Instance.ResetFogTextureScreenCut();

        Vector2 offset = FogController.Instance.GetCurOffset() * img_map_walk.rectTransform.localScale.x;
        Vector2 cur_pos = img_map_walk.rectTransform.anchoredPosition;
        img_map_walk.rectTransform.anchoredPosition = cur_pos - offset;

        Vector2 offset_player = FogController.Instance.GetCurPlayerOffset();
        offset_player = new Vector2(
            offset_player.x * img_map_walk.rectTransform.rect.size.x * img_map_walk.rectTransform.localScale.x,
            offset_player.y * img_map_walk.rectTransform.rect.size.y * img_map_walk.rectTransform.localScale.x);
        var start_x = img_map_walk.rectTransform.anchoredPosition.x -
            img_map_walk.rectTransform.rect.size.x * img_map_walk.rectTransform.localScale.x / 2 + offset_player.x;
        var start_y = img_map_walk.rectTransform.anchoredPosition.y -
            img_map_walk.rectTransform.rect.size.y * img_map_walk.rectTransform.localScale.y / 2 + offset_player.y;
        Vector2 cur_pos_player = img_map_walk.rectTransform.anchoredPosition;
        img_player_head.rectTransform.anchoredPosition = new Vector2(start_x, start_y);
    }

    private void OnClick_Go()
    {
        // 对话测试
        if (maps_List.cur_choosen_index != -1)
        {
            Vector2 off1 = img_player_head.rectTransform.anchoredPosition - maps_List.points[maps_List.cur_choosen_index].rectTransform.anchoredPosition;
            Vector2 off2 = img_player_head.rectTransform.anchoredPosition - maps_List.points[maps_List.cur_choosen_index + 1].rectTransform.anchoredPosition;

            float t = off1.sqrMagnitude / (off1.sqrMagnitude + off2.sqrMagnitude);

            //// 这个是UI上认为的玩家相对于第一个起点的UI的距离
            //Vector2 offset_ui = img_player_head.rectTransform.anchoredPosition - maps_List.points[0].rectTransform.anchoredPosition;
            //// 这个是UI上认为的下一个Point 相对于第一个起点的距离
            //Vector2 offset_ui_point = maps_List.points[maps_List.cur_choosen_index].rectTransform.anchoredPosition - maps_List.points[0].rectTransform.anchoredPosition;

            PlayerController_TopDown.Instance.DoPath(maps_List.cur_choosen_index, t);
            gameObject.SetActive(false);
        }
    }

    private void OnClick_Exit()
    {
        print("111");
        gameObject.SetActive(false);
    }

    private bool isRightDown = false;
    public void OnClick_Rotate_Right_Up()
    {
        isRightDown = false;
    }

    public void OnClick_Rotate_Right_Down()
    {
        isRightDown = true;
    }

    private bool isLeftDown = false;
    public void OnClick_Rotate_Left_Down()
    {
        isLeftDown = true;
    }

    public void OnClick_Rotate_Left_Up()
    {
        isLeftDown = false;
    }

    private void Update()
    {
        if (isLeftDown)
        {
            img_Map.transform.Rotate(Vector3.forward);
        }
        if (isRightDown)
        {
            img_Map.transform.Rotate(Vector3.back);
        }
        if (!Player.Instance.can_walk)
        {
            var move_x = Input.GetAxis("Horizontal");
            var move_y = Input.GetAxis("Vertical");
            var temp_pos = img_Map.transform.position;
            temp_pos += new Vector3(move_x, move_y, 0) * move_speed * Time.deltaTime;

            temp_pos.x = Mathf.Clamp(temp_pos.x, init_img_pos.x - max_x_radious, init_img_pos.x + max_x_radious);
            temp_pos.y = Mathf.Clamp(temp_pos.y, init_img_pos.y - max_y_radious, init_img_pos.y + max_y_radious);
            img_Map.transform.position = temp_pos;
        }
    }
}
