using SFML.Graphics;
using SFML.System;
using SFML.Window;
using StackManagerNS;
using QueueManagerNS;
using LinkedListManagerNS;
using TextBoxNS;
using StaticArrayManagerNS;
using DynamicArrayManagerNS;

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

    private int screen = 1;

    public bool IsStackButtonActive => _activeButton == "stackBtn";

    public bool IsQueueButtonActive => _activeButton == "queueBtn";

    public bool IsLinkedListButtonActive => _activeButton == "linkedListBtn";

    public bool IsStaticArrayButtonActive => _activeButton == "staticArrayBtn";

    public bool IsDynamicArrayButtonActive => _activeButton == "dynamicArrayBtn";

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
      AddButton("Stack", new Vector2f(5, 5));                    //0
      AddButton("Push", new Vector2f(5, 85));                    //1
      AddButton("Pop", new Vector2f(5, 165));                    //2
      AddButton("Queue", new Vector2f(315, 5));                  //3
      AddButton("Enqueue", new Vector2f(315, 85));               //4
      AddButton("Dequeue", new Vector2f(315, 165));              //5
      AddButton("Linked List", new Vector2f(625, 5));            //6
      AddButton("Add \nFirst", new Vector2f(625, 85), 145);      //7
      AddButton("Remove \nFirst", new Vector2f(625, 165), 145);  //8
      AddButton("Add \nLast", new Vector2f(780, 85), 145);       //9
      AddButton("Remove \nLast", new Vector2f(780, 165), 145);   //10
      AddButton("Add \nAfter", new Vector2f(625, 245), 145);     //11
      AddButton("Remove \nAfter", new Vector2f(780, 245), 145);  //12
      AddButton("Static Array", new Vector2f(935, 5));           //13
      AddButton("Add Element", new Vector2f(935, 85));           //14
      AddButton("Remove Element", new Vector2f(935, 165));       //15
      AddButton("Dynamic Array", new Vector2f(1245, 5));         //16
      AddButton("Add Element", new Vector2f(1245, 85));          //17
      AddButton("Remove Element", new Vector2f(1245, 165));      //18
      AddButton(">", new Vector2f(1555, 5));                     //19
      AddButton("<", new Vector2f(1555, 5));                     //20
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

    public void HandleInput(RenderWindow window, StackManager stackManager, QueueManager queueManager, LinkedListManager linkedListManager, StaticArrayManager staticArrayManager, DynamicArrayManager dynamicArrayManager)
    {
      if (Mouse.IsButtonPressed(Mouse.Button.Left))
      {
        Vector2f mousePos = window.MapPixelToCoords(Mouse.GetPosition(window));
        if (screen == 1)
        {
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
            else if (_buttons[13].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              _activeButton = _activeButton != "staticArrayBtn" ? "staticArrayBtn" : "";
            }
            else
            if (_buttons[16].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              _activeButton = _activeButton != "dynamicArrayBtn" ? "dynamicArrayBtn" : "";
            }
            else
            if (_buttons[19].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              screen++;
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
            else if (_activeButton == "staticArrayBtn")
            {
              if (_buttons[14].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
              {
                staticArrayManager.AddElement();
              }
              else
             if (_buttons[15].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
              {
                staticArrayManager.RemoveElement();
              }
            }
            else if (_activeButton == "dynamicArrayBtn")
            {
              if (_buttons[17].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
              {
                dynamicArrayManager.AddElement();
              }
              else
             if (_buttons[18].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
              {
                dynamicArrayManager.RemoveElement();
              }
            }
          }
          _buttons[0].FillColor = _activeButton == "stackBtn" ? Color.Green : Color.White;
          _buttons[3].FillColor = _activeButton == "queueBtn" ? Color.Green : Color.White;
          _buttons[6].FillColor = _activeButton == "linkedListBtn" ? Color.Green : Color.White;
          _buttons[13].FillColor = _activeButton == "staticArrayBtn" ? Color.Green : Color.White;
          _buttons[16].FillColor = _activeButton == "dynamicArrayBtn" ? Color.Green : Color.White;
        }
        else if (screen == 2)
        {
          if (!_mouseDown)
          {
            if (_buttons[20].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
              screen--;
            }
          }
        }
        _mouseDown = true;
      }
      else
      {
        _mouseDown = false;
      }
    }

    public void Draw(RenderWindow window)
    {
      DrawButtons(window);
      DrawButtonTexts(window);
      DrawTextBoxes(window);
    }

    private void DrawButtons(RenderWindow window)
    {
      foreach (var button in _buttons)
      {
        if (ShouldDraw(button, _buttons.Cast<Drawable>().ToList()))
        {
          window.Draw(button);
        }
      }
    }

    private bool ShouldDraw(Drawable item, List<Drawable> drawables)
    {
      if (screen == 1)
      {
        if (item == drawables[0] || item == drawables[3] || item == drawables[6] || item == drawables[13] || item == drawables[16] || item == drawables[19])
        {
          return true;
        }

        if (_activeButton == "stackBtn" && (item == drawables[1] || item == drawables[2]))
        {
          return true;
        }

        if (_activeButton == "queueBtn" && (item == drawables[4] || item == drawables[5]))
        {
          return true;
        }

        if (_activeButton == "linkedListBtn" && (item == drawables[7] || item == drawables[8] || item == drawables[9] || item == drawables[10] || item == drawables[11] || item == drawables[12]))
        {
          return true;
        }

        if (_activeButton == "staticArrayBtn" && (item == drawables[14] || item == drawables[15]))
        {
          return true;
        }

        if (_activeButton == "dynamicArrayBtn" && (item == drawables[17] || item == drawables[18]))
        {
          return true;
        }
      }
      else if (screen == 2)
      {
        if (item == drawables[20])
        {
          return true;
        }
      }

      return false;
    }

    private void DrawButtonTexts(RenderWindow window)
    {
      foreach (var text in _buttonTexts)
      {
        if (ShouldDraw(text, _buttonTexts))
        {
          window.Draw(text);
        }
      }
    }

    private void DrawTextBoxes(RenderWindow window)
    {
      foreach (TextBox textBox in _textBoxes ?? Enumerable.Empty<TextBox>())
      {
        if (_activeButton == "linkedListBtn" && _textBoxes?[0] == textBox)
        {
          textBox.Draw(window);
        }
      }
    }
  }
}