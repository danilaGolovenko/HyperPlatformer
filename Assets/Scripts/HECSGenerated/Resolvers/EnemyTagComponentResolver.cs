using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct EnemyTagComponentResolver : IResolver<EnemyTagComponent>, IResolver<EnemyTagComponentResolver,EnemyTagComponent>, IData	{		public EnemyTagComponentResolver In(ref EnemyTagComponent enemytagcomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetEnemyTagComponent();			Out(ref local);		}		public void Out(ref EnemyTagComponent enemytagcomponent)		{		}	}}