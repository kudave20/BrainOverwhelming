using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class InputReceiver : MonoBehaviour
    {
        private GameObject cubeToRotate = null;

        public void Init(GameObject cube)
        {
            cubeToRotate = cube;
        }

        public void Clear()
        {
            cubeToRotate = null;
        }

        private void Update()
        {
            // 입력에 맞는 동작
        }
    }
}
