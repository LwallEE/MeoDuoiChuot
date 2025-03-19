using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private Camera camera;

    public float Height => camera.orthographicSize * 2;
    public float Width => Height * camera.aspect;
    public Vector2 UpperLeftPos => camera.transform.position + new Vector3(-Width / 2, Height / 2, 0);
    public Vector2 LowerRightPos => camera.transform.position + new Vector3(Width / 2, -Height / 2, 0);

    protected override void Awake()
    {
        base.Awake();
        camera = GetComponent<Camera>();
    }

    public Vector2 GetWorldPointOfMousePosition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool IsInSide(Vector2 vt)
    {
        return vt.x > UpperLeftPos.x && vt.x <= LowerRightPos.x && vt.y >= LowerRightPos.y && vt.y <= UpperLeftPos.y;
    }

    public Vector2 ClampInsideScreen(Vector2 pos,float offset = 0)
    {
        float clampX = Mathf.Clamp(pos.x, UpperLeftPos.x+offset, LowerRightPos.x-offset);
        float clampY = Mathf.Clamp(pos.y, LowerRightPos.y+offset, UpperLeftPos.y-offset);
        return new Vector2(clampX, clampY);
    }

    public Vector2 GetRandomPositionInsideScreen(float offset = 0)
    {
        return new Vector2(Random.Range(UpperLeftPos.x+offset, LowerRightPos.x-offset),
            Random.Range(LowerRightPos.y+offset, UpperLeftPos.y-offset));
    }
}
