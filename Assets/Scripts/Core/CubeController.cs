using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
        Clock,
        CounterClock
    }

    public class CubeController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotateSpeed = 3f;

        private Vector3 startPosition;
        private Vector3 destination = new Vector3(0, 0, 10f);

        private float moveTime = 0;
        private float rotateTime = 0;

        private bool isRotating = false;

        public void Init()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            // elapsedTime += Time.deltaTime;
        }

        /// <summary>
        /// 큐브 이동
        /// </summary>
        public IEnumerator TranslateCube()
        {
            while (transform.position.z > -10f)
            {
                moveTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, destination, moveTime * moveSpeed * 0.1f);
                yield return null;
            }
        }

        public IEnumerator RotateCube(Direction direction)
        {
            if (isRotating) yield break;

            isRotating = true;
            rotateTime = 0;

            Vector3 towardDirection = direction switch
            {
                Direction.Right => Vector3.down,
                Direction.Up => Vector3.right,
                Direction.Left => Vector3.up,
                Direction.Down => Vector3.left,
                Direction.Clock => Vector3.back,
                Direction.CounterClock => Vector3.forward,
                Direction.None => Vector3.zero,
                _ => Vector3.zero
            };

            Quaternion fromRotation = transform.rotation;

            while (rotateTime * rotateSpeed < 1f)
            {
                rotateTime += Time.deltaTime;

                transform.Rotate(towardDirection * 90f * Time.deltaTime * rotateSpeed, Space.World);

                yield return null;
            }

            transform.rotation = fromRotation * Quaternion.Inverse(fromRotation) * Quaternion.Euler(towardDirection * 90f) * fromRotation;

            isRotating = false;
        }
    }
}
