using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [Event(ETDemoEventIdType.InitSceneStart)]
    public class InitSceneStart : AEvent
    {
		public override void Run()
		{
			Log.Debug("InitSceneStart!!!");
			// 创建ui
			UI ui = UIFactory.Create(DemoUIType.UITest.StringToAB(), DemoUIType.UITest, DemoUIType.UITest);

			ui.AddComponent<UITestComponent>();

			// 添加进ui管理组件
			Game.Scene.GetComponent<UIComponent>().Add(ui);
		}
	}
}

