using SFML.Graphics;
using SFML.System;
using SFML.Window;
using StackManagerNS;
using QueueManagerNS;
using LinkedListManagerNS;
using TextBoxNS;

namespace UIManagerNS
{

  class UIManager
  {
    private Font _font;
    private uint _width;
    private Window _window;
    private List<RectangleShape> _buttons;
    private List<TextBox> _textBoxes;
    private List<Drawable> _buttonTexts;
    private string? _activeButton;
    private bool _mouseDown;

    public bool IsStackButtonActive => _activeButton == "stackBtn";

    public bool IsQueueButtonActive => _activeButton == "queueBtn";

    public bool IsLinkedListButtonActive => _activeButton == "linkedListBtn";

    public UIManager(Font font, uint width, Window window)
    {
      _font = font;
      _width = width;
      _window = window;
      _buttons = new List<RectangleShape>();
      _buttonTexts = new List<Drawable>();
      _textBoxes = new List<TextBox>();

      CreateButtons();
      CreateTextBoxes();
    }

    private void CreateButtons()
    {
      AddButton("Stack", new Vector2f(5, 5));
      AddButton("Push", new Vector2f(5, 85));
      AddButton("Pop", new Vector2f(5, 165));
      AddButton("Queue", new Vector2f(315, 5));
      AddButton("Enqueue", new Vector2f(315, 85));
      AddButton("Dequeue", new Vector2f(315, 165));
      AddButton("Linked List", new Vector2f(625, 5));
      AddButton("Add \nFirst", new Vector2f(625, 85), 145);
      AddButton("Remove \nFirst", new Vector2f(625, 165), 145);
      AddButton("Add \nLast", new Vector2f(780, 85), 145);
      AddButton("Remove \nLast", new Vector2f(780, 165), 145);
      AddButton("Add \nAfter", new Vector2f(625, 245), 145);
      AddButton("Remove \nAfter", new Vector2f(780, 245), 145);
    }

    private void CreateTextBoxes()
    {
      TextBox tb = new TextBox(new Vector2f(625, 325), new Vector2f(300, 75), _font);
      _textBoxes.Add(tb);
      _window.TextEntered += tb.HandleTextEvent;
      _window.MouseButtonPressed += tb.HandleMouseEvent;
    }

    private void AddButton(string text, Vector2f position, int sizeX = 300, int sizeY = 75, int fontSize = 24)
    {
      var button = new RectangleShape(new Vector2f(sizeX, sizeY))
      {
        Position = position
      };

      var buttonText = new Text(text, _font)
      {
        FillColor = Color.Black,
        Position = new Vector2f(position.X, position.Y)
      };
      _buttons.Add(button);
      _buttonTexts.Add(buttonText);
    }

    public void HandleInput(RenderWindow window, StackManager stackManager, QueueManager queueManager, LinkedListManager linkedListManager)
    {
      if (Mouse.IsButtonPressed(Mouse.Button.Left))
      {
        Vector2f mousePos = window.MapPixelToCoords(Mouse.GetPosition(window));
        if (!_mouseDown)
        {
          if (_buttons[0].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
          {
            _activeButton = _activeButton != "stackBtn" ? "stackBtn" : "";
          }
          else

          if (_buttons[3].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
          {
            _activeButton = _activeButton != "queueBtn" ? "queueBtn" : "";
          }
          else

          if (_buttons[6].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
          {
            _activeButton = _activeButton != "linkedListBtn" ? "linkedListBtn" : "";
          }
          else

          if (_activeButton == "stackBtn")
          {
            if (_buttons[1].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              stackManager.AddStack();
            }
            else

            if (_buttons[2].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              stackManager.RemoveStack();
            }
          }
          else
          if (_activeButton == "queueBtn")
          {
            if (_buttons[4].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              queueManager.AddQueueItem();
            }
            else

            if (_buttons[5].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              queueManager.RemoveQueueItem();
            }
          }
          else
          if (_activeButton == "linkedListBtn")
          {
            if (_buttons[7].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.AddFirstLinkedListItem();
            }
            else

            if (_buttons[8].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.RemoveFirstLinkedListItem();
            }
            else

            if (_buttons[9].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.AddLastLinkedListItem();
            }
            else
            if (_buttons[10].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.RemoveLastLinkedListItem();
            }
            else

            if (_buttons[11].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.AddAfterLinkedListItem(_textBoxes[0].getText());
            }
            else
            if (_buttons[12].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              linkedListManager.RemoveAfterLinkedListItem(_textBoxes[0].getText());
            }
          }
        }
        _buttons[0].FillColor = _activeButton == "stackBtn" ? Color.Green : Color.White;
        _buttons[3].FillColor = _activeButton == "queueBtn" ? Color.Green : Color.White;
        _buttons[6].FillColor = _activeButton == "linkedListBtn" ? Color.Green : Color.White;
        _mouseDown = true;
      }
      else
      {
        _mouseDown = false;
      }
    }

    public void Draw(RenderWindow window)
    {
      foreach (var button in _buttons)
      {
        if (_buttons[1] == button || _buttons[2] == button)
        {
          if (_activeButton == "stackBtn")
          {
            window.Draw(button);
          }
        }
        else if (_buttons[4] == button || _buttons[5] == button)
        {
          if (_activeButton == "queueBtn")
          {
            window.Draw(button);
          }
        }
        else if (_buttons[7] == button || _buttons[8] == button || _buttons[9] == button || _buttons[10] == button || _buttons[11] == button || _buttons[12] == button)
        {
          if (_activeButton == "linkedListBtn")
          {
            window.Draw(button);
          }
        }
        else
        {
          window.Draw(button);
        }
      }

      foreach (var text in _buttonTexts)
      {
        window.Draw(text);
      }

      foreach (TextBox textBox in _textBoxes ?? Enumerable.Empty<TextBox>())
      {
        if (_textBoxes?[0] == textBox)
        {
          if (_activeButton == "linkedListBtn")
          {
            textBox.Draw(window);
          }
        }
        else
        {
          textBox.Draw(window);
        }
      }
    }
  }
}