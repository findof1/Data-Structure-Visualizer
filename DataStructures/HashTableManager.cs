using SFML.Graphics;
using SFML.System;

namespace HashTableManagerNS
{
  class HashTableManager
  {
    private Font _font;
    private uint _width;
    private Stack<RectangleShape> _stackDrawables;
    private Stack<Text> _textDrawables;
    private List<Drawable> initial;
    private int _stackSize;

    public HashTableManager(Font font, uint width)
    {
      _font = font;
      _width = width;
      _stackDrawables = new Stack<RectangleShape>();
      _textDrawables = new Stack<Text>();
      initial = new List<Drawable>();

      var newStack = new RectangleShape(new Vector2f(150, 50))
      {
        Position = new Vector2f(_width / 2f - 100, 400 + (55 * (_stackSize - 1)))
      };

      var newStack2 = new RectangleShape(new Vector2f(150, 50))
      {
        Position = new Vector2f(_width / 2f + 55, 400 + (55 * (_stackSize - 1)))
      };


      var keyText = new Text("Key", _font);

      keyText.FillColor = Color.Black;
      keyText.Position = new Vector2f(_width / 2f - 100 + keyText.GetGlobalBounds().Width / 2f, 400 + (55 * (_stackSize - 1)) + keyText.GetGlobalBounds().Height / 3f);


      var indexText = new Text("Value", _font);

      indexText.FillColor = Color.Black;
      indexText.Position = new Vector2f(_width / 2f + 55 + indexText.GetGlobalBounds().Width / 2f, 400 + (55 * (_stackSize - 1)) + indexText.GetGlobalBounds().Height / 3f);

      initial.Add(newStack);
      initial.Add(newStack2);
      initial.Add(keyText);
      initial.Add(indexText);

      _stackSize = 1;

      for (int i = 0; i < 5; i++)
      {
        Add();
      }
    }

    public void Add()
    {
      if (_stackSize < 10)
      {
        _stackSize++;
        var newStack = new RectangleShape(new Vector2f(150, 50))
        {
          Position = new Vector2f(_width / 2f - 100, 400 + (55 * (_stackSize - 1)))
        };

        var newStack2 = new RectangleShape(new Vector2f(150, 50))
        {
          Position = new Vector2f(_width / 2f - 100 + 155, 400 + (55 * (_stackSize - 1)))
        };

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(_width / 2f + 155f - number.GetGlobalBounds().Width / 2f, 400 + (55 * (_stackSize - 1)) + number.GetGlobalBounds().Height / 3f);
        string word = "";
        for (var i = 0; i < 5; i++)
        {
          word = word + (char)(rand.Next(0, 26) + 97);
        }

        var text = new Text($"{word}", _font);

        text.FillColor = Color.Black;
        text.Position = new Vector2f(_width / 2f - text.GetGlobalBounds().Width / 2f, 400 + (55 * (_stackSize - 1)) + text.GetGlobalBounds().Height / 3f);


        _stackDrawables.Push(newStack);
        _stackDrawables.Push(newStack2);
        _textDrawables.Push(text);
        _textDrawables.Push(number);
      }
    }

    public void Remove()
    {
      if (_stackSize > 1)
      {
        _stackSize--;
        _stackDrawables.Pop();
        _textDrawables.Pop();
        _stackDrawables.Pop();
        _textDrawables.Pop();
      }
    }

    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        foreach (var item in initial)
        {
          window.Draw(item);
        }

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