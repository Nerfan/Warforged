using System;
using System.Windows.Media;

namespace Warforged
{

	public abstract class Card
	{
        public enum Color
        {
            red,
            green,
            blue,
            black
        }
		public string name{get; protected set;}
		public string effect{get; protected set;}
		public Color color{get; protected set;}

		public abstract void activate(Character user);
	}
}

