using System;
using ETModel;

namespace ETHotfix.ETDemo
{
    public static class MapHelper
    {
        /// <summary>
        /// 进入地图
        /// </summary>
        /// <returns></returns>
        public static async ETVoid EnterMapAsync(int MapSizeX,int MapSizeY)
        {
            try
            {
                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("etdemomap.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync("ETDemoMap");
                }

                Game.Scene.AddComponent<MapGridOperaComponent>();
                Game.EventSystem.Run(ETDemoEventIdType.EnterMapFinish, MapSizeX, MapSizeY);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}


