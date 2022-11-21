using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct PlayerTagComponentResolver : IResolver<PlayerTagComponent>, IResolver<PlayerTagComponentResolver,PlayerTagComponent>, IData	{		public PlayerTagComponentResolver In(ref PlayerTagComponent playertagcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetPlayerTagComponent();			Out(ref local);		}		public void Out(ref PlayerTagComponent playertagcomponent)		{		}	}}