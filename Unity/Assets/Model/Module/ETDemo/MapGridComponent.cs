
using UnityEngine;

namespace ETModel
{
    public class MapGridData
    {
        public int GridX;
        public int GridY;
        public string ABName;
        public string BgName;
    }

    [ObjectSystem]
    public class MapGridComponentAwakeSystem : AwakeSystem<MapGridComponent, MapGridData>
    {
        public override void Awake(MapGridComponent self, MapGridData mapGridData)
        {
            self.Awake(mapGridData);
        }
    }

    /// <summary>
    /// 地图格子组件
    /// 目前只有一个背景图片
    /// </summary>
    public class MapGridComponent : Component
    {
        public int GridX { get; private set; }
        public int GridY { get; private set; }

        public void Awake(MapGridData mapGridData)
        {
            // 数据存储
            GridX = mapGridData.GridX;
            GridY = mapGridData.GridY;

            Unit unit = this.GetParent<Unit>();
            GameObject goUnit = unit.GameObject;
            ReferenceCollector rc = goUnit.GetComponent<ReferenceCollector>();
            SpriteRenderer bgSpriteRender = rc.Get<GameObject>("Bg").GetComponent<SpriteRenderer>();
            // 创建图片资源
            bgSpriteRender.sprite = CreateSprite(mapGridData.ABName, mapGridData.BgName);
            // 更新位置
            UpdateGirdPos(unit, bgSpriteRender);
        }

        Sprite CreateSprite(string abName, string imgName)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            Texture2D tex =
                (Texture2D)resourcesComponent.GetAsset(abName.StringToAB(), imgName);
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        void UpdateGirdPos(Unit unit, SpriteRenderer bgSpriteRender)
        {
            Vector3 Size = bgSpriteRender.bounds.size;
            // 计算格子位置
            unit.Position = new Vector3(
                GridX * Size.x,
                GridY * Size.y,
                0);
            //Log.Debug("x:{0},y:{1}", GridX, GridY);
        }
    }
}


