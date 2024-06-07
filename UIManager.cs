using SFML.Graphics;
using SFML.System;
using SFML.Window;
using StackManagerNS;
using QueueManagerNS;
using LinkedListManagerNS;
using TextBoxNS;
using StaticArrayManagerNS;
using DynamicArrayManagerNS;
using BinaryTreeManagerNS;

namespace UIManagerNS
{
  class UIManager
  {
    private Font _font;
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
    public bool IsBinaryTreeButtonActive => _activeButton == "binaryTreeBtn";

    public UIManager(Font font, Window window)
    {
      _font = font;
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
      AddButton("<", new Vector2f(5, 5));                        //20
      AddButton("Binary Tree", new Vector2f(315, 5));            //21
      AddButton("Go \nLeft", new Vector2f(315, 85), 145);        //22
      AddButton("Go \nRight", new Vector2f(470, 85), 145);       //23
      AddButton("Go \nUp", new Vector2f(315, 165), 300);         //24
      AddButton("Add \nLeft", new Vector2f(315, 245), 145);      //25
      AddButton("Add \nRight", new Vector2f(470, 245), 145);     //26
      AddButton("Remove \nLeft", new Vector2f(315, 325), 145);   //27
      AddButton("Remove \nRight", new Vector2f(470, 325), 145);  //28
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

    public void HandleInput(RenderWindow window, StackManager stackManager, QueueManager queueManager, LinkedListManager linkedListManager, StaticArrayManager staticArrayManager, DynamicArrayManager dynamicArrayManager, BinaryTreeManager binaryTreeManager)
    {
      if (!Mouse.IsButtonPressed(Mouse.Button.Left))
      {
        _mouseDown = false;
        return;
      }

      if (_mouseDown)
      {
        return;
      }

      Vector2f mousePos = window.MapPixelToCoords(Mouse.GetPosition(window));

      CheckMainButtonInput(mousePos);
      UpdateButtonColors();
      HandleScreenNavigation(mousePos);

      if (screen == 1)
      {
        HandleStackActions(mousePos, stackManager);
        HandleQueueActions(mousePos, queueManager);
        HandleLinkedListActions(mousePos, linkedListManager);
        HandleStaticArrayActions(mousePos, staticArrayManager);
        HandleDynamicArrayActions(mousePos, dynamicArrayManager);
      }
      else if (screen == 2)
      {
        HandleBinaryTreeActions(mousePos, binaryTreeManager);
      }

      _mouseDown = true;
    }

    private void CheckMainButtonInput(Vector2f mousePos)
    {
      var buttonMappings = new Dictionary<int, string>
            {
                { 0, "stackBtn" },
                { 3, "queueBtn" },
                { 6, "linkedListBtn" },
                { 13, "staticArrayBtn" },
                { 16, "dynamicArrayBtn" },
                { 21, "binaryTreeBtn" }
            };

      foreach (var mapping in buttonMappings)
      {
        if (_buttons[mapping.Key].GetGlobalBounds().Contains(mousePos.X, mousePos.Y) &&
           ((screen == 1 && mapping.Key != 21) || (screen == 2 && mapping.Key == 21)))
        {
          _activeButton = _activeButton != mapping.Value ? mapping.Value : "";
          break;
        }
      }
    }

    private void UpdateButtonColors()
    {
      var buttonColorMappings = new Dictionary<string, int>
            {
                { "stackBtn", 0 },
                { "queueBtn", 3 },
                { "linkedListBtn", 6 },
                { "staticArrayBtn", 13 },
                { "dynamicArrayBtn", 16 },
                { "binaryTreeBtn", 21 }
            };

      foreach (var mapping in buttonColorMappings)
      {
        _buttons[mapping.Value].FillColor = _activeButton == mapping.Key ? Color.Green : Color.White;
      }
    }

    private void HandleScreenNavigation(Vector2f mousePos)
    {
      if (_buttons[19].GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && screen == 1)
      {
        screen++;
      }

      if (_buttons[20].GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && screen == 2)
      {
        screen--;
      }
    }

    private void HandleStackActions(Vector2f mousePos, StackManager stackManager)
    {
      if (_activeButton != "stackBtn") return;

      if (_buttons[1].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        stackManager.AddStack();
      }
      else if (_buttons[2].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        stackManager.RemoveStack();
      }
    }

    private void HandleQueueActions(Vector2f mousePos, QueueManager queueManager)
    {
      if (_activeButton != "queueBtn") return;

      if (_buttons[4].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        queueManager.AddQueueItem();
      }
      else if (_buttons[5].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        queueManager.RemoveQueueItem();
      }
    }

    private void HandleLinkedListActions(Vector2f mousePos, LinkedListManager linkedListManager)
    {
      if (_activeButton != "linkedListBtn") return;

      if (_buttons[7].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddFirstLinkedListItem();
      }
      else if (_buttons[8].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveFirstLinkedListItem();
      }
      else if (_buttons[9].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddLastLinkedListItem();
      }
      else if (_buttons[10].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveLastLinkedListItem();
      }
      else if (_buttons[11].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddAfterLinkedListItem(_textBoxes[0].getText());
      }
      else if (_buttons[12].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveAfterLinkedListItem(_textBoxes[0].getText());
      }
    }

    private void HandleStaticArrayActions(Vector2f mousePos, StaticArrayManager staticArrayManager)
    {
      if (_activeButton != "staticArrayBtn") return;

      if (_buttons[14].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        staticArrayManager.AddElement();
      }
      else if (_buttons[15].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        staticArrayManager.RemoveElement();
      }
    }

    private void HandleDynamicArrayActions(Vector2f mousePos, DynamicArrayManager dynamicArrayManager)
    {
      if (_activeButton != "dynamicArrayBtn") return;

      if (_buttons[17].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        dynamicArrayManager.AddElement();
      }
      else if (_buttons[18].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        dynamicArrayManager.RemoveElement();
      }
    }

    private void HandleBinaryTreeActions(Vector2f mousePos, BinaryTreeManager binaryTreeManager)
    {
      if (_activeButton != "binaryTreeBtn") return;

      if (_buttons[22].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoLeft();
      }
      else if (_buttons[23].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoRight();
      }
      else if (_buttons[24].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoUp();
      }
      else if (_buttons[25].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.AddLeft();
      }
      else if (_buttons[26].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.AddRight();
      }
      else if (_buttons[27].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.RemoveLeft();
      }
      else if (_buttons[28].GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.RemoveRight();
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
        if (item == drawables[20] || item == drawables[21])
        {
          return true;
        }

        if (_activeButton == "binaryTreeBtn" && (item == drawables[22] || item == drawables[23] || item == drawables[24] || item == drawables[25] || item == drawables[26] || item == drawables[27] || item == drawables[28]))
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
        if (_activeButton == "linkedListBtn" && _textBoxes?[0] == textBox && screen == 1)
        {
          textBox.Draw(window);
        }
      }
    }
  }
}