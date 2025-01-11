using System;

namespace GameOfVlad.Utils.GameObject;

public class TickUpdater(int frequency, Action action)
{
    public int Tick { get; private set; }

    public void Update()
    {
        Tick++;
        if (Tick % frequency == 0)
        {
            action.Invoke();
        }
    }
}