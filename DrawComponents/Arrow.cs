using SFML.Graphics;
using SFML.System;

namespace ArrowNS
{
  public class Arrow
  {
    private RectangleShape _shaft;
    private ConvexShape _head;

    public Arrow(Vector2f start, Vector2f end, Color color, float thickness)
    {
      Vector2f direction = end - start;
      float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

      direction /= length;

      _shaft = new RectangleShape(new Vector2f(length - 10, thickness));
      _shaft.FillColor = color;
      _shaft.Position = start;
      _shaft.Rotation = (float)(Math.Atan2(direction.Y, direction.X) * 180 / Math.PI);

      _head = new ConvexShape();
      _head.SetPointCount(3);
      _head.SetPoint(1, new Vector2f(0, 0));
      _head.SetPoint(0, new Vector2f(-10, 10));
      _head.SetPoint(2, new Vector2f(10, 10));
      _head.Position = end;
      _head.Rotation = _shaft.Rotation + 90;
    }

    public void Draw(RenderWindow window)
    {
      window.Draw(_shaft);
      window.Draw(_head);
    }
  }
}