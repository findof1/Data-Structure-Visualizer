using SFML.Graphics;
using SFML.Window;
using UIManagerNS;
using StackManagerNS;
using QueueManagerNS;
using LinkedListManagerNS;
using StaticArrayManagerNS;
using DynamicArrayManagerNS;
using BinaryTreeManagerNS;
using HashTableManagerNS;

namespace SFMLNetExample
{
  class Program
  {
    static void Main(string[] args)
    {
      uint width = 1920;
      uint height = 1080;
      var window = new RenderWindow(new VideoMode(width, height), "Data Structures Visualizer");
      window.SetFramerateLimit(60);

      var font = new Font("./Arial.ttf");
      var uiManager = new UIManager(font, window);

      var stackManager = new StackManager(font, width);

      var queueManager = new QueueManager(font);

      var linkedListManager = new LinkedListManager(font);

      var staticArrayManager = new StaticArrayManager(font);

      var dynamicArrayManager = new DynamicArrayManager(font);

      var binaryTreeManager = new BinaryTreeManager(font, width);

      var hashTableManager = new HashTableManager(font, width);

      window.Closed += (sender, e) => window.Close();

      while (window.IsOpen)
      {
        window.DispatchEvents();
        uiManager.HandleInput(window, stackManager, queueManager, linkedListManager, staticArrayManager, dynamicArrayManager, binaryTreeManager, hashTableManager);
        window.Clear(Color.Black);
        linkedListManager.DrawArrows(window, uiManager.IsLinkedListButtonActive);
        uiManager.Draw(window);
        stackManager.Draw(window, uiManager.IsStackButtonActive);
        queueManager.Draw(window, uiManager.IsQueueButtonActive);
        linkedListManager.Draw(window, uiManager.IsLinkedListButtonActive);
        staticArrayManager.Draw(window, uiManager.IsStaticArrayButtonActive);
        dynamicArrayManager.Draw(window, uiManager.IsDynamicArrayButtonActive);
        binaryTreeManager.Draw(window, uiManager.IsBinaryTreeButtonActive);
        hashTableManager.Draw(window, uiManager.IsHashTableButtonActive);
        window.Display();
      }
    }
  }
}