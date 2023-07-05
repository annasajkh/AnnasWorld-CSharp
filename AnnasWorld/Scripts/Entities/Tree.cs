using AnnasWorld.Desktop.Scripts.Constructs;
using AnnasWorld.Desktop.Scripts.Utils;
using Microsoft.Xna.Framework;

namespace AnnasWorld.Desktop.Scripts.Entities
{
    public class Tree : Entity
    {
        public Tree(Vector2 position) : base(position)
        {
            sprite = new Sprite(texture: Core.textureAtlas,
                                sourceRectangle: new Rectangle(Core.textureSize,
                                                               Core.textureSize * 4,
                                                               Core.textureSize * 4,
                                                               Core.textureSize * 4),
                                position: new Vector2(position.X, position.Y),
                                scale: new Vector2(Core.gameScale, Core.gameScale));

            sprite.origin.Y = sprite.Height * 0.305f;

            collider = new RectangleCollider(new Vector2(position.X, position.Y), 20, 40);

            collisionResolveType = CollisionResolveType.STATIC;
        }
    }
}
