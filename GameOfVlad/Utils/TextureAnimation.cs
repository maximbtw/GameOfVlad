using GameOfVlad.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils;

public class TextureAnimation<TGameObject>
    where TGameObject : IGameObject
{
    private TGameObject _gameObject;
    private readonly Texture2D[] _textures;
    private readonly float _timePerFrame;
    private readonly bool _looping;

    private int _currentFrame;
    private float _timer;

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
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        bool needSwitchTexture = false;
        if (_timer >= _timePerFrame)
        {
            _timer = 0f;
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