using UnityEngine;

namespace Brain.Core
{
    public class CubeDetector : MonoBehaviour
    {
        private bool[,] planeInfos = null;

        private int sideLength = 0;

        public void Init(bool[,] planeInfos, int sideLength)
        {
            this.planeInfos = planeInfos;
            this.sideLength = sideLength;
        }

        /// <summary>
        /// 큐브 판정
        /// </summary>
        public bool DetectCube()
        {
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    Vector3 childPosition = transform.GetChild(i * sideLength + j).position;
                    bool isHit = Physics.Raycast(childPosition, Vector3.back, 3f);

                    if (planeInfos[i, j] && !isHit)
                    {
                        return false;
                    }
                    if (!planeInfos[i, j] && isHit)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
