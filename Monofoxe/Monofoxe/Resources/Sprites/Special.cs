// Template tags: 
// Special - Name of output class.
// Special - Name of current group.
// <sprite_name> - Name of each sprite.
// <sprite_hash_name> - Hash name of each sprite.

using Microsoft.Xna.Framework.Content;
using Monofoxe.Engine;
using Monofoxe.Engine.Drawing;
using System.Collections.Generic;

namespace Resources.Sprites
{
	public static class Special
	{
		#region Sprites.
		public static Sprite AutismCat;
		#endregion Sprites.
		
		private static string _groupName = "Special";
		private static ContentManager _content = new ContentManager(GameMgr.Game.Services);
		
		public static bool Loaded = false;
		
		public static void Load()
		{
			Loaded = true;
			var graphicsPath = AssetMgr.ContentDir + '/' + AssetMgr.GraphicsDir +  '/' + _groupName;
			var sprites = _content.Load<Dictionary<string, Sprite>>(graphicsPath);
			
			#region Sprite constructors.
			
			AutismCat = sprites["autism cat"];
			
			#endregion Sprite constructors.
		}
		
		public static void Unload()
		{
			_content.Unload();
			Loaded = false;
		}
	}
}