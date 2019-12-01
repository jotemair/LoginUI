using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadialInventory
{

    public class Inventory : MonoBehaviour
    {
        #region Variables

        public List<Item> inv = new List<Item>();

        [Header("Main UI")]
        public bool showSelectMenu;
        public bool toggleToggleable;
        public float scrW, scrH;

        [Header("Resources")]
        public Texture2D radialTexture;
        public Texture2D slotTexture;
        [Range(0, 100)]
        public int circleScaleOffset;

        [Header("Icons")]
        public Vector2 iconSize;
        public bool showIcons, showBoxes, showBounds;

        [Range(0.1f, 1f)]
        public float iconSizeNum;

        [Range(-360, 360)]
        public int radialRotation;

        [SerializeField]
        private float iconOffset;

        [Header("Mouse Settings")]
        public Vector2 mouse;
        public Vector2 input;
        private Vector2 circleCenter;

        [Header("Input Settings")]
        public float inputDist;
        public float inputAngle;
        public int keyIndex;
        public int mouseIndex;
        public int inputIndex;

        [Header("Sector Settings")]
        public Vector2[] slotPos;
        public Vector2[] boundPos;

        [Range(1, 8)]
        public int numOfSectors = 1;

        [Range(50, 300)]
        public float circleRadius;

        public float mouseDistance, sectorDegree, mouseAngles;
        public int sectorIndex = 0;
        public bool withinCircle;

        [Header("Misc")]
        public Rect debugWindow;

        #endregion

        #region Setup Functions

        private Vector2 Scr(float x, float y)
        {
            return new Vector2(scrW * x, scrH * y);
        }

        private Vector2[] BoundPositions(int slots)
        {
            Vector2[] l_boundPos = new Vector2[slots];
            float angle = 0f + radialRotation;
            for (int i = 0; i < l_boundPos.Length; ++i)
            {
                l_boundPos[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
                l_boundPos[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
                angle += sectorDegree;
            }

            return l_boundPos;
        }

        private Vector2[] SlotPositions(int slots)
        {
            Vector2[] l_slotPositions = new Vector2[slots];
            // Numbers are placeholder
            float angle = ((iconOffset / 2f) * 2f) + radialRotation;
            for (int i = 0; i < l_slotPositions.Length; ++i)
            {
                l_slotPositions[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
                l_slotPositions[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
                angle += sectorDegree;
            }

            return l_slotPositions;
        }

        private void SetItemSlots(int slots, Vector2[] pos)
        {
            for (int i = 0; i < slots; ++i)
            {
                Texture2D texture = (inv.Count > i) ? inv[i].Icon.texture : slotTexture;
                GUI.DrawTexture(new Rect(
                                    pos[i].x - (scrW * iconSizeNum * 0.5f),
                                    pos[i].y - (scrH * iconSizeNum * 0.5f),
                                    scrW * iconSizeNum,
                                    scrH * iconSizeNum
                                ),
                                texture
                            );
            }
        }

        private int CheckCurrentSector(float angle)
        {
            float boundingAngle = 0f;

            for (int i = 0; i < numOfSectors; ++i)
            {
                boundingAngle += sectorDegree;
                if (angle < boundingAngle)
                {
                    return i;
                }
            }

            return 0;
        }

        private void CalculateMouseAngles()
        {
            mouse = Input.mousePosition;

            input.x = Input.GetAxis("Horizontal");
            input.y = -Input.GetAxis("Vertical");

            mouseDistance = Mathf.Sqrt(Mathf.Pow(mouse.x - circleCenter.x, 2) + Mathf.Pow(mouse.y - circleCenter.y, 2));

            inputDist = input.magnitude;

            withinCircle = (mouseDistance <= circleRadius);

            if (input.x != 0f || input.y != 0f)
            {
                inputAngle = (Mathf.Atan2(-input.y, input.x) * 180f / Mathf.PI) + radialRotation;
            }
            else
            {
                mouseAngles = (Mathf.Atan2(mouse.y - circleCenter.y, mouse.x - circleCenter.x) * 180f / Mathf.PI) + radialRotation;
            }

            inputAngle += (inputAngle < 0f) ? 360f : 0f;
            mouseAngles += (mouseAngles < 0f) ? 360f : 0f;

            inputIndex = CheckCurrentSector(inputAngle);
            mouseIndex = CheckCurrentSector(mouseAngles);

            if (input.x != 0f || input.y != 0f)
            {
                sectorIndex = inputIndex;
            }
            else
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    sectorIndex = mouseIndex;
                }
            }

            sectorIndex = numOfSectors - sectorIndex - 1;
        }

        #endregion

        #region Unity Functions

        private void Start()
        {
            for (int i = 0; i < 4; ++i)
            {
                inv.Add(ItemData.CreateItem(i));
            }
            inv.Add(ItemData.CreateItem(402));
            inv.Add(ItemData.CreateItem(502));
            inv.Add(ItemData.CreateItem(401));
            inv.Add(ItemData.CreateItem(400));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;

                circleCenter.x = Screen.width / 2;
                circleCenter.y = Screen.height / 2;

                showSelectMenu = true;
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                showSelectMenu = false;
            }
        }

        private void OnGUI()
        {
            if (showSelectMenu)
            {
                CalculateMouseAngles();
                sectorDegree = 360f / numOfSectors;

                iconOffset = sectorDegree / 2f;
                slotPos = SlotPositions(numOfSectors);
                boundPos = BoundPositions(numOfSectors);

                // DeadZone
                GUI.Box(new Rect(Scr(7.5f, 4f), Scr(1f ,1f)),"");

                // Circle
                GUI.DrawTexture(new Rect(
                                        (circleCenter.x - circleRadius) - (circleScaleOffset / 4f),
                                        (circleCenter.y - circleRadius) - (circleScaleOffset / 4f),
                                        (circleRadius * 2f) + (circleScaleOffset / 2f),
                                        (circleRadius * 2f) + (circleScaleOffset / 2f)
                                    ),
                                    radialTexture
                                );

                if (showBoxes)
                {
                    for (int i = 0; i < numOfSectors; ++i)
                    {
                        GUI.DrawTexture(new Rect(
                                        slotPos[i].x - (scrW * iconSizeNum * 0.5f),
                                        slotPos[i].y - (scrH * iconSizeNum * 0.5f),
                                        scrW * iconSizeNum,
                                        scrH * iconSizeNum
                                    ),
                                    slotTexture
                                );
                    }
                }

                if (showBounds)
                {
                    for (int i = 0; i < numOfSectors; ++i)
                    {
                        GUI.Box(new Rect(
                                        boundPos[i].x - (scrW * 0.1f * 0.5f),
                                        boundPos[i].y - (scrH * 0.1f * 0.5f),
                                        scrW * 0.1f,
                                        scrH * 0.1f
                                    ),
                                    ""
                                );
                    }
                }

                if (showIcons)
                {
                    SetItemSlots(numOfSectors, slotPos);
                }
            }
        }

        #endregion
    }

}
