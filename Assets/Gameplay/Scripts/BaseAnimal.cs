using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimal : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected bool isMoving;
    private Transform tf;

    public Transform Tf
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }

            return tf;
        }
    }
    public float GetSpeed()
    {
        return speed;
    }
    protected void CheckFlip(float targetX)
    {
        if ((targetX < Tf.position.x && Tf.eulerAngles.y < 0.1f) || (targetX > Tf.position.x && Tf.eulerAngles.y > 0.1f))
        {
            Tf.Rotate(0,180,0);
        }
    }

    public void StopMove()
    {
        isMoving = false;
    }

    public virtual void StartMove()
    {
        isMoving = true;
    }
}
