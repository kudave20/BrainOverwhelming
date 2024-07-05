using Brain.GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Brain.Core
{
    public class CubeDetector : MonoBehaviour
    {
        public float elapsedTime;
        public bool collisionHappened = false;

        private int[] row = { -1, 0, 1 };
        private int[] col = { 1, 0, -1 };

        public UnityEvent OnGameOver, OnKeepGoing;

        /// <summary>
        /// 큐브 판정
        /// </summary>
        private void Update()
        {
            elapsedTime += Time.deltaTime;
        }

        public void DetectCube()
        {
            if (elapsedTime > 15f)
            {
                elapsedTime = 0f;
                if(!Determine())
                {
                    OnGameOver?.Invoke();
                }
                OnKeepGoing?.Invoke();
            }

            if (collisionHappened)
            {
                OnGameOver?.Invoke();
            }
        }

        private bool Determine()
        {
            RaycastHit hit;
            bool ret = false;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 curPos = new Vector3(-11f, row[i], col[j]);
                    ret = Physics.Raycast(curPos, Vector3.right, out hit, 10f);
                    if (!ret) return ret;
                }
            }
            return ret;
        }
    }
}
