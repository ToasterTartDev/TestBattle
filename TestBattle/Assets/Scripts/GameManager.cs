using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int nowEnemyAttack = 0; //счетчик кто сейчас атакует
    public List<Enemy> allEnemy = new List<Enemy>();    //список всех enemy

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //проверка по клику, если атакующий и атакуемый готовы, то атаковать 
        {
            if ((allEnemy[nowEnemyAttack].currentState == Entity.State.idle) && (allEnemy[nowEnemyAttack].targetEnemy != null) && (allEnemy[nowEnemyAttack].targetEnemy.currentState == Entity.State.idle))
            {
                allEnemy[nowEnemyAttack].SetState(1);
                nowEnemyAttack++;
                if(nowEnemyAttack >= allEnemy.Count)
                    nowEnemyAttack = 0;
            }
        }
    }
}
