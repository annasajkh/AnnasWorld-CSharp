using AnnasWorld.Desktop.Scripts.Constructs;
using AnnasWorld.Desktop.Scripts.Entities;
using AnnasWorld.Desktop.Scripts.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnasWorld
{
    public class Core : Game
    {
        public static int gameScale = 3;
        public static int textureSize = 16;

        public static float friction = 0.2f;
        public static float cameraSmoothFactor = 5f;
        public static float zoomFactor = 0.1f;
        public static float zoomSpeed = 1f;

        public static List<Entity> entities;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Texture2D textureAtlas;
        public static Player player;
        public static Random random;
        public static Vector2 spawnPosition = new Vector2(500, 500);

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            Window.Title = "Game";

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;

            base.Initialize();

            random = new Random();

            player = new Player(position: spawnPosition,
                                camera: new Camera2D(viewport: GraphicsDevice.Viewport,
                                                     position: spawnPosition,
                                                     rotation: 0,
                                                     zoom: 1));

            entities = new List<Entity>();


            for (int i = 0; i < 200; i++)
            {
                if (random.NextDouble() > 0.5)
                {
                    entities.Add(new Tree(new Vector2((float)(random.NextDouble() * 10000), (float)(random.NextDouble() * 10000))));
                }
                else
                {
                    entities.Add(new Rock(new Vector2((float)(random.NextDouble() * 10000), (float)(random.NextDouble() * 10000))));
                }
            }

            entities.Add(player);

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureAtlas = Content.Load<Texture2D>("texture_atlas");
        }

        protected override void UnloadContent()
        {
            textureAtlas.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.GetInput(Keyboard.GetState(), Mouse.GetState());

            foreach (var entity in entities)
            {
                entity.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            foreach (var entity in entities)
            {
                if (!(entity is Player))
                {
                    player.Resolve(entity, entity.collisionResolveType);
                }
            }

            entities = entities.OrderBy(e => e.position.Y).ToList();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SeaGreen);

            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred,
                              samplerState: SamplerState.PointClamp,
                              transformMatrix: player.camera.GetViewMatrix());

            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch, SpriteEffects.None);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}