using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunmanager : MonoBehaviour
{
    //枪的旋转
    private float maxYRotation = 120;
    private float minYRotation = 0;
    private float maxXRotation = 60;
    private float minXRotation = 0;

    //射击时间
    private float shootTime = 1;
    private float shootTimer = 0;
    //子弹和子弹位置
    public GameObject bulletGo;
    public Transform firePosition;
    public float BulletSpeed = 2000;//子弹速度

    //音效组件
    private AudioSource gunAudio;
    private void Awake()
    {
        gunAudio = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (GameController._instance.isPaused == false)//当游戏非暂停时
        {
            
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootTime)
            {
                //可以射击 鼠标左键
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bulletCurrent = GameObject.Instantiate(bulletGo, firePosition.position, Quaternion.identity);
                    //通过施加力使子弹运动
                    bulletCurrent.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
                    //播放动画
                    gameObject.GetComponent<Animation>().Play();
                    shootTimer = 0;

                    //播放音效
                    gunAudio.Play();
                    //更新ui
                    UIManager._Instance.AddShootNum();

                    
                }
            }
            //计算旋转比例大小
            float xPosPrecent = Input.mousePosition.x / Screen.width;
            float yPosPrecent = Input.mousePosition.y / Screen.height;

            //旋转角度  在min-max之间
            float xAngle = -Mathf.Clamp(yPosPrecent * maxXRotation, minXRotation, maxXRotation) + 15;
            float yAngle = Mathf.Clamp(xPosPrecent * maxYRotation, minYRotation, maxYRotation) - 60;

            transform.eulerAngles = new Vector3(xAngle, yAngle, 0);
        }
    }
}
