using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct ViewReferenceGameObjectComponentResolver : IResolver<ViewReferenceGameObjectComponent>, IResolver<ViewReferenceGameObjectComponentResolver,ViewReferenceGameObjectComponent>, IData	{		public ViewReferenceGameObjectComponentResolver In(ref ViewReferenceGameObjectComponent viewreferencegameobjectcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetViewReferenceGameObjectComponent();			Out(ref local);		}		public void Out(ref ViewReferenceGameObjectComponent viewreferencegameobjectcomponent)		{		}	}}