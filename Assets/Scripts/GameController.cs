using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class GameController : MonoSingleton<GameController>
{
    public UIScreen_Map ui_map;
    public float time = 0;

    public bool is_end = false;
    void Update()
    {
        time += Time.deltaTime;

        if (is_end)
        {
            return;
        }

        if (PlayerController_TopDown.Instance.is_auto_moving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            PopScreen();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (time > 0.1f && Player.Instance.can_walk)
            {
                time = 0;

                SanUI.Instance.san -= SanUI.Instance.click_cost_san;
                SanUI.Instance.OnRefreshSan();

                var mousePos = Input.mousePosition;
                var world_pos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 mouse_pos = FogController.Instance.RevealAtMousePosition(world_pos);
                PlayerController_TopDown.Instance.OnSetClick(mouse_pos);
            }
        }
    }

    public void PopScreen()
    {
        if (PlayerController_TopDown.Instance.is_auto_moving)
        {
            return;
        }
        if (!ui_map.gameObject.activeInHierarchy)
        {
            ui_map.gameObject.SetActive(true);
            ui_map.OnPopScreen(true, null);
        }
    }
}
