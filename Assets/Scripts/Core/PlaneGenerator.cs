using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class PlaneGenerator : MonoBehaviour
    {
        /// <summary>
        /// 판 생성
        /// </summary>

        [SerializeField]
        private GameObject cubePrefab;
        [SerializeField]
        private GameObject planePrefab;

        private GameObject plane;
        private int[] row = { -1, 0, 1 };
        private int[] col = { 1, 0, -1 };
        private bool[] isHit = new bool[9];

        public void Start()
        {
            //GeneratePlane();
        }

        public void GeneratePlane()
        {
            // 판 생성

            plane = Instantiate(planePrefab);

            ChooseFace();

            MakePlane();

            RotatePlane();

            plane.transform.position = new Vector3(-10.0f, 0, 0);

        }

        private void ChooseFace()
        {
            int face = Random.Range(0, 6);

            switch(face)
            {
                case 0: //x face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(2, row[i], col[j]), Vector3.left, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 1: //-x face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(-2, row[i], col[j]), Vector3.right, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 2: //y face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(row[i], 2, col[j]), Vector3.down, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 3: //-y face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(row[i], -2, col[j]), Vector3.up, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 4: //z face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(col[j], row[i], 2), Vector3.back, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 5: //-z face
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(col[j], row[i], -2), Vector3.forward, 1.0f)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
            }
        }

        private void MakePlane()
        {
            for (int i = 0; i < 9; i++)
            {
                if (isHit[i])
                {
                    plane.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        private void RotatePlane()
        {
            int angle = Random.Range(0, 4);

            switch(angle)
            {
                case 0: //degree 0
                    break;
                case 1: //degree 90
                    plane.transform.Rotate(new Vector3(90.0f, 0, 0));
                    break;
                case 2: //degree 180
                    plane.transform.Rotate(new Vector3(180.0f, 0, 0));
                    break;
                case 3: //degree 270
                    plane.transform.Rotate(new Vector3(270.0f, 0, 0));
                    break;
            }    
        }

        public void ClearPlane()
        {
            plane.SetActive(false);
        }
    }
}
