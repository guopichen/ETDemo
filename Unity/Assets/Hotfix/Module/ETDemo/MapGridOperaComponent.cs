
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class MapGridOperaComponentUpdateSystem : UpdateSystem<MapGridOperaComponent>
    {
        public override void Update(MapGridOperaComponent self)
        {
            self.Update();
        }
    }

    public class MapGridOperaComponent : Component
    {
        public void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Unit unit = GetMapGrid();
                if (unit != null) // 点中地图
                {
                    MapGridComponent mapGrid = unit.GetComponent<MapGridComponent>();
                    Log.Debug("点中地图=>GridX:{0}, GridY:{1}", mapGrid.GridX, mapGrid.GridY);
                    // 玩家移动
                    Game.EventSystem.Run(ETDemoEventIdType.MapPlayerMove, unit.Position);
                }
            }
        }


        //// 根据鼠标点获取到选中的地图格子
        Unit GetMapGrid()
        {
            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
            Collider2D[] cols = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Log.Debug("cols count: "+ cols.Length);
            foreach (var col in cols)
            {
                //Log.Debug("mousePosition:{0}, grid:{1}",
                //    Input.mousePosition,
                //    col.transform.parent.name);
                ////
                return unitComponent.Get(col.gameObject.GetHashCode());
            }
            return null;
        }
    }
}


