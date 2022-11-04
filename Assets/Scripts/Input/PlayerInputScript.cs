using Assets.Scripts.Enums;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerControllerScript _playerController;

    private int _horizontal = 0, _vertical = 0;

    private void Awake()
    {
        _playerController = GetComponent<PlayerControllerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = 0;
        _vertical = 0;

        GetKeyboardInput();
        SetMovement();
    }

    void GetKeyboardInput()
    {
        _horizontal = GetAxisRaw(EAxis.Horizontal);
        _vertical = GetAxisRaw(EAxis.Vertical);

        if (_horizontal != 0) _vertical = 0;
    }

    int GetAxisRaw(EAxis axis)
    {
        if (axis == EAxis.Horizontal)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) return -1;
            if (Input.GetKeyDown(KeyCode.RightArrow)) return 1;
        }
        else if (axis == EAxis.Vertical)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) return 1;
            if (Input.GetKeyDown(KeyCode.DownArrow)) return -1;
        }
        return 0;
    }

    void SetMovement()
    {
        if(_vertical != 0) _playerController.SetInputDirection(_vertical == 1 ? EPlayerDirection.UP : EPlayerDirection.DOWN);
        else if(_horizontal != 0) _playerController.SetInputDirection(_horizontal == 1 ? EPlayerDirection.RIGHT : EPlayerDirection.LEFT);
    }
}
