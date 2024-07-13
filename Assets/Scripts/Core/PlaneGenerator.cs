using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Brain.Core
{
    public class PlaneGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject planePrefab = null;

        private List<Vector3Int> occupiedPositions = null;
        private int sideLength = 3;

        private bool[,] planeInfos = new bool[5, 5];

        private GameObject plane = null;

        public void Init(List<Vector3Int> occupiedPositions, int sideLength)
        {
            this.occupiedPositions = occupiedPositions;
            this.sideLength = sideLength;
        }

        /// <summary>
        /// 판 생성
        /// </summary>
        public void GeneratePlane()
        {
            plane = Instantiate(planePrefab);

            ChooseFace();

            MakePlane();

            RotatePlane();

            plane.transform.position = new Vector3(0, 0, 10f);
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
            plane.SetActive(false);
        }
    }
}
