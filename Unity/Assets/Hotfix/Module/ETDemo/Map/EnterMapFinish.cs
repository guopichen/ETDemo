using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [Event(ETDemoEventIdType.EnterMapFinish)]
    public class EnterMapFinish: AEvent<int, int>
    {
        public override void Run(int MapSizeX,int MapSizeY)
        {
            // 
            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
 
            // 格子资源
            string abName = "etdemomap";
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();

            // 地图

            Unit mapGridUnit = CreateMap(resourcesComponent,abName,unitComponent,MapSizeX,MapSizeY);

            // 玩家
            Unit playerUnit = CreatePlayer(resourcesComponent, abName, unitComponent);
            playerUnit.Position = mapGridUnit.Position;

        }

        Unit CreatePlayer(ResourcesComponent resourcesComponent, string abName, UnitComponent unitComponent)
        {
            GameObject gameObjectPlayer = UnityEngine.Object.Instantiate(
                (GameObject)resourcesComponent.GetAsset(abName.StringToAB(), "MapPlayer"));
            // 创建玩家实体
            Unit playerUnit = ETModel.ComponentFactory.CreateWithId<ETModel.Unit, GameObject>(99,gameObjectPlayer);
            unitComponent.Add(playerUnit);
            playerUnit.AddComponent<MoveComponent>();
            return playerUnit;
        }

        Unit CreateMap(ResourcesComponent resourcesComponent, string abName, UnitComponent unitComponent,
            int MapSizeX, int MapSizeY)
        {
            GameObject bundleGameObjectMapGrid =
                (GameObject)resourcesComponent.GetAsset(abName.StringToAB(), "MapGrid");
            // 添加格子
            Unit mapGridUnit = null;
            MapGridData mapGridData = new MapGridData();
            mapGridData.ABName = abName;
            for (int x = 0; x < MapSizeX; x++)
            {
                for (int y = 0; y < MapSizeY; y++)
                {
                    GameObject gameObjectGrid = UnityEngine.Object.Instantiate(bundleGameObjectMapGrid);
                    gameObjectGrid.name = string.Format("Grid[{0}][{1}]", x, y);
                    // 创建地图格子实体
                    int id = gameObjectGrid.GetHashCode();// x * 1000 + y;
                    Log.Debug("创建地图格子实体:{0} ", id);
                    mapGridUnit = ETModel.ComponentFactory.CreateWithId<ETModel.Unit, GameObject>(
                        id, gameObjectGrid);
                    // 添加地图格子组件
                    mapGridData.GridX = x;
                    mapGridData.GridY = y;
                    mapGridData.BgName = "Desert";
                    MapGridComponent mapGridComponent = ETModel.ComponentFactory.CreateWithParent<MapGridComponent, MapGridData>(
                        mapGridUnit, mapGridData);
                    mapGridUnit.AddComponent(mapGridComponent);
                    // add
                    unitComponent.Add(mapGridUnit);
                }
            }
            return mapGridUnit;
        }
    }
}
