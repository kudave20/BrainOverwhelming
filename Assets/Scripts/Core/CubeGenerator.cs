
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab = null;

        private InputReceiver inputReceiver = null;

        private GameObject cubeInstance;
        private Transform cubeParentTransform;
        private int cubeLimit;  //가능한 총 큐브 개수 = 전체 부피
        private int cubeCount;  //생성할 큐브 개수
        private int sideLength; //한 변 길이
        private int sideArea;   //한 면 넓이
        private List<Vector3Int> occupiedPostions = new List<Vector3Int>();
        private bool[,,] boolOccupiedPositions = new bool[3, 3, 3];
        private List<Vector3Int> possiblePositions = new List<Vector3Int>();
        private int[] dx = { 1, 0, 0, -1, 0, 0 };
        private int[] dy = { 0, 1, 0, 0, -1, 0 };
        private int[] dz = { 0, 0, 1, 0, 0, -1 };

        public GameObject Init(InputReceiver inputReceiver)
        {
            this.inputReceiver = inputReceiver;
            cubeInstance = Instantiate(cubePrefab);
            sideLength = 3;
            sideArea = sideLength * sideLength;
            cubeLimit = sideLength * sideArea;
            cubeParentTransform = cubeInstance.transform;
            ClearCube();

            return cubeInstance;
        }

        /// <summary>
        /// 큐브 생성
        /// </summary>
        public void GenerateCube()
        {
            cubeCount = Random.Range(3, cubeLimit - 1);
            int startCube = Random.Range(0, cubeLimit);
            int yz = startCube % sideArea;
            Vector3Int firstCubePos = new Vector3Int(startCube / sideArea, yz / sideLength, yz % sideLength);
            UpdatePosition(firstCubePos);
            cubeParentTransform.GetChild(startCube).gameObject.SetActive(true);
            int nextCubeIndex;
            int nextCube;
            while(cubeCount-- != 0)
            {
                nextCubeIndex = Random.Range(0, possiblePositions.Count);
                UpdatePosition(possiblePositions[nextCubeIndex]);
                nextCube = possiblePositions[nextCubeIndex].x * sideArea + possiblePositions[nextCubeIndex].y * sideLength + possiblePositions[nextCubeIndex].z;
                cubeParentTransform.GetChild(nextCube).gameObject.SetActive(true);
                possiblePositions.RemoveAt(nextCubeIndex);
            }

            // inputReceiver.Init();
        }

        private void UpdatePosition(Vector3Int pos)
        {
            occupiedPostions.Add(pos);
            boolOccupiedPositions[pos.x, pos.y, pos.z] = true;
            int newX, newY, newZ;
            Vector3Int newPos;

            for(int i = 0; i < 6; ++i)
            {
                newX = pos.x + dx[i];
                newY = pos.y + dy[i];
                newZ = pos.z + dz[i];
                if(newX < 0 || newY < 0 || newZ < 0 || newX >= sideLength || newY >= sideLength || newZ >= sideLength)
                {
                    continue;
                }
                newPos = new Vector3Int(newX, newY, newZ);
                if(boolOccupiedPositions[newPos.x, newPos.y, newPos.z])
                {
                    continue;
                }
                possiblePositions.Add(newPos);
            }
        }

        /// <summary>
        /// 큐브 제거
        /// </summary>
        public void ClearCube()
        {
            // 큐브 제거
            cubeParentTransform = cubeInstance.transform;
            foreach(Transform child in cubeParentTransform)
            {
                child.gameObject.SetActive(false);
            }
            occupiedPostions.Clear();
            possiblePositions.Clear();
            //System.Array.Clear(possiblePositions, 0, boolOccupiedPositions.Length);

            // inputReceiver.Clear();
        }
    }
}
