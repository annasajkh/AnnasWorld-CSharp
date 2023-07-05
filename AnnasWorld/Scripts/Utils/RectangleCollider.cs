using AnnasWorld.Desktop.Scripts.Constructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnnasWorld.Desktop.Scripts.Utils
{
    public enum CollisionResolveType
    {
        STATIC,
        DYNAMIC,
        NONE
    }



    public class RectangleCollider : GameObject
    {
        Sprite sprite;

        public RectangleCollider(Vector2 position, int width, int height) : base(position)
        {
            sprite = new Sprite(texture: Core.textureAtlas,
                                sourceRectangle: new Rectangle(Core.textureSize * 2,
                                                               0,
                                                               Core.textureSize,
                                                               Core.textureSize),
                                position: new Vector2(position.X, position.Y),
                                scale: new Vector2(width / Core.textureSize, height / Core.textureSize));
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(position.X - sprite.Width * 0.5f),
                                     (int)(position.Y - sprite.Height * 0.5f),
                                     sprite.Width,
                                     sprite.Height);
            }
        }

        public override void Update(float delta)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            sprite.position = position;
            sprite.Draw(spriteBatch, spriteEffects);
        }
    }
}
