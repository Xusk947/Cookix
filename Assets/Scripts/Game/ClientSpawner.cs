using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField]
    private float _clientSpawnDelay = 15f;
    [Range(1, 3), SerializeField]
    private int _clientMaxSpawnCount = 1;
    private float _timer;
    public ClientController client;
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _clientSpawnDelay;
            for(int i = 0; i < Random.Range(1, _clientMaxSpawnCount); i++)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        ClientController controller = Instantiate(client);
        controller.Target = GameManager.Instance.clientEntter.transform.position;
        controller.transform.position = transform.position;
    }
}
