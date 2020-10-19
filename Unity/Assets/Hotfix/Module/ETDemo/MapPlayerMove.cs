using ETModel;
using UnityEngine;
using System.Threading;

namespace ETHotfix
{


    // 玩家移动
    [Event(ETDemoEventIdType.MapPlayerMove)]
    public class MapPlayerMove : AEvent<Vector3>
    {
        public override void Run(Vector3 target)
        {
            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
            Unit playerUnit = unitComponent.Get(99);
            MoveComponent moveComponent = playerUnit.GetComponent<MoveComponent>();
            // move
            CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
            moveComponent.MoveToAsync(new Vector3(target.x,target.y, playerUnit.Position.z), 10.0f, CancellationTokenSource.Token);
            Log.Debug("玩家移动到目标: " + target);

        }
    }

}


