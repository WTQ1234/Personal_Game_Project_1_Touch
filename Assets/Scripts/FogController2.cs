using System.Collections;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using HRL;
using UnityEngine.UI;

public class FogController2 : MonoSingleton<FogController2>
{
    [Title("����ƶ����ҿ�����İ뾶")]
    public int reveal_radius = 10; //�������õĽ�ʾ�뾶
    [Title("��ҵ����꣬�ҿ�����İ뾶")]
    [InfoBox("�޸Ĵ˴�����Ҫ��������Player�ļ��Collider�������Բ���")]
    public int reveal_radius_mouse = 25; //�������õĽ�ʾ�뾶
    [Title("��ҵ����꣬�ҿ�����ʱ��͸���Ȳ�˥���ĳ̶�")]
    public float mouse_rate = 0.9f;
    [Title("��ҵ����֮꣬���������úڰ��ļ��ʱ��")]
    public float fade_time = 1f; // �����䰵��ʱ��
    [Title("��ҵ����֮꣬���������úڰ��ı��ʣ�ԽСԽ��")]
    public float fade_rate = 0.99f;
    [Title("��ҵ����֮꣬���������úڰ�֮�󣬻��������һ��͸���ȣ� ��Χ0-1��0.1�ͺ�����")]
    public float fade_last = 0f;
    private Color[] _pixels;


    [Title("�����Ӧ��ͼ")]
    public Texture2D fogTexture; // ���ɰ�
    public Image img_fog;

    private int[] obstacles_id;         // �津�������ϰ�������꣬�����δ֪�ϰ����ô�渺�������ڵ�ͼѡ����ʾ����
    // ��͸���ȶ�����bool����������������������ë��
    private float[] pixels_is_forever;   // ���Ƿ�����Ҿ�����·�ߣ�����Ǿ����ģ���ô�˴�����lerp�������ڴ洢����·��
    private DateTime[] pixels_fade_times;    // ������ʱ�䣬����ʱ���ָ��ʱ�����lerp

    private int max_x;
    private int max_y;
    private int min_x;
    private int min_y;
    private bool can_screen_shortcut = false;
    private int total_length = 0;

    public float mouse_reveal_distance = 5;

    private bool isStart = false;

    private void Start()
    {
        isStart = true;
        _pixels = fogTexture.GetPixels();
        total_length = fogTexture.width * fogTexture.height;
        obstacles_id = new int[total_length];
        pixels_is_forever = new float[total_length];
        pixels_fade_times = new DateTime[total_length];
        for (int i = 0; i < total_length; i++)
        {
            _pixels[i] = Color.black;
            pixels_is_forever[i] = 1;
        }
        fogTexture.SetPixels(_pixels);
        fogTexture.Apply();

        // ��ʼ�����ڽ�ͼ��������
        max_x = int.MinValue;
        max_y = int.MinValue;
        min_x = int.MaxValue;
        min_y = int.MaxValue;
    }

    private void LateUpdate()
    {
        if (!isStart) return;
        var cur_date_time = DateTime.Now;
        for (int index = 0; index < pixels_fade_times.Length; index++)
        {
            if (pixels_fade_times[index] != DateTime.MinValue)
            {
                if (pixels_is_forever[index] > 0)
                {
                    if (Mathf.Abs((float)DiffSeconds(cur_date_time, pixels_fade_times[index])) > fade_time)
                    {
                        // ��ȡ����������Ŀ��͸����
                        var temp_color = Color.black;
                        temp_color.a = pixels_is_forever[index];

                        _pixels[index] = Color.Lerp(temp_color, _pixels[index], fade_rate);
                        if (_pixels[index].a > pixels_is_forever[index] - 0.01f - fade_last)
                        {
                            pixels_fade_times[index] = DateTime.MinValue;
                        }
                    }
                }
            }
        }
    }

    private double DiffSeconds(DateTime startTime, DateTime endTime)
    {
        TimeSpan secondSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
        return secondSpan.TotalSeconds;
    }

