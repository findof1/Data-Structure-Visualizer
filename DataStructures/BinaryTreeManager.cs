using SFML.Graphics;
using SFML.System;

namespace BinaryTreeManagerNS
{
  public class TreeNode
  {
    public CircleShape Shape;
    public Text TextVar;
    public TreeNode? LeftChild;
    public TreeNode? RightChild;
    public TreeNode? PrevNode;
    public float x;
    public float y;

    public TreeNode(Font _font, float xval, float yval)
    {
      x = xval;
      y = yval;

      var rand = new Random();
      var number = new Text($"{rand.Next(0, 20)}", _font);

      number.FillColor = Color.Black;
      number.Position = new Vector2f(xval, yval);
      TextVar = number;
      Shape = new CircleShape(25)
      {
        Position = new Vector2f(x, y),
        FillColor = Color.White,
      };

    }
  }
  class BinaryTreeManager
  {
    private Font _font;
    private uint _width;
    private TreeNode _head;
    public TreeNode _currentNode;
    public int _currentDepth;


    public BinaryTreeManager(Font font, uint width)
    {
      _font = font;
      _width = width;
      _head = new TreeNode(_font, 900, 400);
      _currentNode = _head;
      _currentDepth = 1;

    }
    public void AddLeft()
    {
      if (_currentNode.LeftChild == null && _currentDepth < 4)
      {
        TreeNode node = new TreeNode(_font, _currentNode.x - 200, _currentNode.y + 100);
        _currentNode.LeftChild = node;
        node.PrevNode = _currentNode;
        _currentNode = node;
        _currentDepth++;
        UpdateNodePositions(_head, 900, 400, 200, 100);
      }
    }

    public void AddRight()
    {
      if (_currentNode.RightChild == null && _currentDepth < 4)
      {
        TreeNode node = new TreeNode(_font, _currentNode.x + 200, _currentNode.y + 100);
        _currentNode.RightChild = node;
        node.PrevNode = _currentNode;
        _currentNode = node;
        _currentDepth++;
        UpdateNodePositions(_head, 900, 400, 200, 100);
      }
    }

    public void RemoveLeft()
    {
      _currentNode.LeftChild = null;
      UpdateNodePositions(_head, 900, 400, 200, 100);
    }

    public void RemoveRight()
    {
      _currentNode.RightChild = null;
      UpdateNodePositions(_head, 900, 400, 200, 100);
    }

    public void GoLeft()
    {
      if (_currentNode.LeftChild != null)
      {
        _currentNode = _currentNode.LeftChild;
        _currentDepth++;
      }
    }

    public void GoRight()
    {
      if (_currentNode.RightChild != null)
      {
        _currentNode = _currentNode.RightChild;
        _currentDepth++;
      }
    }

    public void GoUp()
    {
      if (_currentNode.PrevNode != null)
      {
        _currentNode = _currentNode.PrevNode;
        _currentDepth--;
      }
    }

    private void UpdateNodePositions(TreeNode node, float x, float y, float horizontalSpacing, float _verticalSpacing)
    {
      if (node == null)
      {
        return;
      }

      node.x = x;
      node.y = y;
      node.Shape.Position = new Vector2f(x, y);
      node.TextVar.Position = new Vector2f(x, y);

      if (node.LeftChild != null)
      {
        UpdateNodePositions(node.LeftChild, x - horizontalSpacing, y + _verticalSpacing, horizontalSpacing / 2, _verticalSpacing);
      }

      if (node.RightChild != null)
      {
        UpdateNodePositions(node.RightChild, x + horizontalSpacing, y + _verticalSpacing, horizontalSpacing / 2, _verticalSpacing);
      }
    }

    public void Draw(RenderWindow window, bool isActive)
    {
      if (isActive)
      {
        DrawNode(window, _head);
      }
    }

    private void DrawNode(RenderWindow window, TreeNode node)
    {
      if (node == null)
      {
        return;
      }

      if (node == _currentNode)
      {
        node.Shape.FillColor = Color.Green;
      }
      else
      {
        node.Shape.FillColor = Color.White;
      }

      window.Draw(node.Shape);
      window.Draw(node.TextVar);

      if (node.LeftChild != null)
      {
        var line = new Vertex[]
        {
            new Vertex(new Vector2f(node.x + node.Shape.Radius, node.y + node.Shape.Radius), Color.Black),
            new Vertex(new Vector2f(node.LeftChild.x + node.LeftChild.Shape.Radius, node.LeftChild.y + node.LeftChild.Shape.Radius), Color.White)
        };
        window.Draw(line, 0, 2, PrimitiveType.Lines);

        DrawNode(window, node.LeftChild);
      }

      if (node.RightChild != null)
      {
        var line = new Vertex[]
        {
             new Vertex(new Vector2f(node.x + node.Shape.Radius, node.y + node.Shape.Radius), Color.White),
             new Vertex(new Vector2f(node.RightChild.x + node.RightChild.Shape.Radius, node.RightChild.y + node.RightChild.Shape.Radius), Color.White)
        };
        window.Draw(line, 0, 2, PrimitiveType.Lines);

        DrawNode(window, node.RightChild);
      }
    }
  }
}