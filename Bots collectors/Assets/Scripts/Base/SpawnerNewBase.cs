using System;
using TMPro;
using UnityEngine;

public class SpawnerNewBase : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Base _prefab;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private LayerMask _ground;

    private Flag _currentFlag;
    private Vector3 _pointSet;
    private Unit _unit;
    private int _priceNewBase = 5;

    public event Action FlagPlaced;

    private void FixedUpdate()
    {
        TryPlaceFlag();
    }

    public void SetPrefabBase(Base prefab) => _prefab = prefab;

    public Base GetInfoPreafab() => _prefab;

    public void AttemptBuild()
    {
        _unit = _base.GiveFreeUnit();

        if (_base.GiveInfoCountResorce() >= _priceNewBase && _unit != null)
        {
            _unit.UnitReadyToBuild += ConstructBase;
            _unit.SetFlag(_currentFlag);
            _unit.GoToPoint(_currentFlag.transform);
        }
    }

    private void TryPlaceFlag()
    {
        if (_base.CanPlaceFlag == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ground))
                {
                    Vector3 vector3 = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);

                    _pointSet = vector3;
                }

                PlaceFlag();

                FlagPlaced?.Invoke();
            }
        }
    }

    private void PlaceFlag()
    {
        if (_currentFlag == null)
        {
            _currentFlag = Instantiate(_flagPrefab, _pointSet, Quaternion.identity);
            AttemptBuild();
        }
        else
        {
            _currentFlag.transform.position = _pointSet;
            AttemptBuild();
        }
    }

    public void ConstructBase()
    {
        _base.GiveInfo(out Unit unit, out TextMeshProUGUI text, out Base prefab, out PoolResource poolResource);

        Base newBase = Instantiate(prefab, _currentFlag.transform.position, Quaternion.identity);
        newBase.Initialize(unit, text, prefab, poolResource);

        _unit.SetBase(newBase);

        Destroy(_currentFlag);
        _unit.UnitReadyToBuild -= ConstructBase;
    }
}