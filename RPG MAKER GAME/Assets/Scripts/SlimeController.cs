using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MovingObject
{
    public int atk; // 공격력
    public float attackDelay;   // 공격 유예

    public float inter_MoveWaitTime;    // 대기시간
    private float current_interMMT;

    public string atkSound;

    private Vector2 PlayerPos;  // 플레이어 좌표값

    private int random_int;
    private string direction;

    void Start()
    {
        Queue<string> queue = new Queue<string>();
        // queue = new Queue<string>();
        current_interMMT = inter_MoveWaitTime;
    }

    void Update()
    {
        current_interMMT -= Time.deltaTime;

        if (current_interMMT <= 0)
        {
            current_interMMT = inter_MoveWaitTime;
            RandomDirection();

            if (base.checkcollision())
            {
                return;
            }

            base.Move(direction);
        }
    }

    private void RandomDirection()  // 랜덤 방향으로 이동
    {
        Vector3 vector = new Vector3();
        vector.Set(0, 0, vector.z);
        // vector.Set(0,0, vector.z);
        random_int = Random.Range(0, 4);

        switch (random_int)
        {
            case 0:
                vector.y = 1f;
                direction = "UP";
                break;
            case 1:
                vector.y = -1f;
                direction = "DOWN";
                break;
            case 2:
                vector.x = 1f;
                direction = "RIGHT";
                break;
            case 3:
                vector.x = -1f;
                direction = "LEFT";
                break;
        }
    }
}
