using AnnasWorld.Desktop.Scripts.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace AnnasWorld.Desktop.Scripts.Constructs
{
    public class Entity : GameObject
    {
        public Sprite sprite;
        public RectangleCollider collider;
        public CollisionResolveType collisionResolveType;

        public Vector2 velocity;
        public Vector2 maxVelocity = new Vector2(300, 300);

        public Entity(Vector2 position, Sprite sprite = null, RectangleCollider collider = null) : base(position)
        {
            this.sprite = sprite;

            if (sprite == null)
            {
                this.sprite = new Sprite(texture: Core.textureAtlas,
                                         sourceRectangle: new Rectangle(Core.textureSize * 3,
                                                               0,
                                                               Core.textureSize,
                                                               Core.textureSize),
                                          position: new Vector2(position.X, position.Y),
                                          scale: new Vector2(Core.gameScale, Core.gameScale));
            }

            if (collider == null)
            {
                this.collider = new RectangleCollider(new Vector2(position.X, position.Y), this.sprite.Width, this.sprite.Height);
            }

            collisionResolveType = CollisionResolveType.STATIC;

            velocity = new Vector2();
        }

        public void Resolve(Entity other, CollisionResolveType collisionResolveType)
        {
            Rectangle collision = Rectangle.Intersect(collider.Rectangle, other.collider.Rectangle);

            if (collision.Width < collision.Height && position.X > other.position.X)
            {
                switch (collisionResolveType)
                {
                    case CollisionResolveType.STATIC:
                        position.X += collision.Width;
                        position.X = (float)Math.Floor(position.X);
                        break;

                    case CollisionResolveType.NONE:
                        break;
                }
            }
            if (collision.Width > collision.Height && position.Y > other.position.Y)
            {
                switch (collisionResolveType)
                {
                    case CollisionResolveType.STATIC:
                        position.Y += collision.Height;
                        position.Y = (float)Math.Floor(position.Y);
                        break;

                    case CollisionResolveType.NONE:
                        break;
                }
            }



            if (collision.Width < collision.Height && position.X < other.position.X)
            {
                switch (collisionResolveType)
                {
                    case CollisionResolveType.STATIC:
                        collision.Width -= 1;
                        position.X = (float)Math.Floor(position.X);
                        position.X -= collision.Width;
                        break;

                    case CollisionResolveType.NONE:
                        break;
                }

            }
            if (collision.Width > collision.Height && position.Y < other.position.Y)
            {
                switch (collisionResolveType)
                {
                    case CollisionResolveType.STATIC:
                        collision.Height -= 1;
                        position.Y = (float)Math.Floor(position.Y);
                        position.Y -= collision.Height;
                        break;

                    case CollisionResolveType.NONE:
                        break;
                }
            }

            collider.position = position;
            sprite.position = position;
        }

        public override void Update(float delta)
        {
            velocity = Vector2.Clamp(velocity, -maxVelocity, maxVelocity);
            velocity *= (1.0f - Core.friction);

            position += velocity * delta;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            sprite.Draw(spriteBatch, spriteEffects);
            //collider.Draw(spriteBatch, spriteEffects);
        }
    }
}
