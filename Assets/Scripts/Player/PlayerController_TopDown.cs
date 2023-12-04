using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HRL;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public class PlayerController_TopDown : MonoSingleton<PlayerController_TopDown>
{
    public float speed;
    public float speed_run;

    public Transform foot;

    public MouseClickTrigger click;

    public Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;

    public bool isRush;

    private PlayerInputActions controls;
    private Vector2 move;

    public CapsuleCollider2D capsuleCollider;

    void Awake()
    {
        //controls = new PlayerInputActions();
        //controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;

        //InputManager.Instance.RegisterKeyboardAction(InputOccasion.Update, KeyCode.Space, ButtonType.Down, Jump);
        //InputManager.Instance.RegisterMouseAction(InputOccasion.Update, 1, ButtonType.Down, RushReady);
        //InputManager.Instance.RegisterMouseAction(InputOccasion.Update, 1, ButtonType.Up, Rush);
    }

    void OnEnable()
    {
        //controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        //controls.GamePlay.Disable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        // 初始化擦除
        FogController.Instance.RevealAtPosition(foot.transform.position);
    }

    private void FixedUpdate()
    {
        Flip();
        Run();
    }

    void Update()
    {

        if (playerHasXAxisSpeed || is_auto_moving)
        {
            
        }
        FogController.Instance.RevealAtPosition(foot.transform.position);
        //SwitchAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            if (is_auto_moving)
            {
                print("11111111111 撞墙了");
                is_auto_moving = false;
                myRigidbody.velocity = Vector2.zero;

                DialogueManager.StartConversation("撞墙对话", null, null, -1);

                SanUI.Instance.san -= SanUI.Instance.fail_cost_san;
                SanUI.Instance.OnRefreshSan();
            }
        }
    }

    void Flip()
    {
        if (myRigidbody.velocity.x > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (myRigidbody.velocity.x < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        bool run = Mathf.Abs(myRigidbody.velocity.x) > 0.1f;
        bool up = (myRigidbody.velocity.y) > 0.1f;
        bool down = (myRigidbody.velocity.y) < -0.1f;

        //print($"-- {run} {down} {up}");

        myAnim.SetBool("Run", run);
        myAnim.SetBool("Down", down);
        myAnim.SetBool("Up", up);
    }

    bool playerHasXAxisSpeed;
    void Run()
    {
        if (GameController.Instance.is_end)
        {
            myRigidbody.velocity = Vector2.zero;
            myAnim.SetBool("Run", false);
            myAnim.SetBool("Down", false);
            myAnim.SetBool("Up", false);
            return;
        }

        if (!isRush && Player.Instance.can_walk && (!is_auto_moving))
        {
            var move_x = Input.GetAxis("Horizontal");
            var move_y = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(move_x * speed, move_y * speed);
                //Mathf.Lerp(move_x * speed, myRigidbody.velocity.x, 0.9f),
                //Mathf.Lerp(move_y * speed, myRigidbody.velocity.y, 0.9f));
            myRigidbody.velocity = playerVelocity;

            playerHasXAxisSpeed = 
                Mathf.Abs(myRigidbody.velocity.x) + Mathf.Abs(myRigidbody.velocity.y) > 0.05f; //
        }
        else
        {
            if (!is_auto_moving)
            {
                myRigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void OnSetClick(Vector2 click_pos)
    {
        click.gameObject.SetActive(true);
        click.gameObject.transform.position = click_pos;
        //particleSystem.gameObject.transform.position = click_pos;
    }

    public bool is_auto_moving = false;

    int cur_index;
    Vector2 vec_offset_ui;
    Vector2 vec_offset_point;
    public void DoPath(int index, float t)
    {
        print(index);

        cur_index = index;
        // 取得想要的点和起点
        var next_point_1 = Maps_List_InScene.Instance.maps_InScenes[index].transform.position;
        var next_point_2 = Maps_List_InScene.Instance.maps_InScenes[index + 1].transform.position;
        cur_index++;

        var cur_fake_point = Vector2.Lerp(next_point_1, next_point_2, t);

        Vector2 target_fake_point = next_point_2;

        Vector2 pos2D = transform.position;

        is_auto_moving = true;
        myRigidbody.velocity = (target_fake_point - cur_fake_point) * speed_run;
    }

    public void __DoPath()
    {
        if (!is_auto_moving) return;

        print("kkk");

        var last_cur_index = cur_index;
        cur_index++;
        if (cur_index > Maps_List_InScene.Instance.maps_InScenes.Count)
        {
            if (is_auto_moving)
            {
                is_auto_moving = false;
            }
            return;
        }
        print(Maps_List_InScene.Instance.maps_InScenes[last_cur_index].transform.name);
        print(Maps_List_InScene.Instance.maps_InScenes[cur_index].transform.name);
        Vector2 last_fake_point = Maps_List_InScene.Instance.maps_InScenes[last_cur_index].transform.position;
        Vector2 next_point = Maps_List_InScene.Instance.maps_InScenes[cur_index].transform.position;
        myRigidbody.velocity = (next_point - last_fake_point) * speed_run;
    }
}
