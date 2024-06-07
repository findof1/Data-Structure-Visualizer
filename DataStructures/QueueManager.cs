using SFML.Graphics;
using SFML.System;


namespace QueueManagerNS
{
  class QueueManager
  {
    private Font _font;
    private Queue<RectangleShape> _queueDrawables;
    private Queue<Text> _textDrawables;
    private int _queueLength;

    public QueueManager(Font font, uint width)
    {
      _font = font;
      _queueDrawables = new Queue<RectangleShape>();
      _textDrawables = new Queue<Text>();
      _queueLength = 0;

      for (int i = 0; i < 5; i++)
      {
        AddQueueItem();
      }
    }

    public void AddQueueItem()
    {
      if (_queueLength < 10)
      {
        _queueLength++;
        var newQueueItem = new RectangleShape(new Vector2f(100, 100))
        {
          Position = new Vector2f(100 + (105 * (_queueLength - 1)), 800)
        };

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(100 + (105 * (_queueLength - 1)) + (newQueueItem.GetGlobalBounds().Width / 4), 800 + (newQueueItem.GetGlobalBounds().Height / 4));

        _queueDrawables.Enqueue(newQueueItem);
        _textDrawables.Enqueue(number);
      }
    }

    public void RemoveQueueItem()
    {
      if (_queueLength >= 1)
      {
        _queueLength--;
        _queueDrawables.Dequeue();
        _textDrawables.Dequeue();

        foreach (var item in _queueDrawables)
        {
          item.Position -= new Vector2f(105, 0);
        }

        foreach (var item in _textDrawables)
        {
          item.Position -= new Vector2f(105, 0);
        }
      }
    }

    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        foreach (var item in _queueDrawables)
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