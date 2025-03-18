using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Line : MonoBehaviour
{
    [SerializeField] private Transform startPoint;

    [SerializeField] private Transform endPoint;

    [SerializeField] private float lineThickness = 0.2f;
    [SerializeField] private float centerRadius = 0.2f;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CircleCollider2D centerCollider2D;

    [SerializeField] private float lengthTest;
    
    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = CameraController.Instance.GetWorldPointOfMousePosition();
            if (boxCollider.OverlapPoint(mousePos))
            {
                if (centerCollider2D.OverlapPoint(mousePos))
                {
                    Debug.Log("Correct");
                }
                else
                {
                    Debug.Log("Incorrect");
                }
            }
        }
    }

    [ContextMenu("Setup")]
    public void Setup()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
        lineRenderer.startWidth = lineRenderer.endWidth = lineThickness;
        
        FitCollider();
    }
    
    void FitCollider()
    {
        Vector2 direction = (endPoint.position - startPoint.position).normalized;
        float length = Vector2.Distance(startPoint.position, endPoint.position);
        Vector2 center = (startPoint.position + endPoint.position) / 2f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set BoxCollider2D properties
        boxCollider.size = new Vector2(length, lineThickness);
        boxCollider.transform.position = center; // Adjust position relative to the GameObject
        boxCollider.transform.rotation = Quaternion.Euler(0, 0, angle); // Rotate to match line
        
        //setup center
        centerCollider2D.transform.position = center;
        centerCollider2D.radius = centerRadius;
    }

    public void GenerateRandomLine(float length)
    {
        Vector2 dau = CameraController.Instance.GetRandomPositionInsideScreen();

        float cuoi_x = dau.x + Random.Range(-length, length);
        float cuoi_y = Mathf.Sqrt(length * length - Mathf.Pow(cuoi_x - dau.x, 2)) + dau.y;

        Vector2 cuoi = CameraController.Instance.ClampInsideScreen(new Vector2(cuoi_x, cuoi_y));

        startPoint.position = dau;
        endPoint.position = cuoi;
        Setup();
    }

    [ContextMenu("Test")]
    public void TestGenerate()
    {
        GenerateRandomLine(lengthTest);
    }
}
