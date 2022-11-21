using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct ViewDestructionDelayedComponentResolver : IResolver<ViewDestructionDelayedComponent>, IResolver<ViewDestructionDelayedComponentResolver,ViewDestructionDelayedComponent>, IData	{		public ViewDestructionDelayedComponentResolver In(ref ViewDestructionDelayedComponent viewdestructiondelayedcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetViewDestructionDelayedComponent();			Out(ref local);		}		public void Out(ref ViewDestructionDelayedComponent viewdestructiondelayedcomponent)		{		}	}}