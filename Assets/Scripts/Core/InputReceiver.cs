using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class InputReceiver : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 10f;

        private CubeController cubeController = null;

        private Vector3 startPosition;
        private Vector3 endPosition;

        public void Init(CubeController cubeController)
        {
            this.cubeController = cubeController;
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endPosition = touch.position;

                    Direction rotation = DetermineRotation();

                    StartCoroutine(cubeController.RotateCube(rotation));
                }
            }
        }

        private Direction DetermineRotation()
        {
            Vector2 delta = (endPosition - startPosition);

            // 터치 처리
            if (delta.sqrMagnitude < sensitivity)
            {
                if (endPosition.x < Screen.width * 0.5f) return Direction.CounterClock;
                else return Direction.Clock;
            }

            // 드래그 처리
            float angle = Vector2.SignedAngle(Vector2.right, delta);

            if (angle > -45f && angle <= 45f) return Direction.Right;
            if (angle > 45f && angle <= 135f) return Direction.Up;
            if (angle > 135f || angle <= -135f) return Direction.Left;
            if (angle > -135f && angle <= -45f) return Direction.Down;

            return Direction.None;
        }
    }
}
