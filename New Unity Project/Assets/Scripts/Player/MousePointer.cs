using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    private RawImage pointer;
    private GameObject spawnBullet;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.Find("Canvas/Pointer").GetComponent<RawImage>();
        spawnBullet = GameObject.Find("Player/SpawnBullet");
        camera = GameObject.Find("Player/Main Camera").GetComponent<Camera>();
        //Vector3 position = camera.transform.position;
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 center = new Vector3(rect.anchoredPosition.x,rect.anchoredPosition.y,0);
        //position.x = center.x;
        //position.y = center.y;
        //camera.transform.position = position;
        //spawnBullet.transform.position = position;
        pointer.transform.position = Input.mousePosition;
    }

    // Update is called once per frame
    void Update() //prove per vedere se spara
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 center = new Vector3(rect.anchoredPosition.x, rect.anchoredPosition.y, 0);
        pointer.transform.position = Input.mousePosition;
        if(Input.GetKey(KeyCode.X))
        {
            Vector3 mouseVector = Input.mousePosition - center;
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.localScale *= 3;
            Rigidbody rigid = bullet.AddComponent<Rigidbody>();
            Vector3 throwVector = (Vector3.forward * mouseVector.magnitude*0.9f + mouseVector)*10;
            rigid.AddForce(throwVector);
            print(mouseVector);
            Thread.Sleep(500);
        }
    }
}
