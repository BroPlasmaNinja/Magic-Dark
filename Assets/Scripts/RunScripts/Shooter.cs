using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.RunScripts;
using System.Linq;
using Assets.Scripts.RunScripts.ScriptableObjects;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    SpellInfo info;
    private List<GameObject> Enemies = new List<GameObject>();
    private GameObject closestEnemy => Enemies.Aggregate((x, y) => (x.transform.position - transform.position).magnitude < (y.transform.position - transform.position).magnitude ? x : y);

    private void Awake()
    {
        NewSpell(new Spell(info));
    }
    public void NewSpell(Spell sp)
    {
        StartCoroutine(SpellCast(sp));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Enemies.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Enemies.Remove(collision.gameObject);
    }
    IEnumerator SpellCast(Spell spell)
    {
        while (true)
        {
            Debug.Log("Amogus");
            yield return new WaitForSeconds(spell.state.cooldown);
            if (Enemies.Count != 0)
                for (int i = 0; i < spell.state.countProjectiles; i++)
                {
                    GameObject go = spell.CreateObject(null);
                    var GoalVector = (closestEnemy.transform.position - transform.position).normalized;
                    go.transform.position = transform.position;
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(GoalVector.x, GoalVector.y);
                    yield return new WaitForSeconds(spell.state.waitBetweenProjectileMs / 1000);
                }
        }
    }
}
