using SFML.Graphics;
using SFML.System;

namespace StaticArrayManagerNS
{
  class StaticArrayManager
  {
    private Font _font;
    private uint _width;
    private RectangleShape[] _stackDrawables;
    private Text[] _textDrawables;
    private int _stackSize;

    public StaticArrayManager(Font font, uint width)
    {
      _font = font;
      _width = width;
      _stackDrawables = new RectangleShape[5];
      _textDrawables = new Text[5];
      _stackSize = 0;

      for (int i = 0; i < 5; i++)
      {
        InitArray();
      }
    }

    public void InitArray()
    {
      if (_stackSize < 5)
      {

        var newStack = new RectangleShape(new Vector2f(100, 100))
        {
          Position = new Vector2f(400 + (105 * (_stackSize - 1)), 800)
        };

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(400 + (105 * (_stackSize - 1)) + number.GetGlobalBounds().Height / 3f, 800);
        _stackDrawables[_stackSize] = newStack;
        _textDrawables[_stackSize] = number;
        _stackSize++;
      }
    }
    public void AddElement()
    {
      if (_stackSize < 5)
      {

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);
        number.FillColor = Color.Black;
        number.Position = _textDrawables[_stackSize].Position;
        _textDrawables[_stackSize] = number;
        _stackSize++;
      }
    }

    public void RemoveElement()
    {
      if (_stackSize >= 1)
      {

        var text = new Text("null", _font);
        text.FillColor = Color.Black;
        text.Position = _textDrawables[_stackSize - 1].Position;
        _textDrawables[_stackSize - 1] = text;
        _stackSize--;

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