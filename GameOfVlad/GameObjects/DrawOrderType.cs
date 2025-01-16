namespace GameOfVlad.GameObjects;

public enum DrawOrderType : int
{
    Background = 0,
    BackgroundEntity = 10,
    BackEffect = 20,
    Projectile = 30,
    Entity = 40,
    Player = 50,
    FrontEntity = 60,
    FrontEffect = 70,
    UI = 80
}