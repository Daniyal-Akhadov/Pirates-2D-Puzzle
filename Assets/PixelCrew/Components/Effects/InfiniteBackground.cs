using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.Effects
{
    public class InfiniteBackground : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private Camera _camera;

        private Bounds _containerBounds;
        private Bounds _allBounds;

        private Vector3 _boundsToTransformDelta;
        private Vector3 _containerDelta;
        private Vector3 _screenSize;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            var sprites = _container.GetComponentsInChildren<SpriteRenderer>();

            foreach (var sprite in sprites)
            {
                _containerBounds.Encapsulate(sprite.bounds);
            }

            _allBounds = _containerBounds;
            _boundsToTransformDelta = transform.position - _allBounds.center;
            _containerDelta = _container.position - _allBounds.center;
        }

        private void LateUpdate()
        {
            var min = _camera.ViewportToWorldPoint(Vector3.zero);
            var max = _camera.ViewportToWorldPoint(Vector3.one);
            _screenSize = new Vector3(max.x - min.x, max.y - min.y);

            _allBounds.center = transform.position - _boundsToTransformDelta;
            float cameraPositionX = _camera.transform.position.x;
            var screenLeft = new Vector3(cameraPositionX - _screenSize.x / 2, _containerBounds.center.y);
            var screenRight = new Vector3(cameraPositionX + _screenSize.x / 2, _containerBounds.center.y);

            if (_allBounds.Contains(screenLeft) == false)
            {
                InstantiateContainer(_allBounds.min.x - _containerBounds.extents.x);
            }

            if (_allBounds.Contains(screenRight) == false)
            {
                InstantiateContainer(_allBounds.max.x + _containerBounds.extents.x);
            }
        }

        private void InstantiateContainer(float boundsCenterX)
        {
            var newBounds = new Bounds(new Vector3(boundsCenterX, _containerBounds.center.y), _containerBounds.size);
            _allBounds.Encapsulate(newBounds);
            _boundsToTransformDelta = transform.position - _allBounds.center;
            float newContainerPosition = boundsCenterX + _containerDelta.x;
            var newPosition = new Vector3(newContainerPosition, _container.transform.position.y);

            Instantiate(_container, newPosition, Quaternion.identity, transform);
        }

        private void OnDrawGizmos()
        {
            GizmosUtils.DrawBounds(_allBounds, Color.magenta);
        }
    }
}