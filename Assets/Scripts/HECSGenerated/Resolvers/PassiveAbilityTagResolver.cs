using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct PassiveAbilityTagResolver : IResolver<PassiveAbilityTag>, IResolver<PassiveAbilityTagResolver,PassiveAbilityTag>, IData	{		public PassiveAbilityTagResolver In(ref PassiveAbilityTag passiveabilitytag)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetPassiveAbilityTag();			Out(ref local);		}		public void Out(ref PassiveAbilityTag passiveabilitytag)		{		}	}}