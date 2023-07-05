using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnnasWorld.Desktop.Scripts.Constructs;

namespace AnnasWorld.Desktop.Scripts.Utils
{
    public class Sprite : GameObject
    {
        public Vector2 origin;

        public Texture2D texture;
        public Rectangle sourceRectangle;
        public Vector2 scale;
        public Color color;
        public float rotation;

        public Sprite(Texture2D texture, 
                      Rectangle sourceRectangle,
                      Vector2 position, 
                      Vector2 scale,
                      float rotation = 0,
                      Color? color = null) : base(position)
        {
            this.texture = texture;
            this.position = position;

            this.scale = scale;
            this.rotation = rotation;
            this.sourceRectangle = sourceRectangle;

            if (color != null)
            {
                this.color = (Color)color;
            }
            else
            {
                this.color = Color.White;
            }

            origin = new Vector2(sourceRectangle.Width * 0.5f, sourceRectangle.Height * 0.5f);
        }

        public override void Update(float delta)
        {
            throw new System.NotImplementedException();
        }

        public int Width
        {
            get
            {
                return (int)(sourceRectangle.Width * scale.X);
            }
        }

        public int Height
        {
            get
            {
                return (int)(sourceRectangle.Height * scale.Y);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(texture,
                             sourceRectangle: sourceRectangle,
                             position: position,
                             color: color,
                             rotation: rotation,
                             scale: scale,
                             origin: origin,
                             effects: spriteEffects,
                             layerDepth: 1);
        }
    }
}