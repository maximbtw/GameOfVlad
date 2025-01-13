using GameOfVlad.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.GameObject;

public class TextureAnimation<TGameObject>
    where TGameObject : IGameObject
{
    private TGameObject _gameObject;
    private readonly Texture2D[] _textures;
    private readonly float _timePerFrame;
    private readonly bool _looping;
    private readonly Timer _timer = new();
    
    private int _currentFrame;

    public TextureAnimation(
        TGameObject gameObject,
        Texture2D[] textures,
        float timePerFrame,
        bool looping = true)
    {
        _gameObject = gameObject;
        _textures = textures;
        _timePerFrame = timePerFrame;
        _looping = looping;

        SetTexture();
    }

    public void Update(GameTime gameTime)
    {
        _timer.Update(gameTime);

        bool needSwitchTexture = false;
        if (_timer.Time >= _timePerFrame)
        {
            _timer.Reset();
            _currentFrame++;
            needSwitchTexture = true;

            if (_currentFrame >= _textures.Length)
            {
                if (_looping)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame = _textures.Length - 1;
                }
            }
        }

        if (needSwitchTexture)
        {
            SetTexture();
        }
    }

    private void SetTexture()
    {
        _gameObject.Texture = _textures[_currentFrame];
    }
}