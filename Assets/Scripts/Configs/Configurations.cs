using Client.Configs;
using UnityEngine;

[CreateAssetMenu]
public class Configurations : ScriptableObject
{
    [SerializeField] private PlayerConfig _player;
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private ItemsGeneratorConfig itemsGeneratorConfig;

    public PlayerConfig Player => _player;
    public Prefabs Prefabs => _prefabs;
    public ItemsGeneratorConfig ItemsGeneratorConfig => itemsGeneratorConfig;
}