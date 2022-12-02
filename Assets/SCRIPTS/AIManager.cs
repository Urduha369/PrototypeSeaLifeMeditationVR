using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    public GameObject[] _waypoints;
    public GameObject _npc;
    public GameObject _player;
    private int _currentPoint = 0;
    public float _patrolSpeed;
    public float _fleeSpeed;
    public float _pointRadius = 0.5f;
    public float _playerRadius = 4.5f;
    public bool _flee;

    void Update()
    {
        NPCFlee();
     //   NPCPatroling();

    }
    private void NPCFlee()
    {
        Vector3 direction = _npc.transform.position - _player.transform.position;//npc distance and player distance =100

        if (direction.sqrMagnitude < _playerRadius)//the player is near the npc
        {
            Vector3 dir = new Vector3(direction.x, 0, direction.z);
            _npc.transform.position += dir * _fleeSpeed * Time.deltaTime;
       
        
            print("NPC flee ");
            _flee = true;
        }
        else//the player is not near the npc
        {
            //  print("NPC Patroling");
            if (!_flee)
            {
                NPCPatroling();
            }
            else
            {
                StartCoroutine(CancelFlee());
            }

        }
    }
    private void NPCPatroling()
    {
        if (Vector3.Distance(_waypoints[_currentPoint].transform.position, _npc.transform.position) < _pointRadius)//0.5//distance betwen 1 2 is 10f
        {
            _currentPoint++;//0change to 1-2-5
            //current = Random.Range(0, waypoints.Length);
            if (_currentPoint >= _waypoints.Length)
            {
                _currentPoint = 0;
            }
        }
        _npc.transform.position = Vector3.MoveTowards(_npc.transform.position, _waypoints[_currentPoint].transform.position, Time.deltaTime * _patrolSpeed);
    }
    private IEnumerator CancelFlee()
    {
        yield return new WaitForSeconds(3f);//3 seconds can be changed waiting after fleeing is done
        _flee = false;
    }
}