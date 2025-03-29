using Assets.Scripts.RunScripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _speed;

        [SerializeField] private GameObject _player;

        public event EventHandler death;

        public void Death()
        {

        }

        public void TakeDMG()
        {

        }

        public void AI()
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _player.transform.position, _speed*Time.deltaTime);
        }

        public void Update()
        {
            AI();
        }
    }
}
