﻿using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Spell : MonoBehaviour
    {
        public SpellInfo state;
        public Spell(SpellInfo info)
        {
            state = new SpellInfo();
            state.SetState(info);
        }
        public void SetState(SpellInfo info)
        {
            state = new SpellInfo();
            state.SetState(info);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDMG(state.dmg);
                transform.GetComponent<AudioSource>().PlayOneShot(state.bangSound);
                Destroy(gameObject);
            }
        }
        public GameObject CreateObject(Transform tr)
        {
            GameObject gm = new GameObject();
            gm.AddComponent<Spell>().SetState(state);
            var sp = gm.AddComponent<SpriteRenderer>();
            sp.sprite = state.sprite;
            sp.sortingOrder = 4;
            var rb2d = gm.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
            gm.AddComponent<CircleCollider2D>();
            gm.AddComponent<AudioSource>();
            gm.layer = 7;

            return gm;
        }
        

    }
}
