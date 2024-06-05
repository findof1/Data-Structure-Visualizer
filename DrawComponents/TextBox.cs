using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TextBoxNS
{
  public class TextBox
  {
    private RectangleShape box;
    private Text text;
    private bool hasFocus;
    private string inputText;

    public TextBox(Vector2f position, Vector2f size, Font font)
    {
      box = new RectangleShape(size)
      {
        FillColor = Color.White,
        OutlineColor = Color.Blue,
        OutlineThickness = 2,
        Position = position
      };

      text = new Text("", font, 20)
      {
        FillColor = Color.Black,
        Position = new Vector2f(position.X + 5, position.Y + 5)
      };

      hasFocus = false;
      inputText = string.Empty;
    }

    public void Draw(RenderWindow window)
    {
      window.Draw(box);
      window.Draw(text);
    }

    public int getText()
    {
      if (int.TryParse(inputText, out int result))
      {
        return result;
      }
      return 0;
    }

    public void HandleMouseEvent(object? sender, MouseButtonEventArgs e)
    {


      if (box.GetGlobalBounds().Contains(e.X, e.Y))
      {
        hasFocus = true;
        box.FillColor = Color.Blue;
        box.OutlineColor = Color.White;
      }
      else
      {
        hasFocus = false;
        box.FillColor = Color.White;
        box.OutlineColor = Color.Blue;
      }

    }

    public void HandleTextEvent(object? sender, TextEventArgs e)
    {

      if (hasFocus)
      {
        if (e.Unicode == "\b")
        {
          if (inputText.Length > 0)
          {
            inputText = inputText.Substring(0, inputText.Length - 1);
          }
        }
        else
        {
          if (e.Unicode == "1" || e.Unicode == "2" || e.Unicode == "3" || e.Unicode == "4" || e.Unicode == "5" || e.Unicode == "6" || e.Unicode == "7" || e.Unicode == "8" || e.Unicode == "9" || e.Unicode == "0")
          {
            inputText += e.Unicode;
          }
        }
        text.DisplayedString = inputText;
      }
    }
  }
}