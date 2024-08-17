using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TiledSharp;

namespace GameMennoPlochaet.Managers
{
    internal class ContentLoader
    {
        private static ContentManager content;

        // Hero textures
        public static List<Texture2D> HeroTextures { get; private set; } = new List<Texture2D>();

        // Enemy texture
        public static Texture2D BatTexture { get; private set; }
        public static Texture2D BirdTexture { get; private set; }
        public static Texture2D ExplosionTexture { get; private set; }

        // Gem textures
        public static Texture2D BlueGemTexture { get; private set; }
        public static Texture2D BlueGemSingleTexture { get; private set; }

        // Star textures
        public static Texture2D Star {  get; private set; }

        // Map1
        public static TmxMap Map { get; private set; }
        public static Texture2D Tileset { get; private set; }
        public static Texture2D Background1 { get; private set; }
        public static Texture2D map1 { get; private set; }

        // Map2
        public static TmxMap Map2 { get; private set; }
        public static Texture2D Tileset2 { get; private set; }
        public static Texture2D Background2 { get; private set; }
        public static Texture2D map2 { get; private set; }

        // Menu screen
        public static Texture2D MenuScreenBackground { get; private set; }
        public static Texture2D PlayButton { get; private set; }

        // Defeat screen
        public static Texture2D DefeatScreenBackground { get; private set; }
        public static Texture2D QuitButton { get; private set; }
        public static Texture2D RetryButton { get; private set; }

        // Victory screen
        public static Texture2D VictoryScreenBackground { get; private set; }

        // UI
        public static Texture2D Heart { get; private set; }

        public static void Initialize(ContentManager contentManager)
        {
            content = contentManager;
        }

        public static void LoadAllContent()
        {
            if (content == null)
            {
                throw new System.InvalidOperationException("ContentManager not initialized. Call Initialize() first.");
            }

            LoadHeroTextures();

            LoadEnemyTextures();

            LoadItemTextures();

            LoadStarTextures();

            LoadLevelOneMap();

            LoadLevelTwoMap();

            LoadMenuScreenContent();

            LoadDefeatScreenContent();

            LoadVictoryScreenContent();

            LoadUITextures();
        }

        private static void LoadHeroTextures()
        {
            try
            {
                HeroTextures.Add(content.Load<Texture2D>("Characters/Hero/Animation/Run"));
                HeroTextures.Add(content.Load<Texture2D>("Characters/Hero/Animation/Jump"));
                HeroTextures.Add(content.Load<Texture2D>("Characters/Hero/Animation/Idle"));
                HeroTextures.Add(content.Load<Texture2D>("Characters/Hero/Animation/Walk"));
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading hero textures: {ex.Message}");
            }
        }
        private static void LoadEnemyTextures()
        {
            try
            {
                BatTexture = content.Load<Texture2D>("Characters/Enemies/Bat/Bat");
                BirdTexture = content.Load<Texture2D>("Characters/Enemies/Bird/Bird");
                ExplosionTexture = content.Load<Texture2D>("Characters/Enemies/Landmine/Explosion");
            }

            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading enemy textures: {ex.Message}");
            }
        }
        private static void LoadItemTextures()
        {
            try
            {
                BlueGemTexture = content.Load<Texture2D>("Items/BlueGem");
                BlueGemSingleTexture = content.Load<Texture2D>("Items/BlueGemSingle");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading item textures: {ex.Message}");
            }
        }
        private static void LoadStarTextures()
        {
            try
            {
                Star = content.Load<Texture2D>("Victory/Star");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading star textures: {ex.Message}");
            }
        }
        private static void LoadLevelOneMap()
        {
            try
            {
                Map = new TmxMap("Content/Map/Map1.tmx");
                Tileset = content.Load<Texture2D>(Map.Tilesets[0].Name.ToString());

                // Load backgrounds for level one
                Background1 = content.Load<Texture2D>("Background/SkyDayTime");
                map1 = content.Load<Texture2D>("Background/Map1");

            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading level one map and backgrounds: {ex.Message}");
            }
        }
        private static void LoadLevelTwoMap()
        {
            try
            {
                Map2 = new TmxMap("Content/Map2/Map2.tmx");
                Tileset2 = content.Load<Texture2D>(Map.Tilesets[0].Name.ToString());

                // Load backgrounds for level one
                Background2 = content.Load<Texture2D>("Background/Cave");
                map2 = content.Load<Texture2D>("Background/Map2");

            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading level one map and backgrounds: {ex.Message}");
            }
        }
        private static void LoadMenuScreenContent()
        {
            try
            {
                MenuScreenBackground = content.Load<Texture2D>("Menu/Backgrounds/StartBG");
                PlayButton = content.Load<Texture2D>("Menu/Buttons/btn1");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading menu screen content: {ex.Message}");
            }
        }
        private static void LoadDefeatScreenContent()
        {
            try
            {
                DefeatScreenBackground = content.Load<Texture2D>("Defeat/DefeatScreenBackground");
                QuitButton = content.Load<Texture2D>("Defeat/QuitButton");
                RetryButton = content.Load<Texture2D>("Defeat/RetryButton");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading defeat screen content: {ex.Message}");
            }
        }
        private static void LoadVictoryScreenContent()
        {
            try
            {
                VictoryScreenBackground = content.Load<Texture2D>("Victory/VictoryScreenBackground");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading victory screen content: {ex.Message}");
            }
        }
        private static void LoadUITextures()
        {
            try
            {
                Heart = content.Load<Texture2D>("UI/Heart");
            }
            catch (ContentLoadException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading UI textures: {ex.Message}");
            }
        }
    }
}
