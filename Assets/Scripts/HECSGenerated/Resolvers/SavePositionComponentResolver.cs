using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;using HECSFramework.Core;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct SavePositionComponentResolver : IResolver<SavePositionComponent>, IResolver<SavePositionComponentResolver,SavePositionComponent>, IData	{		[Key(0)]		public Vector3Serialize Position;		[Key(1)]		public Vector3Serialize Rotation;		public SavePositionComponentResolver In(ref SavePositionComponent savepositioncomponent)		{			savepositioncomponent.BeforeSync();			this.Position = savepositioncomponent.Position;			this.Rotation = savepositioncomponent.Rotation;			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetSavePositionComponent();			Out(ref local);		}		public void Out(ref SavePositionComponent savepositioncomponent)		{			savepositioncomponent.Position = this.Position;			savepositioncomponent.Rotation = this.Rotation;		}	}}