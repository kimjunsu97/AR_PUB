using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;                                                                                  //ARRaycastManager를 쓰기 위해서
using UnityEngine.XR.ARSubsystems;                                                                                  //trackableType을 쓰기 위해서
using UnityEngine.EventSystems;                                                                                     //버튼이 눌렸을때 이벤트를 발생시키기 위해서
using UnityEngine.UI;                                                                                               //버튼을 만들기 위해서

public class ARPlaceONPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;                                                                            //ARRaycastManager에 arRaycaster를 할당(instance)
    public GameObject Scene_Start, Scene_Home, Scene_Menu, Scene_Plating, Scene_Order, Scene_Ready;                              //각 캔버스를 오브젝트로 선언
    public GameObject LittleUI;                                                                                     //수량 캔버스를 LittleUI로 선언
    public Text food_number;                                                                                        //각 음식의 수량을 선언(최대 4개)
    public Text food_number1, food_number2, food_number3, food_number4,
                food_number5, food_number6, food_number7, food_number8, food_number9;

    public GameObject Tutorial;
    int tutorial_number = 1;

    public GameObject Menu_1, Menu_2, Menu_3, Menu_4,Menu_5,Menu_6,
                      Menu_7, Menu_8, Menu_9;                                                 //음식의 Prefab을 선언
    GameObject Menu11, Menu12, Menu13, Menu14;                                                                      //생성될 음식을 선언        
    GameObject Menu21, Menu22, Menu23, Menu24;                                                                      //생성될 음식을 선언
    GameObject Menu31, Menu32, Menu33, Menu34;                                                                      //생성될 음식을 선언
    GameObject Menu41, Menu42, Menu43, Menu44;                                                                      //생성될 음식을 선언
    GameObject Menu51, Menu52, Menu53, Menu54;                                                                      //생성될 음식을 선언
    GameObject Menu61, Menu62, Menu63, Menu64;                                                                      //생성될 음식을 선언
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
    

    int button = 0;                                                                                                 //button이라는 변수 선언 및 0으로 초기화 (음식 구분에 사용)
    int count = 0;                                                                                                  //UI와 버튼을 분리시켜주기 위한 변수
    int menu = 0;                                                                                                   //음식의 몇번째 오브젝트를 배치할지에 사용
    int food = 0, food1 = 0, food2 = 0, food3 = 0, food4 = 0,                                                       //수량text에 나타내는데 사용        
        food5 = 0, food6 = 0, food7 = 0, food8 = 0, food9;
    int menu1_count = 0, menu2_count = 0, menu3_count = 0,                                                                                            //menu1의 수량
        menu4_count = 0, menu5_count = 0, menu6_count = 0,
        menu7_count = 0, menu8_count = 0, menu9_count = 0;                                                                                    

    
    void Start()                                                                                                    //스크립트가 시작할때 1회 실행되는 함수
    {
        Scene_Order.SetActive(true);
        Scene_Order.SetActive(false);
        Scene_Start.SetActive(true);                                                                                                            //Home화면에서 시작    
        LittleUI.SetActive(false);                                                                                  //LittleUI 꺼짐
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


    void Update()                                                                                                   //스크립트가 실행되는 동안 매 프레임마다 실행시키는 함수
    {
        PlaceObjectByTouch();                                                                                       //PlaceObjectByTouch라는 함수 실행
        Order_List();
        calculator();
    }

    private void PlaceObjectByTouch()                                                                               //PlaceObjectByTouch라는 함수 선언
    {
        if (Input.touchCount > 0)                                                                                   //터치가 이루어지면
        {

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))                            //UI버튼과 터치를 구분
                return;
            else
            {
                Touch touch = Input.GetTouch(0);                                                                    //모바일 장치 화면에 접촉한 손가락 순서대로 Touch 구조체 반환
                                                                                                                    //첫번째 터치 정보를 touch에 저장
                List<ARRaycastHit> hits = new List<ARRaycastHit>();                                                 //ARRaycastHit라는 자료형의 hits리스트 선언
                if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))                                //Raycast Manager에서 Raycast함수를 사용하면 인자값으로
                {                                                                                                   //1. 어디 방향으로 쏠건지 screenpoint 지정
                                                                                                                    //2. hitResults는 Ray를 쏴서 충돌되는 객체들을 List 형태로 변환
                                                                                                                    //3. 어떤타입의 오브젝트들만 허용 할건지
                                                                                                                    //--> Planes이라는 객체에 Ray가 닿으면 그 결과값을 반환받겠다는 의미
                    if (count == 0)                                                                                 //Prefab 버튼이 눌리지 않으면
                    {
                        Pose hitPose = hits[0].pose;                                                                //첫번째 hit이 일어난 객체의 position값을 hitPose에 저장

                        switch (menu)
                        {
                            case 11:                                                                                //menu11 버튼이 눌리면
                            
                                if (!Menu11)
                                {
                                    Menu11 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu11.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu11.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 12:                                                                                //menu12 버튼이 눌리면
                            
                                if (!Menu12)
                                {
                                    Menu12 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu12.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu12.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 13:                                                                                //menu13 버튼이 눌리면
                            
                                if (!Menu13)
                                {
                                    Menu13 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu13.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu13.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 14:                                                                                //menu14 버튼이 눌리면
                            
                                if (!Menu14)
                                {
                                    Menu14 = Instantiate(Menu_1, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu14.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu14.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 21:                                                                                //menu21 버튼이 눌리면
                            
                                if (!Menu21)
                                {
                                    Menu21 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu21.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu21.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 22:                                                                                //menu22 버튼이 눌리면
                            
                                if (!Menu22)
                                {
                                    Menu22 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu22.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu22.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 23:                                                                                //menu23 버튼이 눌리면
                            
                                if (!Menu23)
                                {
                                    Menu23 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu23.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu23.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            
                            case 24:                                                                                //menu24 버튼이 눌리면
                            
                                if (!Menu24)
                                {
                                    Menu24 = Instantiate(Menu_2, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                    //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu24.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu24.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            case 31:                                                                                //menu11 버튼이 눌리면

                                if (!Menu31)
                                {
                                    Menu31 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu31.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu31.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 32:                                                                                //menu12 버튼이 눌리면

                                if (!Menu32)
                                {
                                    Menu32 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu32.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu32.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 33:                                                                                //menu13 버튼이 눌리면

                                if (!Menu33)
                                {
                                    Menu33 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu33.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu33.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 34:                                                                                //menu14 버튼이 눌리면

                                if (!Menu34)
                                {
                                    Menu34 = Instantiate(Menu_3, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu34.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu34.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 41:                                                                                //menu21 버튼이 눌리면

                                if (!Menu41)
                                {
                                    Menu41 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu41.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu41.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 42:                                                                                //menu22 버튼이 눌리면

                                if (!Menu42)
                                {
                                    Menu42 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu42.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu42.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 43:                                                                                //menu23 버튼이 눌리면

                                if (!Menu43)
                                {
                                    Menu43 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu43.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu43.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 44:                                                                                //menu24 버튼이 눌리면

                                if (!Menu44)
                                {
                                    Menu44 = Instantiate(Menu_4, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu44.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu44.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            case 51:                                                                                //menu11 버튼이 눌리면

                                if (!Menu51)
                                {
                                    Menu51 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu51.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu51.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 52:                                                                                //menu12 버튼이 눌리면

                                if (!Menu52)
                                {
                                    Menu52 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu52.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu52.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 53:                                                                                //menu13 버튼이 눌리면

                                if (!Menu53)
                                {
                                    Menu53 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu53.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu53.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 54:                                                                                //menu14 버튼이 눌리면

                                if (!Menu54)
                                {
                                    Menu54 = Instantiate(Menu_5, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu54.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu54.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 61:                                                                                //menu21 버튼이 눌리면

                                if (!Menu61)
                                {
                                    Menu61 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu61.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu61.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 62:                                                                                //menu22 버튼이 눌리면

                                if (!Menu62)
                                {
                                    Menu62 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu62.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu62.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 63:                                                                                //menu23 버튼이 눌리면

                                if (!Menu63)
                                {
                                    Menu63 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu63.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu63.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 64:                                                                                //menu24 버튼이 눌리면

                                if (!Menu64)
                                {
                                    Menu64 = Instantiate(Menu_6, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu64.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu64.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                            case 71:                                                                                //menu21 버튼이 눌리면

                                if (!Menu71)
                                {
                                    Menu71 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu71.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu71.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 72:                                                                                //menu22 버튼이 눌리면

                                if (!Menu72)
                                {
                                    Menu72 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu72.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu72.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 73:                                                                                //menu23 버튼이 눌리면

                                if (!Menu73)
                                {
                                    Menu73 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu73.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu73.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 74:                                                                                //menu24 버튼이 눌리면

                                if (!Menu74)
                                {
                                    Menu74 = Instantiate(Menu_7, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu74.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu74.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 81:                                                                                //menu21 버튼이 눌리면

                                if (!Menu81)
                                {
                                    Menu81 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu81.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu81.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 82:                                                                                //menu22 버튼이 눌리면

                                if (!Menu82)
                                {
                                    Menu82 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu82.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu82.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 83:                                                                                //menu23 버튼이 눌리면

                                if (!Menu83)
                                {
                                    Menu83 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu83.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu83.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 84:                                                                                //menu24 버튼이 눌리면

                                if (!Menu84)
                                {
                                    Menu84 = Instantiate(Menu_8, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu84.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu84.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 91:                                                                                //menu21 버튼이 눌리면

                                if (!Menu91)
                                {
                                    Menu91 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu91.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu91.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 92:                                                                                //menu22 버튼이 눌리면

                                if (!Menu92)
                                {
                                    Menu92 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu92.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu92.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 93:                                                                                //menu23 버튼이 눌리면

                                if (!Menu93)
                                {
                                    Menu93 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu93.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu93.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;

                            case 94:                                                                                //menu24 버튼이 눌리면

                                if (!Menu94)
                                {
                                    Menu94 = Instantiate(Menu_9, hitPose.position, hitPose.rotation);                   //터치가 될때마다 게임 오브젝트가 인스턴스화(화면에 실체화)
                                                                                                                        //터치가 될때마다 hit된 공간에 게임 오브젝트를 생성	
                                }

                                else                                                                                    //spawnObject가 화면상에 존재하면             
                                {
                                    Menu94.transform.position = hitPose.position;                                       //GameObject의 위치를 갱신
                                    Menu94.transform.rotation = hitPose.rotation;                                       //GameObject의 방향을 갱신
                                }
                                break;
                        }

                    }
                    count = 0;                                                                                      //버튼 초기화
                }

            }

        }
    }
      

    public void Clear()                                                                                             //Clear()라는 버튼 이벤트 함수 선언
    {
        Destroy(Menu11, 0.0f);                                                                                      //모든 생성된 오브젝트를 파괴
        Destroy(Menu12, 0.0f);
        Destroy(Menu13, 0.0f);
        Destroy(Menu14, 0.0f);
        Destroy(Menu21, 0.0f);
        Destroy(Menu22, 0.0f);
        Destroy(Menu23, 0.0f);
        Destroy(Menu24, 0.0f);
        Destroy(Menu31, 0.0f);                                                                                      //모든 생성된 오브젝트를 파괴
        Destroy(Menu32, 0.0f);
        Destroy(Menu33, 0.0f);
        Destroy(Menu34, 0.0f);
        Destroy(Menu41, 0.0f);
        Destroy(Menu42, 0.0f);
        Destroy(Menu43, 0.0f);
        Destroy(Menu44, 0.0f);
        Destroy(Menu51, 0.0f);                                                                                      //모든 생성된 오브젝트를 파괴
        Destroy(Menu52, 0.0f);
        Destroy(Menu53, 0.0f);
        Destroy(Menu54, 0.0f);
        Destroy(Menu61, 0.0f);
        Destroy(Menu62, 0.0f);
        Destroy(Menu63, 0.0f);
        Destroy(Menu64, 0.0f);
        Destroy(Menu71, 0.0f);                                                                                      //모든 생성된 오브젝트를 파괴
        Destroy(Menu72, 0.0f);
        Destroy(Menu73, 0.0f);
        Destroy(Menu74, 0.0f);
        Destroy(Menu81, 0.0f);
        Destroy(Menu82, 0.0f);
        Destroy(Menu83, 0.0f);
        Destroy(Menu84, 0.0f);
        Destroy(Menu91, 0.0f);                                                                                      //모든 생성된 오브젝트를 파괴
        Destroy(Menu92, 0.0f);
        Destroy(Menu93, 0.0f);
        Destroy(Menu94, 0.0f);
        button = 0;                                                                                                 //button = 0으로 초기화
    }

    public void Prefab1()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab1버튼이 눌리면
        button = 1;                                                                                                 //button이라는 변수에 1을 대입
        count = 1;
        littleUI_position = littleUI_position1;
        LittleUI_position();
        food = menu1_count;                                                                                         
        food_count();
    }

    public void Prefab2()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 2;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position2;
        LittleUI_position();
        food = menu2_count;
        food_count();
    }
    public void Prefab3()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 3;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position3;
        LittleUI_position();
        food = menu3_count;
        food_count();
    }
    public void Prefab4()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 4;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position4;
        LittleUI_position();
        food = menu4_count;
        food_count();
    }
    public void Prefab5()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 5;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position5;
        LittleUI_position();
        food = menu5_count;
        food_count();
    }
    public void Prefab6()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 6;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position6;
        LittleUI_position();
        food = menu6_count;
        food_count();
    }
    public void Prefab7()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 7;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position7;
        LittleUI_position();
        food = menu7_count;
        food_count();
    }
    public void Prefab8()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 8;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position8;
        LittleUI_position();
        food = menu8_count;
        food_count();
    }
    public void Prefab9()                                                                                           //Prefab1이라는 버튼 이벤트 함수 선언
    {                                                                                                               //-->Prefab2버튼이 눌리면
        button = 9;                                                                                                 //button이라는 변수에 2를 대입
        count = 1;
        littleUI_position = littleUI_position9;
        LittleUI_position();
        food = menu9_count;
        food_count();
    }

    public void menu_1()                                                                                            //menu1버튼을 눌렀을때
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
    public void menu_2()                                                                                            //menu2버튼을 눌렀을때
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
    public void menu_3()                                                                                            //menu3버튼을 눌렀을때
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
    
    public void menu_4()                                                                                            //menu4버튼을 눌렀을때
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

    
    public void plus()                                                                                              //plus 버튼을 눌렀을때
    {
        food++;
        food_count();
        count = 1;
    }

    public void minus()                                                                                             //minus 버튼을 눌렀을때
    {
        food--;
        food_count();
        count = 1;
    }

    private void food_count()                                                                                       //food_count라는 함수 선언
    {
        if (food >= 4)                                                                                              //food가 4이상이면
        {
            food = 4;                                                                                               //그래도 food가 4를 유지
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI의 4개 모두 활성화
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(true);
            LittleUI.transform.GetChild(3).gameObject.SetActive(true);
            food_number.text = "4";                                                                                 //text에 4를 표시
            
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
        else if (food == 3)                                                                                         //food가 3이면
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 3개 활성화
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(true);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "3";                                                                                //text에 3을 표시
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
        else if (food == 2)                                                                                         //food가 2이면
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 2개 활성화
            LittleUI.transform.GetChild(1).gameObject.SetActive(true);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "2";                                                                                 //text에 2표시
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
        else if (food == 1)                                                                                         //food가 1이면
        {
            LittleUI.transform.GetChild(0).gameObject.SetActive(true);                           //littleUI 1개 활성화
            LittleUI.transform.GetChild(1).gameObject.SetActive(false);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "1";                                                                                 //text에 1표시
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
        else if (food <= 0)                                                                                         //food가 0이하이면
        {
            food = 0;                                                                                               //food가 0을 유지
            LittleUI.transform.GetChild(0).gameObject.SetActive(false);                          //littleUI 모두 비활성화
            LittleUI.transform.GetChild(1).gameObject.SetActive(false);
            LittleUI.transform.GetChild(2).gameObject.SetActive(false);
            LittleUI.transform.GetChild(3).gameObject.SetActive(false);
            food_number.text = "0";                                                                                 //text에 0표시
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
    
    public void ToHome()                                                                                            //HomeCanvas로 이동시키는 버튼
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(true);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(false);
        count = 1;
    }
    public void ToMenu()                                                                                            //MenuCanvas로 이동시키는 버튼
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(true);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(false);
        Scene_Ready.SetActive(false);
        count = 1;
    }

    public void ToPlating()                                                                                         //PlatingCanvas로 이동시키는 버튼
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

    public void ToOrder()                                                                                           //OrderCanvas로 이동시키는 버튼
    {
        Scene_Start.SetActive(false);
        Scene_Home.SetActive(false);
        Scene_Menu.SetActive(false);
        Scene_Plating.SetActive(false);
        Scene_Order.SetActive(true);
        Scene_Ready.SetActive(false);
        count = 1;
    }

    public void ToReady()                                                                                           //ReadyCanvas로 이동시키는 버튼
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