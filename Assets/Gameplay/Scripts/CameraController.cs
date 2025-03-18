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

    public Vector2 ClampInsideScreen(Vector2 pos)
    {
        float clampX = Mathf.Clamp(pos.x, UpperLeftPos.x, LowerRightPos.x);
        float clampY = Mathf.Clamp(pos.y, LowerRightPos.y, UpperLeftPos.y);
        return new Vector2(clampX, clampY);
    }

    public Vector2 GetRandomPositionInsideScreen()
    {
        return new Vector2(Random.Range(UpperLeftPos.x, LowerRightPos.x),
            Random.Range(LowerRightPos.y, UpperLeftPos.y));
    }
}
