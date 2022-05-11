using Gamina.Maze.Interactables.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamina.Maze.Controllers.Inputs
{
    public class InputController : MonoBehaviour
    {
        private IInteractable _interactable;

        [SerializeField]
        private Transform _selectedItem;

        private Vector3 _firstInputPosition;

        [SerializeField]
        [Range(0.01f, 0.15f)]
        private float _sensitivity;

        private bool _inputReleased => Input.GetMouseButtonUp(0);
        private bool _inputHold => Input.GetMouseButton(0);
        private bool _inputDown => Input.GetMouseButtonDown(0);

        private void OnEnable() => _interactable = _selectedItem.GetComponent<IInteractable>();

        private void Update()
        {
            if (_inputDown)
            {
                _firstInputPosition = Input.mousePosition;
                _interactable.OnClick();
            }

            if (_inputReleased)
                OnInputReleased();

            if (_inputHold)
                FollowInput();
        }

        private void FollowInput()
        {
            _firstInputPosition = Vector3.Lerp(_firstInputPosition, Input.mousePosition, Time.deltaTime * 3f);

            float xDiff = Input.mousePosition.x - _firstInputPosition.x;
            xDiff *= _sensitivity;
            
            Vector3 target = new Vector3(_selectedItem.eulerAngles.x, _selectedItem.eulerAngles.y, _selectedItem.eulerAngles.z + xDiff);
            _selectedItem.rotation = Quaternion.Euler(target);
            //Quaternion.RotateTowards(_selectedItem.rotation, Quaternion.Euler(target), Time.deltaTime * 20);
        }

        private void OnInputReleased()
        {
            if (_selectedItem == null) return;
            _interactable.OnRelease();
            _firstInputPosition = Vector3.zero;
        }
    }
}
