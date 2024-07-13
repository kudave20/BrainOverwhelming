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

        private readonly Vector3 destination = new Vector3(-10, 0, 0);
        private readonly Vector3 initCubePos = Vector3.zero;
        private float elapsedTime = 0;

        private bool isRotating = false;

        private void Update()
        {
            // elapsedTime += Time.deltaTime;
        }

        /// <summary>
        /// 큐브 이동
        /// </summary>
        public IEnumerator TranslateCube()
        {
            /*
            while (transform.position.x > -10f)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(initCubePos, destination, elapsedTime * moveSpeed / 10f);
                yield return null;
            }
            */

            yield return new WaitForSeconds(100f);
        }

        public IEnumerator RotateCube(Direction direction)
        {
            if (isRotating) yield break;

            isRotating = true;
            elapsedTime = 0;

            Vector3 towardDirection = direction switch
            {
                Direction.Right => Vector3.down,
                Direction.Up => Vector3.forward,
                Direction.Left => Vector3.up,
                Direction.Down => Vector3.back,
                Direction.Clock => Vector3.right,
                Direction.CounterClock => Vector3.left,
                Direction.None => Vector3.zero,
                _ => Vector3.zero
            };

            Quaternion fromRotation = transform.rotation;

            while (elapsedTime * rotateSpeed < 1f)
            {
                elapsedTime += Time.deltaTime;

                transform.Rotate(towardDirection * 90f * Time.deltaTime * rotateSpeed, Space.World);

                yield return null;
            }

            transform.rotation = fromRotation * Quaternion.Inverse(fromRotation) * Quaternion.Euler(towardDirection * 90f) * fromRotation;

            isRotating = false;
        }
    }
}
