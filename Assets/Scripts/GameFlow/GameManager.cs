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
        [SerializeField] private CubeController cubeController = null;
        [SerializeField] private CubeDetector cubeDetector = null;
        [SerializeField] private InputReceiver inputReceiver = null;

        private bool isGameEnded = false;

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            // 각종 초기화
            cubeGenerator.Init(inputReceiver);

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

                // 판 생성
                planeGenerator.GeneratePlane();

                // 큐브 움직임
                yield return cubeController.TranslateCube();

                // 판정
                if (cubeDetector.DetectCube())
                {
                    // 재시작
                    Debug.Log("Stage Clear!");
                    cubeGenerator.ClearCube();
                }
                else
                {
                    // 게임 오버
                    Debug.Log("Game Over!");
                    isGameEnded = true;
                }

                //cubeDetector.DetectCube();
            }
        }
    }
}
