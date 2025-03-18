using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mouse : BaseAnimal
{
   
   [SerializeField] private Transform goal;
   private Cat cat;
   private float Distance_Check = 0.1f;
   private float Distance_Run = 2f;
   private Vector2 targetPoint;

   private void Awake()
   {
      cat = FindObjectOfType<Cat>();
   }

   private void FixedUpdate()
   {
      if (!isMoving) return;
      MoveToTargetPoint();
   }

   void MoveToTargetPoint()
   {
      if (Vector2.Distance(Tf.position, targetPoint) < Distance_Check)
      {
         targetPoint = GetRandomTargetPoint();
         CheckFlip(targetPoint.x);
      }

      var direction = (targetPoint - (Vector2)Tf.position).normalized;
      Tf.position += (Vector3)direction * (speed * Time.deltaTime);
   }

   Vector2 GetRandomTargetPoint()
   {
      var direction = (Tf.position - cat.Tf.position).normalized;
      var result = Random.Range(0,4) == 0 ?
         new Vector2(-direction.y, direction.x)
         : new Vector2(direction.y, -direction.x);
      float distance = Random.Range(1, CameraController.Instance.Height);
      result *= distance;
      result = result + (Vector2)Tf.position;
      return CameraController.Instance.ClampInsideScreen(result);
   }

  

  
}
