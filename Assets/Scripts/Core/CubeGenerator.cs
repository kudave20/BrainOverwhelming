using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab = null;

        private InputReceiver inputReceiver = null;

        public void Init(InputReceiver inputReceiver)
        {
            this.inputReceiver = inputReceiver;
        }

        /// <summary>
        /// 큐브 생성
        /// </summary>
        public void GenerateCube()
        {
            // 큐브 생성

            // inputReceiver.Init();
        }

        /// <summary>
        /// 큐브 제거
        /// </summary>
        public void ClearCube()
        {
            // 큐브 제거

            // inputReceiver.Clear();
        }
    }
}
