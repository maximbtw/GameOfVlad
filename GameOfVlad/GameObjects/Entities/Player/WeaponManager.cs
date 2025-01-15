using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.WeaponSystem;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Player;

public class WeaponManager
{
    private readonly Dictionary<WeaponType, IWeapon> _weapons = new();
    private readonly List<WeaponType> _availableWeapons = [WeaponType.None];

    private int _currentWeaponIndex;
    
    public WeaponType GetCurrentWeaponType() => _availableWeapons[_currentWeaponIndex];

    public void AddWeapon(IWeapon weapon)
    {
        _weapons.Add(weapon.Type, weapon);
        _availableWeapons.Add(weapon.Type);
    }

    public void Update(GameTime gameTime)
    {
        foreach (IWeapon weapon in _weapons.Values)
        {
            weapon.Update(gameTime);
        }
    }

    public void NextWeapon()
    {
        _currentWeaponIndex++;
        
        if (_currentWeaponIndex == _availableWeapons.Count)
        {
            _currentWeaponIndex = 0;
        }
    }

    public void PrevWeapon()
    {
        _currentWeaponIndex--;
        
        if (_currentWeaponIndex == -1)
        {
            _currentWeaponIndex = _availableWeapons.Count - 1;
        }
    }

    public void Shoot(IGameObject gameObject, Vector2 targetPosition)
    {
        WeaponType currentWeaponType = GetCurrentWeaponType();
        if (currentWeaponType == WeaponType.None)
        {
            return;
        }
        
        IWeapon currentWeapon = _weapons[currentWeaponType];
        
        currentWeapon.Shoot(gameObject, targetPosition);
    }
}