using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField]
  private GameObject player;

  private Transform playerTransform;
  private float smoothTime = 0.15f;
  private Vector3 velocity = Vector3.zero;

  void Awake()
  {
    playerTransform = player.transform;
  }

  void LateUpdate()
  {
    var targetPosition = playerTransform.TransformPoint(new Vector3(0, 0.0f, -10));

    transform.position = Vector3.SmoothDamp(
      transform.position,
      targetPosition,
      ref velocity,
      smoothTime
    );
  }
}
