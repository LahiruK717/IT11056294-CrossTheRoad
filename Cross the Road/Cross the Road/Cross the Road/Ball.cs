using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Cross_the_Road
{
	class Ball
	{
		Texture2D ball;
		Rectangle screenBounds;

        public Ball(Texture2D ball, Rectangle screenBounds)
		{
			this.screenBounds = screenBounds;
            this.ball = ball;
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{

		}
	}
}

