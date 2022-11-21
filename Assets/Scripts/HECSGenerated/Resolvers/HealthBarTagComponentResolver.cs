using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct HealthBarTagComponentResolver : IResolver<HealthBarTagComponent>, IResolver<HealthBarTagComponentResolver,HealthBarTagComponent>, IData	{		public HealthBarTagComponentResolver In(ref HealthBarTagComponent healthbartagcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetHealthBarTagComponent();			Out(ref local);		}		public void Out(ref HealthBarTagComponent healthbartagcomponent)		{		}	}}