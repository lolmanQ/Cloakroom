using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloakroomSharp
{
	public class TGESettings
	{
		public class WindowSettings
		{
			public class Color
			{
				public float r;
				public float g;
				public float b;
				public float a;
			}

			public class Rect
			{
				public int x;
				public int y;
				public int w;
				public int h;
			}

			public Color clear_color = new Color() { r = 0.0f, g = 0.25f, b = 0.2f, a = 1.0f };
			public Rect render_rect = new Rect() { x = 0, y = 0, w = 1600, h = 900 };
			public bool start_in_fullscreen = false;
			public string title = "TGEPP - A better engine";
			public Rect window_rect = new Rect() { x = 0, y = 0, w = 1600, h = 900 };
		}

		public bool enable_vsync = true;
		public string engine_assets_path = "EngineAssets\\";
		public bool use_letterbox_and_pillarbox = true;
		public WindowSettings window_settings = new WindowSettings();

	}
}

//{
//	"enable_vsync": true,
//	"engine_assets_path": "EngineAssets\\",
//	"use_letterbox_and_pillarbox": true,
//	"window_settings": {
//		"clear_color": {
//			"a": 1.0,
//			"b": 0.25,
//			"g": 0.2,
//			"r": 0.0
//		},
//		"render_rect": {
//			"h": 900,
//			"w": 1600,
//			"x": 0,
//			"y": 0
//		},
//		"start_in_fullscreen": false,
//		"title": "TGE - Never gonna give you up!",
//		"window_rect": {
//			"h": 900,
//			"w": 1600,
//			"x": 0,
//			"y": 0
//		}
//	}
//}