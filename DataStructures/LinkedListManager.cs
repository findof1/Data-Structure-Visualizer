using SFML.Graphics;
using SFML.System;
using ArrowNS;

namespace LinkedListManagerNS
{
  class LinkedListManager
  {
    private Font _font;
    private LinkedList<RectangleShape> _linkedListDrawables;
    private LinkedList<Text> _textDrawables;
    private int _linkedListLength;

    public LinkedListManager(Font font)
    {
      _font = font;
      _linkedListDrawables = new LinkedList<RectangleShape>();
      _textDrawables = new LinkedList<Text>();
      _linkedListLength = 0;

      for (int i = 0; i < 5; i++)
      {
        AddFirstLinkedListItem();
      }
    }

    public void AddLastLinkedListItem()
    {
      if (_linkedListLength < 8)
      {
        unSetAlternating();
        _linkedListLength++;
        var newLinkedListItem = new RectangleShape(new Vector2f(100, 100));
        newLinkedListItem.Position = new Vector2f(100 + (205 * (_linkedListLength)), 800);

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(100 + (205 * (_linkedListLength)) + (newLinkedListItem.GetGlobalBounds().Width / 4), 800 + (newLinkedListItem.GetGlobalBounds().Height / 4));

        _linkedListDrawables.AddLast(newLinkedListItem);
        _textDrawables.AddLast(number);

        setAlternating();
      }
    }

    public void AddAfterLinkedListItem(int num)
    {
      if (_linkedListLength < 8 && _linkedListLength > 0)
      {
        unSetAlternating();
        _linkedListLength++;
        var newLinkedListItem = new RectangleShape(new Vector2f(100, 100));
        newLinkedListItem.Position = new Vector2f(100 + (205 * (num + 1)), 800);

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(100 + (205 * (num + 1)) + (newLinkedListItem.GetGlobalBounds().Width / 4), 800 + (newLinkedListItem.GetGlobalBounds().Height / 4));

        LinkedListNode<RectangleShape>? headNode = _linkedListDrawables.First;
        if (headNode != null)
        {
          for (int i = 0; i < num; i++)
          {
            if (headNode.Next != null)
            {
              headNode = headNode.Next;
            }
            else
            {
              _linkedListLength--;
              setAlternating();
              return;
            }
          }
        }
        else
        {
          _linkedListLength--;
          setAlternating();
          return;
        }
        LinkedListNode<Text>? headNode2 = _textDrawables.First;
        if (headNode2 != null)
        {
          for (int i = 0; i < num; i++)
          {
            if (headNode2.Next != null)
            {
              headNode2 = headNode2.Next;
            }
            else
            {
              _linkedListLength--;
              setAlternating();
              return;
            }
          }
        }
        else
        {
          _linkedListLength--;
          setAlternating();
          return;
        }

        if (headNode != null && headNode2 != null)
        {
          _linkedListDrawables.AddAfter(headNode, newLinkedListItem);
          _textDrawables.AddAfter(headNode2, number);

          int index = 0;
          foreach (var item in _linkedListDrawables)
          {
            if (index > num)
            {
              item.Position += new Vector2f(205, 0);
            }
            index++;
          }
          index = 0;
          foreach (var item in _textDrawables)
          {
            if (index > num)
            {
              item.Position += new Vector2f(205, 0);
            }
            index++;
          }

          setAlternating();
        }
        else
        {
          _linkedListLength--;
          setAlternating();
          return;
        }
      }
    }

    public void AddFirstLinkedListItem()
    {
      if (_linkedListLength < 8)
      {
        unSetAlternating();
        _linkedListLength++;
        var newLinkedListItem = new RectangleShape(new Vector2f(100, 100));
        newLinkedListItem.Position = new Vector2f(100, 800);

        var rand = new Random();
        var number = new Text($"{rand.Next(0, 20)}", _font);

        number.FillColor = Color.Black;
        number.Position = new Vector2f(100 + (newLinkedListItem.GetGlobalBounds().Width / 4), 800 + (newLinkedListItem.GetGlobalBounds().Height / 4));

        _linkedListDrawables.AddFirst(newLinkedListItem);
        _textDrawables.AddFirst(number);


        foreach (var item in _linkedListDrawables)
        {
          item.Position += new Vector2f(205, 0);
        }

        foreach (var item in _textDrawables)
        {
          item.Position += new Vector2f(205, 0);
        }

        setAlternating();
      }
    }

