using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class CubeController : MonoBehaviour
    {
        private int speed = 2;
        private readonly Vector3 destination = new Vector3(-10, 0, 0);
        private readonly Vector3 initCubePos = Vector3.zero;
        private float elapsedTime;

        private void Update()
        {
            //elapsedTime += Time.deltaTime;
        }

        /// <summary>
        /// 큐브 이동
        /// </summary>
        public IEnumerator TranslateCube()
        {
            while(transform.position.x >= -10f)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(initCubePos, destination, elapsedTime * speed / 10);
                yield return null;
            }
        }
    }
}
