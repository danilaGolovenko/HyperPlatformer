using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public struct AbilityPredicateComponentResolver : IResolver<AbilityPredicateComponent>, IResolver<AbilityPredicateComponentResolver,AbilityPredicateComponent>, IData	{		public AbilityPredicateComponentResolver In(ref AbilityPredicateComponent abilitypredicatecomponent)		{			return this;		}		public void Out(ref IEntity entity)		{			var local = entity.GetAbilityPredicateComponent();			Out(ref local);		}		public void Out(ref AbilityPredicateComponent abilitypredicatecomponent)		{		}	}}