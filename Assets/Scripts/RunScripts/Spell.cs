using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Spell : MonoBehaviour
    {
        public SpellInfo state;
        public Spell(SpellInfo info)
        {
            state = new SpellInfo(info);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDMG(state.dmg);
            }
        }

    }
}
