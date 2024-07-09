using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class InputReceiver : MonoBehaviour
    {
        //private GameObject cubeToRotate = null;

        /*public void Init(GameObject cube)
        {
            cubeToRotate = cube;
        }

        public void Clear()
        {
            cubeToRotate = null;
        }*/

        private void Update()
        {
            // 입력에 맞는 동작
            switch (Input.inputString) //카메라 위치에 따라 축 방향 수정 필요, CAPS LOCK을 켜지 않으면 인식 불가
            {
                case "A":
                    transform.Rotate(new Vector3(-90, 0, 0));
                    break;
                case "D":
                    transform.Rotate(new Vector3(90, 0, 0));
                    break;
                case "S":
                    transform.Rotate(new Vector3(0, -90, 0));
                    break;
                case "W":
                    transform.Rotate(new Vector3(0, 90, 0));
                    break;
                case "Q":
                    transform.Rotate(new Vector3(0, 0, -90));
                    break;
                case "E":
                    transform.Rotate(new Vector3(0, 0, 90));
                    break;

            }

        }
    }
}
