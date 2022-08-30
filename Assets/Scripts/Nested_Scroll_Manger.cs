using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Nested_Scroll_Manger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;

    const int SIZE = 4;
    float[] pos = new float[SIZE];
    float distance, curPos, targetPos;
    bool isDrag;
    int targetIndex;

    void Start()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }
    float SetPos()
    {
        //절반거리를 기준으로 가까운 위치를 반환
        for (int i = 0; i < SIZE; i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        return 0;
    }
    public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();

    public void OnDrag(PointerEventData eventData) => isDrag = true;
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        targetPos = SetPos();
        
        if (curPos == targetPos)
        {
            print(eventData.delta.x);
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            else if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
    }

    void Update()
    {
        if(!isDrag) scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }
}
