using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;                                                                                  //ARRaycastManager�� ���� ���ؼ�
using UnityEngine.XR.ARSubsystems;                                                                                  //trackableType�� ���� ���ؼ�
using UnityEngine.EventSystems;                                                                                     //��ư�� �������� �̺�Ʈ�� �߻���Ű�� ���ؼ�
using UnityEngine.UI;                                                                                               //��ư�� ����� ���ؼ�

public class ARPlaceONPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;                                                                            //ARRaycastManager�� arRaycaster�� �Ҵ�(instance)
    public GameObject Scene_Start, Scene_Home, Scene_Menu, Scene_Plating, Scene_Order, Scene_Ready;                              //�� ĵ������ ������Ʈ�� ����
    public GameObject LittleUI;                                                                                     //���� ĵ������ LittleUI�� ����
    public Text food_number;                                                                                        //�� ������ ������ ����(�ִ� 4��)
    public Text food_number1, food_number2, food_number3, food_number4,
                food_number5, food_number6, food_number7, food_number8, food_number9;

    public GameObject Tutorial;
    int tutorial_number = 1;

    public GameObject Menu_1, Menu_2, Menu_3, Menu_4,Menu_5,Menu_6,
                      Menu_7, Menu_8, Menu_9;                                                 //������ Prefab�� ����
    GameObject Menu11, Menu12, Menu13, Menu14;                                                                      //������ ������ ����        
    GameObject Menu21, Menu22, Menu23, Menu24;                                                                      //������ ������ ����
    GameObject Menu31, Menu32, Menu33, Menu34;                                                                      //������ ������ ����
    GameObject Menu41, Menu42, Menu43, Menu44;                                                                      //������ ������ ����
    GameObject Menu51, Menu52, Menu53, Menu54;                                                                      //������ ������ ����
    GameObject Menu61, Menu62, Menu63, Menu64;                                                                      //������ ������ ����
    GameObject Menu71, Menu72, Menu73, Menu74;
    GameObject Menu81, Menu82, Menu83, Menu84;
    GameObject Menu91, Menu92, Menu93, Menu94;

    static GameObject littleUI_position;
    public GameObject littleUI_position1, littleUI_position2, littleUI_position3, littleUI_position4,
                        littleUI_position5, littleUI_position6, littleUI_position7, littleUI_position8,
                        littleUI_position9;

    public GameObject Order_Scroll;
    public GameObject Order_1, Order_2, Order_3, Order_4, Order_5, Order_6,
                      Order_7, Order_8, Order_9;
    public Text O1, O2, O3, O4, O5, O6, O7, O8, O9;

    public GameObject Menu_Scroll;
    public GameObject MenuP1, MenuP2, MenuP3, MenuP4, MenuP5, MenuP6,
                      MenuP7, MenuP8, MenuP9;

    public Text TotalPrice;
    

    int button = 0;                                                                                                 //button�̶�� ���� ���� �� 0���� �ʱ�ȭ (���� ���п� ���)
    int count = 0;                                                                                                  //UI�� ��ư�� �и������ֱ� ���� ����
    int menu = 0;                                                                                                   //������ ���° ������Ʈ�� ��ġ������ ���
    int food = 0, food1 = 0, food2 = 0, food3 = 0, food4 = 0,                                                       //����text�� ��Ÿ���µ� ���        
        food5 = 0, food6 = 0, food7 = 0, food8 = 0, food9;
    int menu1_count = 0, menu2_count = 0, menu3_count = 0,                                                                                            //menu1�� ����
        menu4_count = 0, menu5_count = 0, menu6_count = 0,
        menu7_count = 0, menu8_count = 0, menu9_count = 0;                                                                                    

    
    void Start()                                                                                                    //��ũ��Ʈ�� �����Ҷ� 1ȸ ����Ǵ� �Լ�
    {
        Scene_Order.SetActive(true);
        Scene_Order.SetActive(false);
        Scene_Start.SetActive(true);                                                                                                            //Homeȭ�鿡�� ����    
        LittleUI.SetActive(false);                                                                                  //LittleUI ����
        LittleUI.transform.GetChild(0).gameObject.SetActive(false);
        LittleUI.transform.GetChild(1).gameObject.SetActive(false);
        LittleUI.transform.GetChild(2).gameObject.SetActive(false);
        LittleUI.transform.GetChild(3).gameObject.SetActive(false);
        MenuP1.SetActive(false);
        MenuP2.SetActive(false);
        MenuP3.SetActive(false);
        MenuP4.SetActive(false);
        MenuP5.SetActive(false);
        MenuP6.SetActive(false);
    }


    void Update()                                                                                                   //��ũ��Ʈ�� ����Ǵ� ���� �� �����Ӹ��� �����Ű�� �Լ�
    {
        PlaceObjectByTouch();                                                                                       //PlaceObjectByTouch��� �Լ� ����
        Order_List();
        calculator();
    }

    private void PlaceObjectByTouch()                                                                               //PlaceObjectByTouch��� �Լ� ����
    {
        if (Input.touchCount > 0)                                                                                   //��ġ�� �̷������
        {

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))                            //UI��ư�� ��ġ�� ����
                return;
            else
            {
                Touch touch = Input.GetTouch(0);                                                                    //����� ��ġ ȭ�鿡 ������ �հ��� ������� Touch ����ü ��ȯ
                                                                                                                    //ù��° ��ġ ������ touch�� ����
                List<ARRaycastHit> hits = new List<ARRaycastHit>();                                                 //ARRaycastHit��� �ڷ����� hits����Ʈ ����
                if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))                                //Raycast Manager���� Raycast�Լ��� ����ϸ� ���ڰ�����
                {                                                                                                   //1. ��� �������� ����� screenpoint ����
                                                                                                                    //2. hitResults�� Ray�� ���� �浹�Ǵ� ��ü���� List ���·� ��ȯ
                                                                                                                    //3. �Ÿ���� ������Ʈ�鸸 ��� �Ұ���
                                                                                                                    //--> Planes�̶�� ��ü�� Ray�� ������ �� ������� ��ȯ�ްڴٴ� �ǹ�
                    if (count == 0)                                                                                 //Prefab ��ư�� ������ ������
                    {
                        Pose hitPose = hits[0].pose;                                                                //ù��° hit�� �Ͼ ��ü�� position���� hitPose�� ����

                        switch (menu)
                        {
                            case 11:                                                                                //menu11 ��ư�� ������
                            
                                if (!Menu11)
                                {
                                    Menu11 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu11.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu11.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 12:                                                                                //menu12 ��ư�� ������
                            
                                if (!Menu12)
                                {
                                    Menu12 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu12.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu12.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 13:                                                                                //menu13 ��ư�� ������
                            
                                if (!Menu13)
                                {
                                    Menu13 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu13.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu13.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 14:                                                                                //menu14 ��ư�� ������
                            
                                if (!Menu14)
                                {
                                    Menu14 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu14.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu14.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 21:                                                                                //menu21 ��ư�� ������
                            
                                if (!Menu21)
                                {
                                    Menu21 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu21.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu21.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 22:                                                                                //menu22 ��ư�� ������
                            
                                if (!Menu22)
                                {
                                    Menu22 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu22.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu22.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 23:                                                                                //menu23 ��ư�� ������
                            
                                if (!Menu23)
                                {
                                    Menu23 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu23.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu23.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            
                            case 24:                                                                                //menu24 ��ư�� ������
                            
                                if (!Menu24)
                                {
                                    Menu24 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                    //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu24.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu24.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            case 31:                                                                                //menu11 ��ư�� ������

                                if (!Menu31)
                                {
                                    Menu31 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu31.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu31.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 32:                                                                                //menu12 ��ư�� ������

                                if (!Menu32)
                                {
                                    Menu32 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu32.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu32.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 33:                                                                                //menu13 ��ư�� ������

                                if (!Menu33)
                                {
                                    Menu33 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu33.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu33.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 34:                                                                                //menu14 ��ư�� ������

                                if (!Menu34)
                                {
                                    Menu34 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu34.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu34.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 41:                                                                                //menu21 ��ư�� ������

                                if (!Menu41)
                                {
                                    Menu41 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu41.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu41.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 42:                                                                                //menu22 ��ư�� ������

                                if (!Menu42)
                                {
                                    Menu42 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu42.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu42.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 43:                                                                                //menu23 ��ư�� ������

                                if (!Menu43)
                                {
                                    Menu43 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu43.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu43.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 44:                                                                                //menu24 ��ư�� ������

                                if (!Menu44)
                                {
                                    Menu44 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu44.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu44.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            case 51:                                                                                //menu11 ��ư�� ������

                                if (!Menu51)
                                {
                                    Menu51 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu51.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu51.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 52:                                                                                //menu12 ��ư�� ������

                                if (!Menu52)
                                {
                                    Menu52 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu52.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu52.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 53:                                                                                //menu13 ��ư�� ������

                                if (!Menu53)
                                {
                                    Menu53 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu53.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu53.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 54:                                                                                //menu14 ��ư�� ������

                                if (!Menu54)
                                {
                                    Menu54 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu54.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu54.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 61:                                                                                //menu21 ��ư�� ������

                                if (!Menu61)
                                {
                                    Menu61 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu61.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu61.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 62:                                                                                //menu22 ��ư�� ������

                                if (!Menu62)
                                {
                                    Menu62 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu62.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu62.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 63:                                                                                //menu23 ��ư�� ������

                                if (!Menu63)
                                {
                                    Menu63 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu63.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu63.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 64:                                                                                //menu24 ��ư�� ������

                                if (!Menu64)
                                {
                                    Menu64 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu64.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu64.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                            case 71:                                                                                //menu21 ��ư�� ������

                                if (!Menu71)
                                {
                                    Menu71 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu71.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu71.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 72:                                                                                //menu22 ��ư�� ������

                                if (!Menu72)
                                {
                                    Menu72 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu72.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu72.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 73:                                                                                //menu23 ��ư�� ������

                                if (!Menu73)
                                {
                                    Menu73 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu73.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu73.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 74:                                                                                //menu24 ��ư�� ������

                                if (!Menu74)
                                {
                                    Menu74 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu74.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu74.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 81:                                                                                //menu21 ��ư�� ������

                                if (!Menu81)
                                {
                                    Menu81 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu81.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu81.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 82:                                                                                //menu22 ��ư�� ������

                                if (!Menu82)
                                {
                                    Menu82 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu82.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu82.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 83:                                                                                //menu23 ��ư�� ������

                                if (!Menu83)
                                {
                                    Menu83 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu83.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu83.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 84:                                                                                //menu24 ��ư�� ������

                                if (!Menu84)
                                {
                                    Menu84 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu84.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu84.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 91:                                                                                //menu21 ��ư�� ������

                                if (!Menu91)
                                {
                                    Menu91 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu91.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu91.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 92:                                                                                //menu22 ��ư�� ������

                                if (!Menu92)
                                {
                                    Menu92 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu92.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu92.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 93:                                                                                //menu23 ��ư�� ������

                                if (!Menu93)
                                {
                                    Menu93 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu93.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu93.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;

                            case 94:                                                                                //menu24 ��ư�� ������

                                if (!Menu94)
                                {
                                    Menu94 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //��ġ�� �ɶ����� ���� ������Ʈ�� �ν��Ͻ�ȭ(ȭ�鿡 ��üȭ)
                                                                                                                        //��ġ�� �ɶ����� hit�� ������ ���� ������Ʈ�� ����	
                                }

                                else                                                                                    //spawnObject�� ȭ��� �����ϸ�             
                                {
                                    Menu94.transform.position = hitPose.position;                                       //GameObject�� ��ġ�� ����
                                    Menu94.transform.rotation = hitPose.rotation;                                       //GameObject�� ������ ����
                                }
                                break;
                        }

                    }
                    count = 0;                                                                                      //��ư �ʱ�ȭ
                }

            }

        }
    }
      

    public void Clear()                                                                                             //Clear()��� ��ư �̺�Ʈ �Լ� ����
    {
        Destroy(Menu11, 0.0f);                                                                                      //��� ������ ������Ʈ�� �ı�
        Destroy(Menu12, 0.0f);
        Destroy(Menu13, 0.0f);
        Destroy(Menu14, 0.0f);
        Destroy(Menu21, 0.0f);
        Destroy(Menu22, 0.0f);
        Destroy(Menu23, 0.0f);
        Destroy(Menu24, 0.0f);
        Destroy(Menu31, 0.0f);                                                                                      //��� ������ ������Ʈ�� �ı�
        Destroy(Menu32, 0.0f);
        Destroy(Menu33, 0.0f);
        Destroy(Menu34, 0.0f);
        Destroy(Menu41, 0.0f);
        Destroy(Menu42, 0.0f);
        Destroy(Menu43, 0.0f);
        Destroy(Menu44, 0.0f);
        Destroy(Menu51, 0.0f);                                                                                      //��� ������ ������Ʈ�� �ı�
        Destroy(Menu52, 0.0f);
        Destroy(Menu53, 0.0f);
        Destroy(Menu54, 0.0f);
        Destroy(Menu61, 0.0f);
        Destroy(Menu62, 0.0f);
        Destroy(Menu63, 0.0f);
        Destroy(Menu64, 0.0f);
        Destroy(Menu71, 0.0f);                                                                                      //��� ������ ������Ʈ�� �ı�
        Destroy(Menu72, 0.0f);
        Destroy(Menu73, 0.0f);
        Destroy(Menu74, 0.0f);
        Destroy(Menu81, 0.0f);
        Destroy(Menu82, 0.0f);
        Destroy(Menu83, 0.0f);
        Destroy(Menu84, 0.0f);
        Destroy(Menu91, 0.0f);                                                                                      //��� ������ ������Ʈ�� �ı�
        Destroy(Menu92, 0.0f);
        Destroy(Menu93, 0.0f);
        Destroy(Menu94, 0.0f);
        button = 0;                                                                                                 //button = 0���� �ʱ�ȭ
    }

    public void Prefab1()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab1��ư�� ������
        button = 1;                                                                                                 //button�̶�� ������ 1�� ����
        count = 1;
        littleUI_position = littleUI_position1;
        LittleUI_position();
        food = menu1_count;                                                                                         
        food_count();
    }

    public void Prefab2()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 2;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position2;
        LittleUI_position();
        food = menu2_count;
        food_count();
    }
    public void Prefab3()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 3;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position3;
        LittleUI_position();
        food = menu3_count;
        food_count();
    }
    public void Prefab4()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 4;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position4;
        LittleUI_position();
        food = menu4_count;
        food_count();
    }
    public void Prefab5()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 5;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position5;
        LittleUI_position();
        food = menu5_count;
        food_count();
    }
    public void Prefab6()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 6;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position6;
        LittleUI_position();
        food = menu6_count;
        food_count();
    }
    public void Prefab7()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 7;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position7;
        LittleUI_position();
        food = menu7_count;
        food_count();
    }
    public void Prefab8()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 8;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position8;
        LittleUI_position();
        food = menu8_count;
        food_count();
    }
    public void Prefab9()                                                                                           //Prefab1�̶�� ��ư �̺�Ʈ �Լ� ����
    {                                                                                                               //-->Prefab2��ư�� ������
        button = 9;                                                                                                 //button�̶�� ������ 2�� ����
        count = 1;
        littleUI_position = littleUI_position9;
        LittleUI_position();
        food = menu9_count;
        food_count();
    }

    public void menu_1()                                                                                            //menu1��ư�� ��������
    {
        switch (button)
        {
            case 1:
                menu = 11;
                break;
            case 2:
                menu = 21;
                break;
            case 3:
                menu = 31;
                break;
            case 4:
                menu = 41;
                break;
            case 5:
                menu = 51;
                break;
            case 6:
                menu = 61;
                break;
            case 7:
                menu = 71;
                break;
            case 8:
                menu = 81;
                break;
            case 9:
                menu = 91;
                break;
        }
        count = 1;
        LittleUI.SetActive(false);
    }
    public void menu_2()                                                                                            //menu2��ư�� ��������
    {
        switch (button)
        {
            case 1:
                menu = 12;
                break;
            case 2:
                menu = 22;
                break;
            case 3:
                menu = 32;
                break;
            case 4:
                menu = 42;
                break;
            case 5:
                menu = 52;
                break;
            case 6:
                menu = 62;
                break;
            case 7:
                menu = 72;
                break;
            case 8:
                menu = 82;
                break;
            case 9:
                menu = 92;
                break;
        }
        count = 1;
        LittleUI.SetActive(false);
    }
    public void menu_3()                                                                                            //menu3��ư�� ��������
    {
        switch (button)
        {
            case 1:
                menu = 13;
                break;
            case 2:
                menu = 23;
                break;
            case 3:
                menu = 33;
                break;
            case 4:
                menu = 43;
                break;
            case 5:
                menu = 53;
                break;
            case 6:
                menu = 63;
                break;
            case 7:
                menu = 73;
                break;
            case 8:
                menu = 83;
                break;
            case 9:
                menu = 93;
                break;
        }

        count = 1;
        LittleUI.SetActive(false);
    }
    
    public void menu_4()                                                                                            //menu4��ư�� ��������
    {
        switch (button)
        {
            case 1:
                menu = 14;
                break;
            case 2:
                menu = 24;
                break;
            case 3:
                menu = 34;
                break;
            case 4:
                menu = 44;
                break;
            case 5:
                menu = 54;
                break;
            case 6:
                menu = 64;
                break;
            case 7:
                menu = 74;
                break;
            case 8:
                menu = 84;
                break;
            case 9:
                menu = 94;
                break;
        }
        count = 1;
        LittleUI.SetActive(false);
    }

    
    public void plus()                                                                                              //plus ��ư�� ��������
    {
        food++;
        food_count();
        count = 1;
    }

    public void minus()                                                                                             //minus ��ư�� ��������
    {
        food--;
        food_count();
        count = 1;
    }

    private void food_count()                                                                                       //food_count��� �Լ� ����
    {
        if (food >= 4)                                                                                              //food�� 4�̻��̸�
        {
            food = 4;                                                                                               //�׷��� food�� 4�� ����
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI�� 4�� ��� Ȱ��ȭ
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(true);
            LittleUI.transform.GetChild(3).gameObject.SetActive(true);
            food_number.text = "4";                                                                                 //text�� 4�� ǥ��
            
            switch (button)
            {
                case 1:
                    menu1_count = food;
                    food_number1.text = "4";
                    break;
                case 2:
                    menu2_count = food;
                    food_number2.text = "4";
                    break;
                case 3:
                    menu3_count = food;
                    food_number3.text = "4";
                    break;
                case 4:
                    menu4_count = food;
                    food_number4.text = "4";
                    break;
                case 5:
                    menu5_count = food;
                    food_number5.text = "4";
                    break;
                case 6:
                    menu6_count = food;
                    food_number6.text = "4";
                    break;
                case 7:
                    menu7_count = food;
                    food_number7.text = "4";
                    break;
                case 8:
                    menu8_count = food;
                    food_number8.text = "4";
                    break;
                case 9:
                    menu9_count = food;
                    food_number9.text = "4";
                    break;
            }
        }
        else if (food == 3)                                                                                         //food�� 3�̸�
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 3�� Ȱ��ȭ
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(true);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "3";                                                                                //text�� 3�� ǥ��
            switch(button)
            {
                case 1:
                    Destroy(Menu14, 0.0f);
                    menu1_count = food;
                    food_number1.text = "3";
                    break;
                case 2:
                    Destroy(Menu24, 0.0f);
                    menu2_count = food;
                    food_number2.text = "3";
                    break;
                case 3:
                    Destroy(Menu34, 0.0f);
                    menu3_count = food;
                    food_number3.text = "3";
                    break;
                case 4:
                    Destroy(Menu44, 0.0f);
                    menu4_count = food;
                    food_number4.text = "3";
                    break;
                case 5:
                    Destroy(Menu54, 0.0f);
                    menu5_count = food;
                    food_number5.text = "3";
                    break;
                case 6:
                    Destroy(Menu64, 0.0f);
                    menu6_count = food;
                    food_number6.text = "3";
                    break;
                case 7:
                    Destroy(Menu74, 0.0f);
                    menu7_count = food;
                    food_number7.text = "3";
                    break;
                case 8:
                    Destroy(Menu84, 0.0f);
                    menu8_count = food;
                    food_number8.text = "3";
                    break;
                case 9:
                    Destroy(Menu94, 0.0f);
                    menu9_count = food;
                    food_number9.text = "3";
                    break;
            }

        }
        else if (food == 2)                                                                                         //food�� 2�̸�
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 2�� Ȱ��ȭ
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "2";                                                                                 //text�� 2ǥ��
            switch (button)
            {
                case 1:
                    Destroy(Menu13, 0.0f);
                    menu1_count = food;
                    food_number1.text = "2";
                    break;
                case 2:
                    Destroy(Menu23, 0.0f);
                    menu2_count = food;
                    food_number2.text = "2";
                    break;
                case 3:
                    Destroy(Menu33, 0.0f);
                    menu3_count = food;
                    food_number3.text = "2";
                    break;
                case 4:
                    Destroy(Menu43, 0.0f);
                    menu4_count = food;
                    food_number4.text = "2";
                    break;
                case 5:
                    Destroy(Menu53, 0.0f);
                    menu5_count = food;
                    food_number5.text = "2";
                    break;
                case 6:
                    Destroy(Menu63, 0.0f);
                    menu6_count = food;
                    food_number6.text = "2";
                    break;
                case 7:
                    Destroy(Menu73, 0.0f);
                    menu7_count = food;
                    food_number7.text = "2";
                    break;
                case 8:
                    Destroy(Menu83, 0.0f);
                    menu8_count = food;
                    food_number8.text = "2";
                    break;
                case 9:
                    Destroy(Menu93, 0.0f);
                    menu9_count = food;
                    food_number9.text = "2";
                    break;
            }
        }
        else if (food == 1)                                                                                         //food�� 1�̸�
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 1�� Ȱ��ȭ
            LittleUI.transform.GetChild(1).gameObject.SetActive(false);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "1";                                                                                 //text�� 1ǥ��
            switch (button)
            {
                case 1:
                    Destroy(Menu12, 0.0f);
                    menu1_count = food;
                    food_number1.text = "1";
                    break;
                case 2:
                    Destroy(Menu22, 0.0f);
                    menu2_count = food;
                    food_number2.text = "1";
                    break;
                case 3:
                    Destroy(Menu32, 0.0f);
                    menu3_count = food;
                    food_number3.text = "1";
                    break;
                case 4:
                    Destroy(Menu42, 0.0f);
                    menu4_count = food;
                    food_number4.text = "1";
                    break;
                case 5:
                    Destroy(Menu52, 0.0f);
                    menu5_count = food;
                    food_number5.text = "1";
                    break;
                case 6:
                    Destroy(Menu62, 0.0f);
                    menu6_count = food;
                    food_number6.text = "1";
                    break;
                case 7:
                    Destroy(Menu72, 0.0f);
                    menu7_count = food;
                    food_number7.text = "1";
                    break;
                case 8:
                    Destroy(Menu82, 0.0f);
                    menu8_count = food;
                    food_number8.text = "1";
                    break;
                case 9:
                    Destroy(Menu92, 0.0f);
                    menu9_count = food;
                    food_number9.text = "1";
                    break;
            }
        }
        else if (food <= 0)                                                                                         //food�� 0�����̸�
        {
            food = 0;                                                                                               //food�� 0�� ����
            LittleUI.transform.GetChild(0).gameObject.SetActive(false);                          //littleUI ��� ��Ȱ��ȭ
            LittleUI.transform.GetChild(1).gameObject.SetActive(false);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "0";                                                                                 //text�� 0ǥ��
            switch (button)
            {
                case 1:
                    Destroy(Menu11, 0.0f);
                    menu1_count = food;
                    food_number1.text = "0";
                    break;
                case 2:
                    Destroy(Menu21, 0.0f);
                    menu2_count = food;
                    food_number2.text = "0";
                    break;
                case 3:
                    Destroy(Menu31, 0.0f);
                    menu3_count = food;
                    food_number3.text = "0";
                    break;
                case 4:
                    Destroy(Menu41, 0.0f);
                    menu4_count = food;
                    food_number4.text = "0";
                    break;
                case 5:
                    Destroy(Menu51, 0.0f);
                    menu5_count = food;
                    food_number5.text = "0";
                    break;
                case 6:
                    Destroy(Menu61, 0.0f);
                    menu6_count = food;
                    food_number6.text = "0";
                    break;
                case 7:
                    Destroy(Menu71, 0.0f);
                    menu7_count = food;
                    food_number7.text = "0";
                    break;
                case 8:
                    Destroy(Menu81, 0.0f);
                    menu8_count = food;
                    food_number8.text = "0";
                    break;
                case 9:
                    Destroy(Menu91, 0.0f);
                    menu9_count = food;
                    food_number9.text = "0";
                    break;
            }
        }
        menu = 0;
    }
    
    public void ToHome()                                                                                            //HomeCanvas�� �̵���Ű�� ��ư
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(true);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(false);
        count = 1;
    }
    public void ToMenu()                                                                                            //MenuCanvas�� �̵���Ű�� ��ư
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(true);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(false);
        count = 1;
    }

    public void ToPlating()                                                                                         //PlatingCanvas�� �̵���Ű�� ��ư
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(true);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(false);
        if (tutorial_number == 1)
        {
            Tutorial.SetActive(true);
            tutorial_number = 0;
        }
        count = 1;
    }

    public void ToOrder()                                                                                           //OrderCanvas�� �̵���Ű�� ��ư
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(true);
        Scene_Ready.SetActive(false);
        count = 1;
    }

    public void ToReady()                                                                                           //ReadyCanvas�� �̵���Ű�� ��ư
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(true);
        count = 1;

    }

    private void LittleUI_position()
    {
        Vector2 position = littleUI_position.transform.position;

        LittleUI.transform.position = new Vector2(position.x + 350, position.y);

        LittleUI.SetActive(true);
        LittleUI.transform.GetChild(0).gameObject.SetActive(false);
        LittleUI.transform.GetChild(1).gameObject.SetActive(false);
        LittleUI.transform.GetChild(2).gameObject.SetActive(false);
        LittleUI.transform.GetChild(3).gameObject.SetActive(false);
    }
    
    private void Order_List()
    {
        
        if (menu1_count > 0)
        {
            Order_1.SetActive(true);
            MenuP1.SetActive(true);
            O1.text = food_number1.text;
        }
        else
        {
            Order_1.SetActive(false);
            MenuP1.SetActive(false);
        }
                    
        if (menu2_count > 0)
        {
            Order_2.SetActive(true); 
            MenuP2.SetActive(true);
            O2.text = food_number2.text;
        }
        else
        {                 
            Order_2.SetActive(false); 
            MenuP2.SetActive(false);
        }
                                        
        if (menu3_count > 0)
        {
            Order_3.SetActive(true); 
            MenuP3.SetActive(true);
            O3.text = food_number3.text;
        }
        else
        {
            Order_3.SetActive(false);
            MenuP3.SetActive(false);
        }
                    
        if (menu4_count > 0)
        {
            Order_4.SetActive(true); 
            MenuP4.SetActive(true);
            O4.text = food_number4.text;
        }
        else
        {
            Order_4.SetActive(false);
            MenuP4.SetActive(false);
        }
               
        if (menu5_count > 0)
        {
            Order_5.SetActive(true); 
            MenuP5.SetActive(true);
            O5.text = food_number5.text;
        }
        else
        {
            Order_5.SetActive(false);
            MenuP5.SetActive(false);
        }
            
        if (menu6_count > 0)
        {
            Order_6.SetActive(true); 
            MenuP6.SetActive(true); 
            O6.text = food_number6.text;
        }
        else
        {
            Order_6.SetActive(false);
            MenuP6.SetActive(false);
        }

        if (menu7_count > 0)
        {
            Order_7.SetActive(true);
            MenuP7.SetActive(true);
            O7.text = food_number7.text;
        }
        else
        {
            Order_7.SetActive(false);
            MenuP7.SetActive(false);
        }

        if (menu8_count > 0)
        {
            Order_8.SetActive(true);
            MenuP8.SetActive(true);
            O8.text = food_number8.text;
        }
        else
        {
            Order_8.SetActive(false);
            MenuP8.SetActive(false);
        }

        if (menu9_count > 0)
        {
            Order_9.SetActive(true);
            MenuP9.SetActive(true);
            O9.text = food_number9.text;
        }
        else
        {
            Order_9.SetActive(false);
            MenuP9.SetActive(false);
        }

        
    }    

    public void Menu1_plus()
    {
        button = 1;
        food = menu1_count;
        food++;
        food_count();
        food_number1.text = food_number.text;
        count = 1;
    }

    public void Menu1_minus()
    {
        button = 1;
        food = menu1_count;
        food--;
        food_count();
        food_number1.text = food_number.text;
        count = 1;
    }

    public void Menu2_plus()
    {
        button = 2;
        food = menu2_count;
        food++;
        food_count();
        food_number2.text = food_number.text;
        count = 1;
    }

    public void Menu2_minus()
    {
        button = 2;
        food = menu2_count;
        food--;
        food_count();
        food2 = food;
        food_number2.text = food_number.text;
        count = 1;
    }
    public void Menu3_plus()
    {
        button = 3;
        food = menu3_count;
        food++;
        food_count();
        food_number3.text = food_number.text;
        count = 1;
    }

    public void Menu3_minus()
    {
        button = 3;
        food = menu3_count;
        food--;
        food_count();
        food_number3.text = food_number.text;
        count = 1;
    }

    public void Menu4_plus()
    {
        button = 4;
        food = menu4_count;
        food++;
        food_count();
        food_number4.text = food_number.text;
        count = 1;
    }

    public void Menu4_minus()
    {
        button = 4;
        food = menu4_count;
        food--;
        food_count();
        food_number4.text = food_number.text;
        count = 1;
    }
    public void Menu5_plus()
    {
        button = 5;
        food = menu5_count;
        food++;
        food_count();
        food_number5.text = food_number.text;
        count = 1;
    }

    public void Menu5_minus()
    {
        button = 5;
        food = menu5_count;
        food--;
        food_count();
        food_number5.text = food_number.text;
        count = 1;
    }

    public void Menu6_plus()
    {
        button = 6;
        food = menu6_count;
        food++;
        food_count();
        food_number6.text = food_number.text;
        count = 1;
    }

    public void Menu6_minus()
    {
        button = 6;
        food = menu6_count;
        food--;
        food_count();
        food6 = food;
        food_number6.text = food_number.text;
        count = 1;
    }

    public void Menu7_plus()
    {
        button = 7;
        food = menu7_count;
        food++;
        food_count();
        food_number7.text = food_number.text;
        count = 1;
    }

    public void Menu7_minus()
    {
        button = 7;
        food = menu7_count;
        food--;
        food_count();
        food7 = food;
        food_number7.text = food_number.text;
        count = 1;
    }

    public void Menu8_plus()
    {
        button = 8;
        food = menu8_count;
        food++;
        food_count();
        food_number8.text = food_number.text;
        count = 1;
    }

    public void Menu8_minus()
    {
        button = 8;
        food = menu8_count;
        food--;
        food_count();
        food8 = food;
        food_number8.text = food_number.text;
        count = 1;
    }

    public void Menu9_plus()
    {
        button = 9;
        food = menu9_count;
        food++;
        food_count();
        food_number9.text = food_number.text;
        count = 1;
    }

    public void Menu9_minus()
    {
        button = 9;
        food = menu9_count;
        food--;
        food_count();
        food9 = food;
        food_number9.text = food_number.text;
        count = 1;
    }
    public void calculator()
    {
        TotalPrice.text = (12.00 * menu1_count + 8.00 * menu2_count + 13.00 * menu3_count + 5.00 * menu4_count + 5.00 * menu5_count + 3.00 * menu6_count
            +3.50 * menu7_count + 3.00 * menu8_count + 3.00 * menu9_count).ToString("F2");
    }
    public void tutorial_open()
    {
        Tutorial.SetActive(true);
        count = 1;
    }

    public void tutorial_quit()
    {
        Tutorial.SetActive(false);
        count = 1;
    }

}