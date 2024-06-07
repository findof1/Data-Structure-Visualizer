using SFML.Graphics;
using SFML.System;

namespace StaticArrayManagerNS
{
  class StaticArrayManager
  {
    private Font _font;
    private RectangleShape[] _stackDrawables;
    private Text[] _textDrawables;
    private int _arrSize;

    public StaticArrayManager(Font font, uint width)
    {
      _font = font;
      _stackDrawables = new RectangleShape[5];
      _textDrawables = new Text[5];
      _arrSize = 0;

      for (int i = 0; i < 5; i++)
      {
        InitArray();
      }
    }

    public void InitArray()
    {
      if (_arrSize < 5)
      {

        var newStack = new RectangleShape(new Vector2f(100, 100))
        {
          Position = new Vector2f(400 + (105 * (_arrSize - 1)), 800)
        };

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(400 + (105 * (_arrSize - 1)) + number.GetGlobalBounds().Height / 3f, 800);
        _stackDrawables[_arrSize] = newStack;
        _textDrawables[_arrSize] = number;
        _arrSize++;
      }
    }
    public void AddElement()
    {
      if (_arrSize < 5)
      {

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);
        number.FillColor = Color.Black;
        number.Position = _textDrawables[_arrSize].Position;
        _textDrawables[_arrSize] = number;
        _arrSize++;
      }
    }

    public void RemoveElement()
    {
      if (_arrSize >= 1)
      {

        var text = new Text("null", _font);
        text.FillColor = Color.Black;
        text.Position = _textDrawables[_arrSize - 1].Position;
        _textDrawables[_arrSize - 1] = text;
        _arrSize--;

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