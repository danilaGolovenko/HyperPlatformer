using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct MainCameraTagComponentResolver : IResolver<MainCameraTagComponent>, IResolver<MainCameraTagComponentResolver,MainCameraTagComponent>, IData	{		public MainCameraTagComponentResolver In(ref MainCameraTagComponent maincameratagcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetMainCameraTagComponent();			Out(ref local);		}		public void Out(ref MainCameraTagComponent maincameratagcomponent)		{		}	}}