using SFML.Graphics;
using SFML.System;

namespace StackManagerNS
{
  class StackManager
  {
    private Font _font;
    private uint _width;
    private Stack<RectangleShape> _stackDrawables;
    private Stack<Text> _textDrawables;
    private int _stackSize;

    public StackManager(Font font, uint width)
    {
      _font = font;
      _width = width;
      _stackDrawables = new Stack<RectangleShape>();
      _textDrawables = new Stack<Text>();
      _stackSize = 0;

      for (int i = 0; i < 5; i++)
      {
        AddStack();
      }
    }

    public void AddStack()
    {
      if (_stackSize < 10)
      {
        _stackSize++;
        var newStack = new RectangleShape(new Vector2f(200, 50))
        {
          Position = new Vector2f(_width / 2f - 100, 900 - (55 * (_stackSize - 1)))
        };

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(_width / 2f - number.GetGlobalBounds().Width / 2f, 900 - (55 * (_stackSize - 1)) + number.GetGlobalBounds().Height / 3f);


        _stackDrawables.Push(newStack);
        _textDrawables.Push(number);
      }
    }

    public void RemoveStack()
    {
      if (_stackSize >= 1)
      {
        _stackSize--;
        _stackDrawables.Pop();
        _textDrawables.Pop();
      }
    }

    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        foreach (var item in _stackDrawables)
        {
          window.Draw(item);
        }

        foreach (var item in _textDrawables)
        {
          window.Draw(item);
        }
      }
    }
  }
}