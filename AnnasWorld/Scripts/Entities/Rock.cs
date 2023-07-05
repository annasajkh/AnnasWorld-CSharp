using AnnasWorld.Desktop.Scripts.Constructs;
using AnnasWorld.Desktop.Scripts.Utils;
using Microsoft.Xna.Framework;

namespace AnnasWorld.Desktop.Scripts.Entities
{
    public class Rock : Entity
    {
        public Rock(Vector2 position) : base(position)
        {
            sprite = new Sprite(texture: Core.textureAtlas,
                                sourceRectangle: new Rectangle(Core.textureSize * 4,
                                                               0,
                                                               Core.textureSize,
                                                               Core.textureSize),
                                position: new Vector2(position.X, position.Y),
                                scale: new Vector2(Core.gameScale, Core.gameScale));

            collider = new RectangleCollider(new Vector2(position.X, position.Y), 40, 40);

            collisionResolveType = CollisionResolveType.STATIC;
        }
    }
}
