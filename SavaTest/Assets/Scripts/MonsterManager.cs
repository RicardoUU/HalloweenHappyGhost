using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Animation anim;
    
    //怪物当前动画
    public AnimationClip idelClip;
    public AnimationClip dieClip;
    //碰撞音效
    public AudioSource kickAudio;
    //怪物类型
    public int monsterType;
    private void Awake()
    {
        //获得怪物动画
        anim = gameObject.GetComponent<Animation>();
        anim.clip = idelClip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //销毁子弹
        if (collision.collider.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);

            //播放音效
            kickAudio.Play();
            //播放死亡动画
            anim.clip = dieClip;
            anim.Play();
            
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("Deativate");

            //更新分数
            UIManager._Instance.AddScore();  
        }
    }

    private void OnDisable()
    {
        //把默认动画改成正常状态
        anim.clip = idelClip;
    }

    IEnumerator Deativate()
    {
        yield return new WaitForSeconds(0.8f);
        //隐藏怪物 并使循环重新开始
        //TargerManager._instance.UpdateMonsters();
        //调用父级组件方法  确保不会冲突
        gameObject.GetComponentInParent<TargerManager>().UpdateMonsters();
    }
}
