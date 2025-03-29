using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.RunScripts;

public class Shooter : MonoBehaviour
{
    private List<GameObject> Enemies = new List<GameObject>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemies.Remove(collision.gameObject);
    }
    IEnumerator SpellCast(Spell spell)
    {
        yield return new WaitForSeconds(spell.state.cooldown);

    }
}
