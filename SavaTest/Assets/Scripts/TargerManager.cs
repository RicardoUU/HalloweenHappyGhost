using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerManager : MonoBehaviour
{
    //public static TargerManager _instance;

    public GameObject[] monsters;//怪物数组
    public GameObject activeMonster = null;//激活的怪物

    public int targetPosition;//目标位置

    //private void Awake()
    //{
    //    _instance = this;
    //}

    void Start()
    {
        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<BoxCollider>().enabled = false;
            monster.SetActive(false);
        }
        //ActivateMonster();
        StartCoroutine("AliveTimer");//随机时间生成怪物
        
    }
    //激活怪物
    private void ActivateMonster()
    {
        int index = Random.Range(0, monsters.Length);//随机获取怪物
        activeMonster = monsters[index];
        activeMonster.SetActive(true);              //激活怪物
        activeMonster.GetComponent<BoxCollider>().enabled = true;//激活BoxCollider
        StartCoroutine("DeathTimer");//生命周期
    }

    //激活的变为未激活怪物
    private void DeActiveMonster()
    {
        if (activeMonster != null)
        {
           
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
        }
        StartCoroutine("AliveTimer");//随机时间生成怪物

    }


    //生成迭代器
    IEnumerator AliveTimer()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        ActivateMonster();
    }

    //
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(Random.Range(3, 8));
        DeActiveMonster();
    }
    //更新怪物生命周期 在monstermanager中调用
    public void UpdateMonsters()
    {
        StopAllCoroutines();//当被击中时停止所有协程

        if (activeMonster != null)
        {
            activeMonster.SetActive(false);

            activeMonster = null;
            //重新刷新怪物
        }
        StartCoroutine("AliveTimer");//开始生命周期

    }

    ////刷新怪物
    //public void RefreshMonster()
    //{
    //    StopAllCoroutines();
    //    if (activeMonster != null)
    //    {

    //    }
    //}
    //以类型激活怪物
    public void ActiveMonsterByType(int type)
    {
        StopAllCoroutines();
        if (activeMonster != null)
        {
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
            //重新刷新怪物
        }
        activeMonster = monsters[type];
        activeMonster.SetActive(true);
        activeMonster.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine("DeathTimer");//生命周期


    }
}
