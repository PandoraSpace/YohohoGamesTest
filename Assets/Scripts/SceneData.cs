using Client.Views;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerView _player;
    [SerializeField] private GeneratePlaceView[] _placesItem;
    [SerializeField] private DropPlaceView _dropPlace;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _itemAmount;

    public Joystick Joystick => _joystick;
    public PlayerView Player => _player;
    public GeneratePlaceView[] PlacesItem => _placesItem;
    public DropPlaceView DropPlace => _dropPlace;
    public Image ItemIcon => _itemIcon;
    public Text ItemAmount => _itemAmount;
}
