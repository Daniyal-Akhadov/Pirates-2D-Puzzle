using System.Collections;
using UnityEditor;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class RandomSpawner : MonoBehaviour
    {
        [Header("Spawn bound")] [SerializeField]
        private float _sectorAngle = 60f;

        [SerializeField] private float _sectorRotation;
        [SerializeField] private float _waitTime = 0.1f;
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _itemPerBurstCount = 2f;

        private Coroutine _routine;

        private const float FullAngle = 180f;

        private void OnEnable()
        {
            TryStopRoutine();
        }

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            var middleAngleDelta = (180 - _sectorRotation - _sectorAngle) / 2;
            var rightBoundary = GetUnitOnCircle(middleAngleDelta);
            Handles.DrawLine(position, position + rightBoundary);

            var leftBound = GetUnitOnCircle(middleAngleDelta + _sectorAngle);
            Handles.DrawLine(position, position + leftBound);
            Handles.DrawWireArc(position, Vector3.forward, rightBoundary, _sectorAngle, 1);

            Handles.color = new Color(1f, 1f, 1f, 0.1f);
            Handles.DrawSolidArc(position, Vector3.forward, rightBoundary, _sectorAngle, 1);
        }

        private void OnDisable()
        {
            TryStopRoutine();
        }

        public void StartDrop(GameObject[] items)
        {
            TryStopRoutine();
            _routine = StartCoroutine(StartSpawn(items));
        }

        private IEnumerator StartSpawn(GameObject[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; i < items.Length && j < _itemPerBurstCount; j++)
                {
                    Spawn(items[i]);
                    i++;
                }
            }

            yield return new WaitForSeconds(_waitTime);
        }

        private void Spawn(GameObject item)
        {
            var instance = Instantiate(item, transform.position, Quaternion.identity);
            var rigidbody = instance.GetComponent<Rigidbody2D>();
            var randomAngle = UnityEngine.Random.Range(0, _sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            rigidbody.AddForce(forceVector * _speed, ForceMode2D.Impulse);
        }

        private Vector2 AngleToVectorInSector(float angle)
        {
            var angleMiddleDelta = (FullAngle - _sectorAngle - _sectorRotation) / 2;
            return GetUnitOnCircle(angle + angleMiddleDelta);
        }

        private static Vector3 GetUnitOnCircle(float angleDegrees)
        {
            var angleRadians = angleDegrees * Mathf.PI / FullAngle;
            float x = Mathf.Cos(angleRadians);
            float y = Mathf.Sin(angleRadians);
            return new Vector2(x, y);
        }

        private void TryStopRoutine()
        {
            if (_routine != null)
                StopCoroutine(_routine);
        }
    }
}