using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct StateContextComponentResolver : IResolver<StateContextComponent>, IResolver<StateContextComponentResolver,StateContextComponent>, IData	{		public StateContextComponentResolver In(ref StateContextComponent statecontextcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetStateContextComponent();			Out(ref local);		}		public void Out(ref StateContextComponent statecontextcomponent)		{		}	}}