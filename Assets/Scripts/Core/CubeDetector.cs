using System;
using UnityEngine;

namespace Brain.Core
{
    public class CubeDetector : MonoBehaviour
    {
        private int[] row = { -1, 0, 1 };
        private int[] col = { 1, 0, -1 };

        /// <summary>
        /// 큐브 판정
        /// </summary>
        
        public bool DetectCube()
        {
            RaycastHit[] hits;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 curPos = new Vector3(-11f, row[i], col[j]);
                    hits = Physics.RaycastAll(curPos, Vector3.right * 1.5f, 1.5f);

                    if (hits.Length != 1) return false;
                }
            }
            return true;
        }
    }
}
