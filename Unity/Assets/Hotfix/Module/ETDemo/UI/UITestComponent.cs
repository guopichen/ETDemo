using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UITestComponentComponentSystem : AwakeSystem<UITestComponent>
	{
		public override void Awake(UITestComponent self)
		{
			self.Awake();
		}
	}

	public class UITestComponent : Component
    {
        private GameObject testBtn;


		public void Awake()
		{
			Log.Debug("UITestComponent Awake!!!");
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			testBtn = rc.Get<GameObject>("TestBtn");
			testBtn.GetComponent<Button>().onClick.Add(OnTest);
		}

		public void OnTest()
		{
			Log.Debug("OnTest");
		}
	}
}


