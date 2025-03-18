using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : BaseAnimal
{
    private Mouse mouseTarget;
    private float balanceRatio = 0.7f;
    private void Awake()
    {
        mouseTarget = FindObjectOfType<Mouse>();
        speed = mouseTarget.GetSpeed()*balanceRatio;
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;
        CheckFlip(mouseTarget.Tf.position.x);
        var direction = (mouseTarget.Tf.position - Tf.position).normalized;
        Tf.position += (Vector3)direction * (speed * Time.deltaTime);
    }
}
