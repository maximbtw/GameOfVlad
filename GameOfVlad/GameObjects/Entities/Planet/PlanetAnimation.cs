using System;
using System.Collections.Generic;
using System.Linq;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Planet;

public class PlanetAnimation : TextureAnimation<Planet>
{
    private const float TimePerFrame = 10f;

    private PlanetAnimation(Planet gameObject, Texture2D[] textures, float timePerFrame, bool looping = true) : base(
        gameObject, textures, timePerFrame, looping)
    {

    }

    public static PlanetAnimation CreateAnimation(ContentManager contentManager, Planet planet, PlanetType planetType)
    {
        if (planetType == PlanetType.Earth)
        {
            return new PlanetAnimation(planet, GetEarthTextures(contentManager).ToArray(), TimePerFrame);   
        }

        throw new NotSupportedException();
    }

    private static IEnumerable<Texture2D> GetEarthTextures(ContentManager contentManager)
    {
        yield return contentManager.Load<Texture2D>("2025/Sprites/Game/Planet/Earth/Terrestrial_01-256x256");
        yield return contentManager.Load<Texture2D>("2025/Sprites/Game/Planet/Earth/Terrestrial_02-256x256");
        yield return contentManager.Load<Texture2D>("2025/Sprites/Game/Planet/Earth/Terrestrial_03-256x256");
        yield return contentManager.Load<Texture2D>("2025/Sprites/Game/Planet/Earth/Terrestrial_04-256x256");
        yield return contentManager.Load<Texture2D>("2025/Sprites/Game/Planet/Earth/Terrestrial_05-256x256");
    }
}