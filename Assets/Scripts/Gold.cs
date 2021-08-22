using UnityEngine;

public class Gold : LootItem
{
    [SerializeField]
    private int comboValue;
    public override void Take(Transform target)
    {
        GameBase.AddScore.Invoke(value);
        GameBase.Dilaver.SoundSystem.PlaySound(Sounds.loot);
        GameBase.Dilaver.ComboSystem.AddCombo(comboValue,transform.position).ChanceColor(new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f)));
        Destroy(gameObject);
    }
}
