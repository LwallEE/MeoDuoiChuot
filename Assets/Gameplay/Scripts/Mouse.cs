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

   private bool isChase;
   private void Awake()
   {
      cat = FindObjectOfType<Cat>();
   }

   private void FixedUpdate()
   {
      if (!isMoving) return;
      if (goal.gameObject.activeInHierarchy)
      {
         MoveToGoal();
      }
      else
      {
         MoveToTargetPoint();
      }
   }

   public override void StartMove()
   {
      base.StartMove();
      Initial();
   }

   public void Initial()
   {
      targetPoint = Tf.position;
   }
   void MoveToGoal()
   {
      CheckFlip(goal.position.x);
      var direction = (goal.position - Tf.position).normalized;
      Tf.position += (Vector3)direction * (speed * Time.deltaTime);
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

   public void SetIsChase(bool value)
   {
      isChase = value;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Cat") && isChase)
      {
         GameManager.Instance.GameLose();
      }
      else if (other.CompareTag("Goal"))
      {
         GameManager.Instance.GameWin();
      }
   }
}
