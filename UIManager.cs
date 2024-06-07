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
using System.Collections.Generic;
using HashTableManagerNS;

namespace UIManagerNS
{
  class UIButton
  {
    public RectangleShape Shape { get; }
    public Text Text { get; }

    public UIButton(string buttonText, Font font, Vector2f position, Vector2f size, uint fontSize = 24)
    {
      Shape = new RectangleShape(size)
      {
        Position = position,
        FillColor = Color.White
      };

      Text = new Text(buttonText, font, fontSize)
      {
        FillColor = Color.Black,
        Position = new Vector2f(position.X + 10, position.Y + 10)
      };
    }
  }

  class UIManager
  {
    private Font _font;
    private RenderWindow _window;
    private Dictionary<string, UIButton> _buttons;
    private List<TextBox> _textBoxes;
    private string? _activeButton;
    private bool _mouseDown;
    private int _screen = 1;
    public bool IsStackButtonActive => _activeButton == "stackBtn";
    public bool IsQueueButtonActive => _activeButton == "queueBtn";
    public bool IsLinkedListButtonActive => _activeButton == "linkedListBtn";
    public bool IsStaticArrayButtonActive => _activeButton == "staticArrayBtn";
    public bool IsDynamicArrayButtonActive => _activeButton == "dynamicArrayBtn";
    public bool IsBinaryTreeButtonActive => _activeButton == "binaryTreeBtn";
    public bool IsHashTableButtonActive => _activeButton == "hashTableBtn";
    public UIManager(Font font, RenderWindow window)
    {
      _font = font;
      _window = window;
      _buttons = new Dictionary<string, UIButton>();
      _textBoxes = new List<TextBox>();

      CreateButtons();
      CreateTextBoxes();
    }

    private void CreateButtons()
    {
      AddButton("stackBtn", "Stack", new Vector2f(5, 5), new Vector2f(300, 75));
      AddButton("stackPush", "Push", new Vector2f(5, 85), new Vector2f(300, 75));
      AddButton("stackPop", "Pop", new Vector2f(5, 165), new Vector2f(300, 75));
      AddButton("queueBtn", "Queue", new Vector2f(315, 5), new Vector2f(300, 75));
      AddButton("queueEnqueue", "Enqueue", new Vector2f(315, 85), new Vector2f(300, 75));
      AddButton("queueDequeue", "Dequeue", new Vector2f(315, 165), new Vector2f(300, 75));
      AddButton("linkedListBtn", "Linked List", new Vector2f(625, 5), new Vector2f(300, 75));
      AddButton("linkedListAddFirst", "Add First", new Vector2f(625, 85), new Vector2f(145, 75));
      AddButton("linkedListRemoveFirst", "Remove First", new Vector2f(625, 165), new Vector2f(145, 75));
      AddButton("linkedListAddLast", "Add Last", new Vector2f(780, 85), new Vector2f(145, 75));
      AddButton("linkedListRemoveLast", "Remove Last", new Vector2f(780, 165), new Vector2f(145, 75));
      AddButton("linkedListAddAfter", "Add After", new Vector2f(625, 245), new Vector2f(145, 75));
      AddButton("linkedListRemoveAfter", "Remove After", new Vector2f(780, 245), new Vector2f(145, 75));
      AddButton("staticArrayBtn", "Static Array", new Vector2f(935, 5), new Vector2f(300, 75));
      AddButton("staticArrayAdd", "Add Element", new Vector2f(935, 85), new Vector2f(300, 75));
      AddButton("staticArrayRemove", "Remove Element", new Vector2f(935, 165), new Vector2f(300, 75));
      AddButton("dynamicArrayBtn", "Dynamic Array", new Vector2f(1245, 5), new Vector2f(300, 75));
      AddButton("dynamicArrayAdd", "Add Element", new Vector2f(1245, 85), new Vector2f(300, 75));
      AddButton("dynamicArrayRemove", "Remove Element", new Vector2f(1245, 165), new Vector2f(300, 75));
      AddButton("nextScreen", ">", new Vector2f(1555, 5), new Vector2f(300, 75));
      AddButton("prevScreen", "<", new Vector2f(5, 5), new Vector2f(300, 75));
      AddButton("binaryTreeBtn", "Binary Tree", new Vector2f(315, 5), new Vector2f(300, 75));
      AddButton("binaryTreeGoLeft", "Go Left", new Vector2f(315, 85), new Vector2f(145, 75));
      AddButton("binaryTreeGoRight", "Go Right", new Vector2f(470, 85), new Vector2f(145, 75));
      AddButton("binaryTreeGoUp", "Go Up", new Vector2f(315, 165), new Vector2f(300, 75));
      AddButton("binaryTreeAddLeft", "Add Left", new Vector2f(315, 245), new Vector2f(145, 75));
      AddButton("binaryTreeAddRight", "Add Right", new Vector2f(470, 245), new Vector2f(145, 75));
      AddButton("binaryTreeRemoveLeft", "Remove Left", new Vector2f(315, 325), new Vector2f(145, 75), 20);
      AddButton("binaryTreeRemoveRight", "Remove Right", new Vector2f(470, 325), new Vector2f(145, 75), 20);
      AddButton("hashTableBtn", "Hash Table", new Vector2f(625, 5), new Vector2f(300, 75));
      AddButton("hashTableAdd", "Add", new Vector2f(625, 85), new Vector2f(300, 75));
      AddButton("hashTableRemove", "Remove", new Vector2f(625, 165), new Vector2f(300, 75));
    }

