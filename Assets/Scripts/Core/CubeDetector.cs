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
            if (elapsedTime > 15f) //임시 판정 타이밍, 나중에 speed라든가 값이 지면 수정해야 될 것 같습니다
            {
                elapsedTime = 0f;
                if(!Determine())
                {
                    OnGameOver?.Invoke();
                }
                OnKeepGoing?.Invoke();
            }
        }

        private bool Determine()
        {
            RaycastHit[] hits;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 curPos = new Vector3(-11f, row[i], col[j]);
                    hits = Physics.RaycastAll(curPos, Vector3.right, 1.0f);

                    if (hits.Length > 1) return false;
                }
            }
            return true;
        }
    }
}
