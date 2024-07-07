using Brain.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.GameFlow
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CubeGenerator cubeGenerator = null;
        [SerializeField] private PlaneGenerator planeGenerator = null;
        [SerializeField] private CubeDetector cubeDetector = null;
        [SerializeField] private InputReceiver inputReceiver = null;

        private CubeController cubeController = null;

        private bool isGameEnded = false;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            // 각종 초기화
            cubeController = cubeGenerator.Init(inputReceiver).GetComponent<CubeController>();

            StartCoroutine(GameFlow());
        }

        /// <summary>
        /// 게임 진행
        /// </summary>
        private IEnumerator GameFlow()
        {
            while (!isGameEnded)
            {
                // 큐브 생성
                cubeGenerator.GenerateCube();

                //cube = GameObject.FindWithTag("cube");
                //cubeController = cube.GetComponent<CubeController>();

                // 판 생성
                planeGenerator.GeneratePlane();

                // 큐브 움직임
                yield return cubeController.TranslateCube();

                // 판정
                if (cubeDetector.DetectCube())
                {
                    // 재시작
                    cubeGenerator.ClearCube();
                }
                else
                {
                    // 게임 오버
                    isGameEnded = true;
                }
            }
        }
    }
}