    private void CreateTextBoxes()
    {
      TextBox tb = new TextBox(new Vector2f(625, 325), new Vector2f(300, 75), _font);
      _textBoxes.Add(tb);
      _window.TextEntered += tb.HandleTextEvent;
      _window.MouseButtonPressed += tb.HandleMouseEvent;
    }

    private void AddButton(string id, string text, Vector2f position, Vector2f size, uint fontSize = 24)
    {
      _buttons[id] = new UIButton(text, _font, position, size, fontSize);
    }

    public void HandleInput(RenderWindow window, StackManager stackManager, QueueManager queueManager, LinkedListManager linkedListManager, StaticArrayManager staticArrayManager, DynamicArrayManager dynamicArrayManager, BinaryTreeManager binaryTreeManager, HashTableManager hashTableManager)
    {
      if (!Mouse.IsButtonPressed(Mouse.Button.Left))
      {
        _mouseDown = false;
        return;
      }

      if (_mouseDown) return;

      Vector2f mousePos = window.MapPixelToCoords(Mouse.GetPosition(window));

      CheckMainButtonInput(mousePos);
      UpdateButtonColors();
      HandleScreenNavigation(mousePos);

      if (_screen == 1)
      {
        HandleStackActions(mousePos, stackManager);
        HandleQueueActions(mousePos, queueManager);
        HandleLinkedListActions(mousePos, linkedListManager);
        HandleStaticArrayActions(mousePos, staticArrayManager);
        HandleDynamicArrayActions(mousePos, dynamicArrayManager);
      }
      else if (_screen == 2)
      {
        HandleBinaryTreeActions(mousePos, binaryTreeManager);
        HandleHashTableActions(mousePos, hashTableManager);
      }

      _mouseDown = true;
    }

