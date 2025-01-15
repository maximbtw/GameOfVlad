namespace GameOfVlad.GameObjects;

public enum DrawOrderType : int
{
    Background = -1000,
    Effect = -500,
    BackgroundEntity = -400,
    Projectile = -350,
    Mob = -250,
    Player = 0,
    FrontCanvas = 1000
}