using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class PlaneGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject planePrefab = null;

        private List<Vector3Int> occupiedPositions = null;
        private int sideLength = 3;

        private bool[,] planeInfos = new bool[5, 5];

        private CubeDetector cubeDetector = null;

        private GameObject rootCube = null;
        private GameObject plane = null;

        public CubeDetector Init(GameObject rootCube, int sideLength)
        {
            this.rootCube = rootCube;
            this.sideLength = sideLength;

            plane = Instantiate(planePrefab);
            plane.transform.position = new Vector3(0, 0, 10f);

            cubeDetector = plane.GetComponent<CubeDetector>();

            return cubeDetector;
        }

        public void Setup(List<Vector3Int> occupiedPositions)
        {
            this.occupiedPositions = occupiedPositions;
        }

        /// <summary>
        /// 판 생성
        /// </summary>
        public void GeneratePlane()
        {
            ChooseFace();
            MakePlane();
            RotatePlane();

            cubeDetector.Init(planeInfos, sideLength);
        }

        private void ChooseFace()
        {
            int face = Random.Range(0, 3);

            switch (face)
            {
                case 0: // 전후
                    foreach (var position in occupiedPositions)
                    {
                        planeInfos[position.x, position.y] = true;
                    }
                    break;
                case 1: // 상하
                    foreach (var position in occupiedPositions)
                    {
                        planeInfos[position.x, position.z] = true;
                    }
                    break;
                case 2: // 좌우
                    foreach(var position in occupiedPositions)
                    {
                        planeInfos[position.z, position.y] = true;
                    }
                    break;
            }
        }

        private void MakePlane()
        {
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    if (planeInfos[i, j])
                    {
                        plane.transform.GetChild(i * sideLength + j).gameObject.SetActive(false);
                    }
                }
            }
        }

        private void RotatePlane()
        {
            int angle = Random.Range(0, 4);

            switch(angle)
            {
                case 0: // 0도
                    break;
                case 1: // 90도
                    plane.transform.Rotate(new Vector3(0, 0, 90f));
                    break;
                case 2: // 180도
                    plane.transform.Rotate(new Vector3(0, 0, 180f));
                    break;
                case 3: // 270도
                    plane.transform.Rotate(new Vector3(0, 0, 270f));
                    break;
            }    
        }

        public void ClearPlane()
        {
            plane.transform.rotation = Quaternion.identity;

            System.Array.Clear(planeInfos, 0, planeInfos.Length);

            for (int i = 0; i < sideLength * sideLength; i++)
            {
                plane.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