    private void CheckMainButtonInput(Vector2f mousePos)
    {
      var buttonMappingsScreen1 = new Dictionary<string, string>
            {
                { "stackBtn", "stackBtn" },
                { "queueBtn", "queueBtn" },
                { "linkedListBtn", "linkedListBtn" },
                { "staticArrayBtn", "staticArrayBtn" },
                { "dynamicArrayBtn", "dynamicArrayBtn" }
            };
      var buttonMappingsScreen2 = new Dictionary<string, string>
            {
                { "binaryTreeBtn", "binaryTreeBtn" },
                { "hashTableBtn", "hashTableBtn" }
            };
      if (_screen == 1)
      {
        foreach (var mapping in buttonMappingsScreen1)
        {
          if (_buttons[mapping.Key].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
          {
            _activeButton = _activeButton != mapping.Value ? mapping.Value : "";
            break;
          }
        }
      }
      if (_screen == 2)
      {
        foreach (var mapping in buttonMappingsScreen2)
        {
          if (_buttons[mapping.Key].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
          {
            _activeButton = _activeButton != mapping.Value ? mapping.Value : "";
            break;
          }
        }
      }
    }

    private void UpdateButtonColors()
    {
      foreach (var button in _buttons)
      {
        button.Value.Shape.FillColor = _activeButton == button.Key ? Color.Green : Color.White;
      }
    }

    private void HandleScreenNavigation(Vector2f mousePos)
    {
      if (_buttons["nextScreen"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && _screen == 1)
      {
        _screen++;
      }

      if (_buttons["prevScreen"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && _screen == 2)
      {
        _screen--;
      }
    }

    private void HandleStackActions(Vector2f mousePos, StackManager stackManager)
    {
      if (_activeButton != "stackBtn") return;

      if (_buttons["stackPush"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        stackManager.AddStack();
      }
      else if (_buttons["stackPop"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        stackManager.RemoveStack();
      }
    }

    private void HandleQueueActions(Vector2f mousePos, QueueManager queueManager)
    {
      if (_activeButton != "queueBtn") return;

      if (_buttons["queueEnqueue"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        queueManager.AddQueueItem();
      }
      else if (_buttons["queueDequeue"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        queueManager.RemoveQueueItem();
      }
    }

    private void HandleLinkedListActions(Vector2f mousePos, LinkedListManager linkedListManager)
    {
      if (_activeButton != "linkedListBtn") return;

      if (_buttons["linkedListAddFirst"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddFirstLinkedListItem();
      }
      else if (_buttons["linkedListRemoveFirst"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveFirstLinkedListItem();
      }
      else if (_buttons["linkedListAddLast"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddLastLinkedListItem();
      }
      else if (_buttons["linkedListRemoveLast"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveLastLinkedListItem();
      }
      else if (_buttons["linkedListAddAfter"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.AddAfterLinkedListItem(_textBoxes[0].getText());
      }
      else if (_buttons["linkedListRemoveAfter"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        linkedListManager.RemoveAfterLinkedListItem(_textBoxes[0].getText());
      }
    }

    private void HandleStaticArrayActions(Vector2f mousePos, StaticArrayManager staticArrayManager)
    {
      if (_activeButton != "staticArrayBtn") return;

      if (_buttons["staticArrayAdd"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        staticArrayManager.AddElement();
      }
      else if (_buttons["staticArrayRemove"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        staticArrayManager.RemoveElement();
      }
    }

    private void HandleDynamicArrayActions(Vector2f mousePos, DynamicArrayManager dynamicArrayManager)
    {
      if (_activeButton != "dynamicArrayBtn") return;

      if (_buttons["dynamicArrayAdd"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        dynamicArrayManager.AddElement();
      }
      else if (_buttons["dynamicArrayRemove"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        dynamicArrayManager.RemoveElement();
      }
    }

    private void HandleBinaryTreeActions(Vector2f mousePos, BinaryTreeManager binaryTreeManager)
    {
      if (_activeButton != "binaryTreeBtn") return;

      if (_buttons["binaryTreeGoLeft"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoLeft();
      }
      else if (_buttons["binaryTreeGoRight"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoRight();
      }
      else if (_buttons["binaryTreeGoUp"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.GoUp();
      }
      else if (_buttons["binaryTreeAddLeft"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.AddLeft();
      }
      else if (_buttons["binaryTreeAddRight"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.AddRight();
      }
      else if (_buttons["binaryTreeRemoveLeft"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.RemoveLeft();
      }
      else if (_buttons["binaryTreeRemoveRight"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        binaryTreeManager.RemoveRight();
      }
    }

    private void HandleHashTableActions(Vector2f mousePos, HashTableManager hashTableManager)
    {
      if (_activeButton != "hashTableBtn") return;

      if (_buttons["hashTableAdd"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        hashTableManager.Add();
      }
      else if (_buttons["hashTableRemove"].Shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
      {
        hashTableManager.Remove();
      }

    }

    public void Draw(RenderWindow window)
    {
      DrawButtons(window);
      DrawTextBoxes(window);
    }

    private void DrawButtons(RenderWindow window)
    {
      foreach (var button in _buttons.Values)
      {
        if (ShouldDrawButton(button))
        {
          window.Draw(button.Shape);
          window.Draw(button.Text);
        }
      }
    }

    private bool ShouldDrawButton(UIButton button)
    {
      if (_screen == 1)
      {
        if (button == _buttons["stackBtn"] || button == _buttons["queueBtn"] || button == _buttons["linkedListBtn"] ||
            button == _buttons["staticArrayBtn"] || button == _buttons["dynamicArrayBtn"] || button == _buttons["nextScreen"])
        {
          return true;
        }

        if (_activeButton == "stackBtn" && (button == _buttons["stackPush"] || button == _buttons["stackPop"]))
        {
          return true;
        }

        if (_activeButton == "queueBtn" && (button == _buttons["queueEnqueue"] || button == _buttons["queueDequeue"]))
        {
          return true;
        }

        if (_activeButton == "linkedListBtn" && (button == _buttons["linkedListAddFirst"] || button == _buttons["linkedListRemoveFirst"] ||
            button == _buttons["linkedListAddLast"] || button == _buttons["linkedListRemoveLast"] ||
            button == _buttons["linkedListAddAfter"] || button == _buttons["linkedListRemoveAfter"]))
        {
          return true;
        }

        if (_activeButton == "staticArrayBtn" && (button == _buttons["staticArrayAdd"] || button == _buttons["staticArrayRemove"]))
        {
          return true;
        }

        if (_activeButton == "dynamicArrayBtn" && (button == _buttons["dynamicArrayAdd"] || button == _buttons["dynamicArrayRemove"]))
        {
          return true;
        }
      }
      else if (_screen == 2)
      {
        if (button == _buttons["prevScreen"] || button == _buttons["binaryTreeBtn"] || button == _buttons["hashTableBtn"])
        {
          return true;
        }

        if (_activeButton == "binaryTreeBtn" && (button == _buttons["binaryTreeGoLeft"] || button == _buttons["binaryTreeGoRight"] ||
            button == _buttons["binaryTreeGoUp"] || button == _buttons["binaryTreeAddLeft"] ||
            button == _buttons["binaryTreeAddRight"] || button == _buttons["binaryTreeRemoveLeft"] ||
            button == _buttons["binaryTreeRemoveRight"]))
        {
          return true;
        }

        if (_activeButton == "hashTableBtn" && (button == _buttons["hashTableAdd"] || button == _buttons["hashTableRemove"]))
        {
          return true;
        }
      }

      return false;
    }

    private void DrawTextBoxes(RenderWindow window)
    {
      foreach (var textBox in _textBoxes)
      {
        if (_activeButton == "linkedListBtn" && _screen == 1)
        {
          textBox.Draw(window);
        }
      }
    }
  }
}