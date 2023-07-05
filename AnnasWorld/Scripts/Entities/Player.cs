using AnnasWorld.Desktop.Scripts.Constructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AnnasWorld.Desktop.Scripts.Utils;
using System;

namespace AnnasWorld.Desktop.Scripts.Entities
{
    public class Player : Entity
    {

        public Camera2D camera;
        public bool regenerateChunks;

        private float mouseScrollTemp;

        public Player(Vector2 position, Camera2D camera)
            : base(position)
        {
            this.camera = camera;
            sprite = new Sprite(texture: Core.textureAtlas,
                                sourceRectangle: new Rectangle(0,
                                                               Core.textureSize * 7,
                                                               Core.textureSize,
                                                               Core.textureSize),
                                position: position,
                                scale: new Vector2(Core.gameScale, Core.gameScale));

            sprite.origin.Y = sprite.Height * 0.3055f;

            collider = new RectangleCollider(new Vector2(position.X, position.Y), sprite.Width, 20);

            collisionResolveType = CollisionResolveType.STATIC;
        }

        public void GetInput(KeyboardState keyboardState, MouseState mouseState)
        {
            //--------------Zooming---------------
            if (Math.Abs(mouseState.ScrollWheelValue - mouseScrollTemp) > 0.0001f)
            {

                if (mouseState.ScrollWheelValue - mouseScrollTemp < 0)
                {
                    camera.zoom -= Core.zoomSpeed * camera.zoom * Core.zoomFactor;
                }
                else
                {
                    camera.zoom += Core.zoomSpeed * camera.zoom * Core.zoomFactor;
                }

                mouseScrollTemp = mouseState.ScrollWheelValue;
            }

            mouseScrollTemp = mouseState.ScrollWheelValue;
            //------------------------------------

            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = 1;
            }
            else
            {
                velocity.X = 0;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y = 1;
            }
            else
            {
                velocity.Y = 0;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
                velocity *= 300;
            }
        }

        public override void Update(float delta)
        {
            //camera.position = position;
            //---------Smooth Camera Movement------------
            float dirX = position.X - camera.position.X;
            float dirY = position.Y - sprite.origin.Y - camera.position.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            if (!(length <= 0.001f))
            {
                if (Math.Abs(length) > 0.001f)
                {
                    dirX /= length;
                    dirY /= length;
                }
                else
                {
                    dirX = 0;
                    dirY = 0;
                }
                camera.position.X += dirX * length * Core.cameraSmoothFactor * delta;
                camera.position.Y += dirY * length * Core.cameraSmoothFactor * delta;
            }
            //-------------------------------------------

            base.Update(delta);
        }

    }
}