    public void RemoveLastLinkedListItem()
    {
      if (_linkedListLength >= 1)
      {
        unSetAlternating();
        _linkedListLength--;
        _linkedListDrawables.RemoveLast();
        _textDrawables.RemoveLast();

        setAlternating();
      }
    }

    public void RemoveAfterLinkedListItem(int num)
    {
      if (_linkedListLength > 0)
      {
        unSetAlternating();
        _linkedListLength--;


        LinkedListNode<RectangleShape>? headNode = _linkedListDrawables.First;
        if (headNode != null)
        {
          for (int i = 0; i <= num; i++)
          {
            if (headNode.Next != null)
            {
              headNode = headNode.Next;
            }
            else
            {
              _linkedListLength++;
              setAlternating();
              return;
            }
          }
        }
        else
        {
          _linkedListLength++;
          setAlternating();
          return;
        }
        LinkedListNode<Text>? headNode2 = _textDrawables.First;
        if (headNode2 != null)
        {
          for (int i = 0; i <= num; i++)
          {
            if (headNode2.Next != null)
            {
              headNode2 = headNode2.Next;
            }
            else
            {
              _linkedListLength++;
              setAlternating();
              return;
            }
          }
        }
        else
        {
          _linkedListLength++;
          setAlternating();
          return;
        }

        if (headNode != null && headNode2 != null)
        {
          _linkedListDrawables.Remove(headNode);
          _textDrawables.Remove(headNode2);

          int index = 0;
          foreach (var item in _linkedListDrawables)
          {
            if (index > num)
            {
              item.Position -= new Vector2f(205, 0);
            }
            index++;
          }
          index = 0;
          foreach (var item in _textDrawables)
          {
            if (index > num)
            {
              item.Position -= new Vector2f(205, 0);
            }
            index++;
          }

          setAlternating();
        }
        else
        {
          _linkedListLength++;
          setAlternating();
          return;
        }
      }
    }

    public void RemoveFirstLinkedListItem()
    {
      if (_linkedListLength >= 1)
      {

        unSetAlternating();
        _linkedListLength--;
        _linkedListDrawables.RemoveFirst();
        _textDrawables.RemoveFirst();

        foreach (var item in _linkedListDrawables)
        {
          item.Position -= new Vector2f(205, 0);
        }

        foreach (var item in _textDrawables)
        {
          item.Position -= new Vector2f(205, 0);
        }

        setAlternating();
      }
    }

    public void DrawArrows(RenderWindow window, bool IsLinkedListButtonActive)
    {
      if (!IsLinkedListButtonActive) return;

      if (_linkedListDrawables.Count < 2) return;

      var currentNode = _linkedListDrawables.First;
      while (currentNode != null && currentNode.Next != null)
      {
        Vector2f startPosition = currentNode.Value.Position;
        Vector2f endPosition = currentNode.Next.Value.Position;

        Vector2f arrowStart = startPosition + new Vector2f(100, 50);
        Vector2f arrowEnd = endPosition + new Vector2f(0, 50);

        Arrow arrow = new Arrow(arrowStart, arrowEnd, Color.White, 5);
        arrow.Draw(window);

        currentNode = currentNode.Next;
      }
    }

    public void unSetAlternating()
    {
      int index = 0;
      foreach (var item in _linkedListDrawables)
      {
        if (index % 2 != 0)
        {
          item.Position += new Vector2f(0, 100);
        }
        else
        {
          item.Position += new Vector2f(0, 0);
        }
        index++;
      }

      index = 0;
      foreach (var item in _textDrawables)
      {
        if (index % 2 != 0)
        {
          item.Position += new Vector2f(0, 100);
        }
        else
        {
          item.Position += new Vector2f(0, 0);
        }
        index++;
      }
    }

    public void setAlternating()
    {
      int index = 0;
      foreach (var item in _linkedListDrawables)
      {
        if (index % 2 != 0)
        {
          item.Position -= new Vector2f(0, 100);
        }
        else
        {
          item.Position -= new Vector2f(0, 0);
        }
        index++;
      }

      index = 0;
      foreach (var item in _textDrawables)
      {
        if (index % 2 != 0)
        {
          item.Position -= new Vector2f(0, 100);
        }
        else
        {
          item.Position -= new Vector2f(0, 0);
        }
        index++;
      }
    }
    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        foreach (var item in _linkedListDrawables)
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