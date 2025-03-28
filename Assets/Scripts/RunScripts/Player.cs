using Assets.Scripts.RunScripts.Interfaces;
using System;
using UnityEngine.EventSystems;

namespace Assets.Scripts.RunScripts
{
    public sealed class Player : ICanCast, IDamagable

    {
        private float _speed;

        public event EventHandler death;

        public void Cast()
        {

        }

        public void TakeDMG()
        {

        }
        public void Death()
        {

        }

        public void Move()
        {

        }

    }
}
