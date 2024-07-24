using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TiledSharp;


namespace GameMennoPlochaet.Manager
{
    internal class ContentLoader
    {
        private static ContentManager content;
        
        //Hero
        public static List<Texture2D> HeroTexture = new();

        //MapEem
        public static TmxMap map;
        public static Texture2D tileset;
        public static Texture2D background1;
        public static Texture2D background2;
        public static Texture2D background3;
        public static Texture2D background4;
        public static Texture2D enemyBat;


        public ContentLoader(ContentManager contentManager)
        {
            content = contentManager;
        }
        public static void LoadAllContent()
        {
            //Hero
            HeroTexture.Add(content.Load<Texture2D>("Characters/Hero/Animation/Run"));
            HeroTexture.Add(content.Load<Texture2D>("Characters/Hero/Animation/Jump"));
            HeroTexture.Add(content.Load<Texture2D>("Characters/Hero/Animation/Idle"));
            HeroTexture.Add(content.Load<Texture2D>("Characters/Hero/Animation/Walk"));

            //MapEem
            map = new TmxMap("Content/Map/Map1.tmx");
            tileset = content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            background1 = content.Load<Texture2D>("Backgrounds/Day/01");
            background2 = content.Load<Texture2D>("Backgrounds/Day/02");
            background3 = content.Load<Texture2D>("Backgrounds/Day/03");
            background4 = content.Load<Texture2D>("Backgrounds/Day/04");
        }
    }
}