    public void RevealAtPosition(Vector2 pos, bool is_mouse = false)
    {
        if (!isStart) return;
        Vector2 texPos = WorldToTexturePos(pos);
        var x = Mathf.Clamp((int)texPos.x, 0, fogTexture.width);
        var y = Mathf.Clamp((int)texPos.y, 0, fogTexture.height);

        var radius = is_mouse ? reveal_radius_mouse : reveal_radius;
        var target_radius = radius * radius;
        var target_color = new Color(0, 0, 0, 0);
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                var cur_radius_length = i * i + j * j;
                if (cur_radius_length <= target_radius)
                {
                    var index = (y + j) * fogTexture.width + (x + i);
                    var temp_color = Color.Lerp(target_color, _pixels[index], (float)cur_radius_length / (float)target_radius * mouse_rate);
                    if (temp_color.a < _pixels[index].a)
                    {
                        _pixels[index].a = temp_color.a;
                    }
                    if (is_mouse)
                    {
                        pixels_fade_times[index] = DateTime.Now;
                    }
                    else
                    {
                        if (_pixels[index].a < pixels_is_forever[index])
                        {
                            pixels_is_forever[index] = _pixels[index].a;
                        }
                    }
                }
            }
        }

        // ���������Сֵ�����ڽ�ȡ�Ѿ��߹���·��
        if (x + radius > max_x)
        {
            max_x = x + radius;
            can_screen_shortcut = true;
        }
        if (x - radius < min_x)
        {
            min_x = x - radius;
            can_screen_shortcut = true;
        }
        if (y + radius > max_y)
        {
            max_y = y + radius;
            can_screen_shortcut = true;
        }
        if (y - radius < min_y)
        {
            min_y = y - radius;
            can_screen_shortcut = true;
        }

        fogTexture.SetPixels(_pixels);
        fogTexture.Apply();
    }

    public Vector2 RevealAtMousePosition(Vector2 mouse_pos_in_world)
    {
        Vector2 playerPos = Player.Instance.transform.position;
        Vector2 cur_pos = (mouse_pos_in_world - playerPos).normalized * mouse_reveal_distance + playerPos;
        RevealAtPosition(cur_pos, true);
        return cur_pos;
    }

    private Vector2 WorldToTexturePos(Vector2 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector3.forward, 99999, 1 << 7);
        if (hit.collider != null)
        {
            Bounds bounds = hit.collider.bounds;
            float texPosX = (hit.point.x - bounds.min.x) / bounds.size.x;
            float texPosY = (hit.point.y - bounds.min.y) / bounds.size.y;
            int x = Mathf.RoundToInt(texPosX * fogTexture.width);
            int y = Mathf.RoundToInt(texPosY * fogTexture.height);
            return new Vector2(x, y);
        }
        return Vector2.zero;
    }

    public Sprite ResetFogTextureScreenCut()
    {
        if (!can_screen_shortcut) return null;
        print(111);
        var width = fogTexture.width;
        var height = fogTexture.height;
        Color[] screen_cut_pixels = new Color[width * height];
        for (int index = 0; index < screen_cut_pixels.Length; index++)
        {
            screen_cut_pixels[index] = Color.clear;
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //var index_in_map = (j - center_height) * fogTexture.width + (i - center_width);
                var index_by_offset = (j) * fogTexture.height + (i );
                if (index_by_offset < pixels_is_forever.Length && index_by_offset > 0)
                {
                    float a = pixels_is_forever[index_by_offset];
                    if (a < 1)
                    {
                        screen_cut_pixels[index_by_offset] = Color.black;
                        screen_cut_pixels[index_by_offset].a = 0.8f;
                    }
                }
            }
        }
        Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
        texture2D.SetPixels(screen_cut_pixels);
        texture2D.Apply();
        return Sprite.Create(texture2D, new Rect(0, 0, width, height), Vector2.zero);
    }

    public Vector2 GetCurOffset()
    {
        var center_width = (max_x + min_x) / 2 - fogTexture.width / 2;
        var center_height = (max_y + min_y) / 2 - fogTexture.height / 2;
        return new Vector2(center_width, center_height);
    }

    public Vector2 GetCurPlayerOffset()
    {
        Vector2 texPos = WorldToTexturePos(Player.Instance.transform.position);
        
        return new Vector2(
            texPos.x / fogTexture.width,
            texPos.y / fogTexture.height);
    }
}