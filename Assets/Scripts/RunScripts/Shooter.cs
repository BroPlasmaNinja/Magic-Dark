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
        Debug.LogWarning("ÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀÀ, ÍÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÅÃÐÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛÛ!!!!!!!!!!!");
        if (collision.gameObject.CompareTag("Enemy"))
            Enemies.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.LogWarning("Ôóõ, ïðîíåñëî");
        if (collision.gameObject.CompareTag("Enemy"))
            Enemies.Remove(collision.gameObject);
    }
    IEnumerator SpellCast(Spell spell)
    {
        while (true)
        {
            Debug.Log("Amogus");
            yield return new WaitForSeconds(spell.state.cooldown);
            if(Enemies.Count!=0)
                for (int i = 0; i < spell.state.countProjectiles; i++)
                {
                    GameObject go = spell.CreateObject(null);
                    var VectorAfterSpread = (((closestEnemy.transform.position - transform.position).normalized.y / (closestEnemy.transform.position - transform.position).normalized.x) + GameManager.rnd.Next(-(int)spell.state.spreadAngle, (int)spell.state.spreadAngle));
                    go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(VectorAfterSpread), Mathf.Sin(VectorAfterSpread)) * spell.state.speed);
                    go.transform.position = transform.position;
                    //Instantiate(go, null);
                    yield return new WaitForSeconds(spell.state.waitBetweenProjectileMs / 1000);
                }
        }
    }
}
