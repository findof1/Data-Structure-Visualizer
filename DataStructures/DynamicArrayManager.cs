using SFML.Graphics;
using SFML.System;

namespace DynamicArrayManagerNS
{
  class DynamicArrayManager
  {
    private Font _font;
    private RectangleShape[] _arrDrawables;
    private Text[] _textDrawables;
    private int _arrSize;
    private int _currentMaxIndex;

    public DynamicArrayManager(Font font, uint width)
    {
      _font = font;
      _currentMaxIndex = 1;
      _arrDrawables = new RectangleShape[20];
      _textDrawables = new Text[20];
      _arrSize = 0;

      for (int i = 0; i < 2; i++)
      {
        InitArray(i);
      }
    }

    public void InitArray(int n)
    {
      var newStack = new RectangleShape(new Vector2f(50, 50))
      {
        Position = new Vector2f(400 + (55 * (n - 1)), 800)
      };

      var number = new Text($"null", _font, 20);

      number.FillColor = Color.Black;
      number.Position = new Vector2f(400 + (55 * (n - 1)) + number.GetGlobalBounds().Height / 3f, 800);
      _arrDrawables[n] = newStack;
      _textDrawables[n] = number;
    }
    public void AddElement()
    {
      if (_arrSize < 20)
      {
        if (_arrSize < _currentMaxIndex)
        {
          var rand = new Random();
          var number = new Text($"{rand.Next(0, 20)}", _font, 20);
          number.FillColor = Color.Black;
          number.Position = _textDrawables[_arrSize].Position;
          _textDrawables[_arrSize] = number;
          _arrSize++;
        }
        else if (_currentMaxIndex * 2 < 20)
        {
          for (int i = 0; i < _currentMaxIndex; i++)
          {
            InitArray(i + _currentMaxIndex);
          }
          _currentMaxIndex *= 2;
          AddElement();
        }
      }
    }

    public void RemoveElement()
    {
      if (_arrSize >= 1 && _arrSize > _currentMaxIndex / 2)
      {

        var text = new Text("null", _font, 20);
        text.FillColor = Color.Black;
        text.Position = _textDrawables[_arrSize - 1].Position;
        _textDrawables[_arrSize - 1] = text;
        _arrSize--;

      }
      else if (_arrSize >= 1)
      {
        for (int i = _currentMaxIndex; i > _currentMaxIndex / 2; i--)
        {
          _textDrawables[i] = new Text("", _font, 20); ;
          _arrDrawables[i] = new RectangleShape(new Vector2f(0, 0));
        }
        _currentMaxIndex /= 2;
        RemoveElement();
      }
    }

    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        foreach (var item in _arrDrawables)
        {
          if (item != null)
          {
            window.Draw(item);
          }
        }

        foreach (var item in _textDrawables)
        {
          if (item != null)
          {
            window.Draw(item);
          }
        }
      }
    }
  }
}