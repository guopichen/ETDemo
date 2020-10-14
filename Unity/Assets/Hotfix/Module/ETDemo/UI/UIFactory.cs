using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class UIFactory
    {
		public static UI Create(string bundleName,string prefab,string uiName,
			bool fromPool = false)
		{
			try
			{
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(bundleName);
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(bundleName, prefab);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

				UI ui = ComponentFactory.Create<UI, string, GameObject>(uiName, gameObject, fromPool);

				return ui;
			}
			catch (Exception e)
			{
				Log.Error(e);
				return null;
			}
		}
	}

}

