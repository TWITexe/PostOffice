using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMechanic : MonoBehaviour, IBeginDragHandler
{
     [SerializeField] private float speedMovement;
     [SerializeField] private float distanceMove;
     private Vector2 direction;
     private Vector2 position;

     private void Update()
     {
          Move();
     }

     public void OnBeginDrag(PointerEventData eventData)
     {
          position = new Vector2(Camera.main.transform.position.x,Camera.main.transform.position.y);
          direction = eventData.delta.x > 0 ? Vector2.right : Vector2.left;
     }

     private void Move()
     {
          var targetPos = position + direction * distanceMove;
          Camera.main.transform.position = Vector2.MoveTowards(Camera.main.transform.position, targetPos, speedMovement * Time.deltaTime);
          Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
     }
}


