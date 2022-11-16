using System;using Systems;using Commands;using Components;using System.Reflection;namespace HECSFramework.Core{	public sealed class DeathHandlerSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (DeathHandlerSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.DeathCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.EventStateAnimationCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (DeathHandlerSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.DeathCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.EventStateAnimationCommand>(system);		}	}	public sealed class EnemyAttackSystemBindContainerForSys : ISystemSetter	{		private FieldInfo meleeAttackComponentFieldBinding = typeof(EnemyAttackSystem).GetField("meleeAttackComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (EnemyAttackSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.AnimationEventCommand>(system, currentSystem);			meleeAttackComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<MeleeAttackComponent>(HMasks.MeleeAttackComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (EnemyAttackSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.AnimationEventCommand>(system);			meleeAttackComponentFieldBinding.SetValue(system, null);		}	}	public sealed class EnemyHealthUISystemBindContainerForSys : ISystemSetter	{		private FieldInfo enemyHolderComponentFieldBinding = typeof(EnemyHealthUISystem).GetField("enemyHolderComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (EnemyHealthUISystem)system;			enemyHolderComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<EntityHolderComponent>(HMasks.EntityHolderComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (EnemyHealthUISystem)system;			enemyHolderComponentFieldBinding.SetValue(system, null);		}	}	public sealed class GravitySystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (GravitySystem)system;			system.Owner.World.AddGlobalReactComponent<GroundedTagComponent>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (GravitySystem)system;			system.Owner.World.RemoveGlobalReactComponent<GroundedTagComponent>(system);		}	}	public sealed class HealthUISystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class InitializationCameraFollowSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class JumpingSystemBindContainerForSys : ISystemSetter	{		private FieldInfo jumpStartSpeedComponentFieldBinding = typeof(JumpingSystem).GetField("jumpStartSpeedComponent", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo currentSpeedComponentFieldBinding = typeof(JumpingSystem).GetField("currentSpeedComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (JumpingSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.InputStartedCommand>(system, currentSystem);			jumpStartSpeedComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<JumpStartSpeedComponent>(HMasks.JumpStartSpeedComponent));			currentSpeedComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CurrentSpeedComponent>(HMasks.CurrentSpeedComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (JumpingSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.InputStartedCommand>(system);			jumpStartSpeedComponentFieldBinding.SetValue(system, null);			currentSpeedComponentFieldBinding.SetValue(system, null);		}	}	public sealed class MovingEnemySystemBindContainerForSys : ISystemSetter	{		private FieldInfo currentSpeedFieldBinding = typeof(MovingEnemySystem).GetField("currentSpeed", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo wayComponentFieldBinding = typeof(MovingEnemySystem).GetField("wayComponent", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo speedCoeffComponentFieldBinding = typeof(MovingEnemySystem).GetField("speedCoeffComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (MovingEnemySystem)system;			system.Owner.RegisterComponentListenersService.AddListener<StopMovingComponent>(system, currentSystem);			currentSpeedFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CurrentSpeedComponent>(HMasks.CurrentSpeedComponent));			wayComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<WayComponent>(HMasks.WayComponent));			speedCoeffComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<SpeedCoeffComponent>(HMasks.SpeedCoeffComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (MovingEnemySystem)system;			system.Owner.RegisterComponentListenersService.RemoveListener<StopMovingComponent>(system);			currentSpeedFieldBinding.SetValue(system, null);			wayComponentFieldBinding.SetValue(system, null);			speedCoeffComponentFieldBinding.SetValue(system, null);		}	}	public sealed class MovingPlatformSystemBindContainerForSys : ISystemSetter	{		private FieldInfo wayComponentFieldBinding = typeof(MovingPlatformSystem).GetField("wayComponent", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo speedCoeffComponentFieldBinding = typeof(MovingPlatformSystem).GetField("speedCoeffComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (MovingPlatformSystem)system;			wayComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<WayComponent>(HMasks.WayComponent));			speedCoeffComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<SpeedCoeffComponent>(HMasks.SpeedCoeffComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (MovingPlatformSystem)system;			wayComponentFieldBinding.SetValue(system, null);			speedCoeffComponentFieldBinding.SetValue(system, null);		}	}	public sealed class MovingSystemBindContainerForSys : ISystemSetter	{		private FieldInfo currentSpeedFieldBinding = typeof(MovingSystem).GetField("currentSpeed", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo speedCoeffFieldBinding = typeof(MovingSystem).GetField("speedCoeff", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (MovingSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.InputCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.InputEndedCommand>(system, currentSystem);			system.Owner.RegisterComponentListenersService.AddListener<StopMovingComponent>(system, currentSystem);			currentSpeedFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CurrentSpeedComponent>(HMasks.CurrentSpeedComponent));			speedCoeffFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<SpeedCoeffComponent>(HMasks.SpeedCoeffComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (MovingSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.InputCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.InputEndedCommand>(system);			system.Owner.RegisterComponentListenersService.RemoveListener<StopMovingComponent>(system);			currentSpeedFieldBinding.SetValue(system, null);			speedCoeffFieldBinding.SetValue(system, null);		}	}	public sealed class PlatformCatchPlayerSystemBindContainerForSys : ISystemSetter	{		private FieldInfo catchesListComponentFieldBinding = typeof(PlatformCatchPlayerSystem).GetField("catchesListComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (PlatformCatchPlayerSystem)system;			system.Owner.EntityCommandService.AddListener<Trigger2dEnterCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Trigger2dExitCommand>(system, currentSystem);			catchesListComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CatchesListComponent>(HMasks.CatchesListComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (PlatformCatchPlayerSystem)system;			system.Owner.EntityCommandService.RemoveListener<Trigger2dEnterCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Trigger2dExitCommand>(system);			catchesListComponentFieldBinding.SetValue(system, null);		}	}	public sealed class PlayerAnimationSystemBindContainerForSys : ISystemSetter	{		private FieldInfo currentSpeedComponentFieldBinding = typeof(PlayerAnimationSystem).GetField("currentSpeedComponent", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo speedCoeffComponentFieldBinding = typeof(PlayerAnimationSystem).GetField("speedCoeffComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (PlayerAnimationSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.InputCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.InputEndedCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.DamageCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.EventStateAnimationCommand>(system, currentSystem);			currentSpeedComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CurrentSpeedComponent>(HMasks.CurrentSpeedComponent));			speedCoeffComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<SpeedCoeffComponent>(HMasks.SpeedCoeffComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (PlayerAnimationSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.InputCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.InputEndedCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.DamageCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.EventStateAnimationCommand>(system);			currentSpeedComponentFieldBinding.SetValue(system, null);			speedCoeffComponentFieldBinding.SetValue(system, null);		}	}	public sealed class PlayerMeleeAttackSystemBindContainerForSys : ISystemSetter	{		private FieldInfo meleeAttackComponentFieldBinding = typeof(PlayerMeleeAttackSystem).GetField("meleeAttackComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (PlayerMeleeAttackSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.InputStartedCommand>(system, currentSystem);			meleeAttackComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<MeleeAttackComponent>(HMasks.MeleeAttackComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (PlayerMeleeAttackSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.InputStartedCommand>(system);			meleeAttackComponentFieldBinding.SetValue(system, null);		}	}	public sealed class SpawnEnemyHealthUISystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class SpawnHealthUISystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class SpitterAnimationSystemBindContainerForSys : ISystemSetter	{		private FieldInfo currentSpeedComponentFieldBinding = typeof(SpitterAnimationSystem).GetField("currentSpeedComponent", BindingFlags.Instance | BindingFlags.NonPublic);		private FieldInfo meleeAttackComponentFieldBinding = typeof(SpitterAnimationSystem).GetField("meleeAttackComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (SpitterAnimationSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.DamageCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<Commands.EventStateAnimationCommand>(system, currentSystem);			currentSpeedComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<CurrentSpeedComponent>(HMasks.CurrentSpeedComponent));			meleeAttackComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<MeleeAttackComponent>(HMasks.MeleeAttackComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (SpitterAnimationSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.DamageCommand>(system);			system.Owner.EntityCommandService.RemoveListener<Commands.EventStateAnimationCommand>(system);			currentSpeedComponentFieldBinding.SetValue(system, null);			meleeAttackComponentFieldBinding.SetValue(system, null);		}	}	public sealed class TagingGroundedSystemBindContainerForSys : ISystemSetter	{		private FieldInfo subjectToGravityTagComponentFieldBinding = typeof(TagingGroundedSystem).GetField("subjectToGravityTagComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (TagingGroundedSystem)system;			subjectToGravityTagComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<SubjectToGravityTagComponent>(HMasks.SubjectToGravityTagComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (TagingGroundedSystem)system;			subjectToGravityTagComponentFieldBinding.SetValue(system, null);		}	}	public sealed class TakeDamageSystemBindContainerForSys : ISystemSetter	{		private FieldInfo healthComponentFieldBinding = typeof(TakeDamageSystem).GetField("healthComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (TakeDamageSystem)system;			system.Owner.EntityCommandService.AddListener<Commands.DamageCommand>(system, currentSystem);			healthComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<HealthComponent>(HMasks.HealthComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (TakeDamageSystem)system;			system.Owner.EntityCommandService.RemoveListener<Commands.DamageCommand>(system);			healthComponentFieldBinding.SetValue(system, null);		}	}	public sealed class AbilitiesSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (AbilitiesSystem)system;			currentSystem.abilitiesHolderComponent = currentSystem.Owner.GetOrAddComponent<AbilitiesHolderComponent>(HMasks.AbilitiesHolderComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (AbilitiesSystem)system;			currentSystem.abilitiesHolderComponent = null;		}	}	public sealed class JobUpdateSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class DestroyEntityWorldSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (DestroyEntityWorldSystem)system;			system.Owner.World.AddGlobalReactCommand<DestroyEntityWorldCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (DestroyEntityWorldSystem)system;			system.Owner.World.RemoveGlobalReactCommand<DestroyEntityWorldCommand>(system);		}	}	public sealed class PoolingSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class RemoveComponentWorldSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (RemoveComponentWorldSystem)system;			system.Owner.World.AddGlobalReactCommand<RemoveHecsComponentWorldCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<AddHecsComponentWorldCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (RemoveComponentWorldSystem)system;			system.Owner.World.RemoveGlobalReactCommand<RemoveHecsComponentWorldCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<AddHecsComponentWorldCommand>(system);		}	}	public sealed class WaitingCommandsSystemsBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (WaitingCommandsSystems)system;			system.Owner.World.AddGlobalReactCommand<WaitAndEntityCallbackCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<WaitAndCallbackCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (WaitingCommandsSystems)system;			system.Owner.World.RemoveGlobalReactCommand<WaitAndEntityCallbackCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<WaitAndCallbackCommand>(system);		}	}	public sealed class AINPCSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (AINPCSystem)system;			system.Owner.EntityCommandService.AddListener<NeedDecisionCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<IsDeadCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<SetDefaultStrategyCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<ChangeStrategyCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<ForceStopAICommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<ForceStartAICommand>(system, currentSystem);			currentSystem.aIStrategyComponent = currentSystem.Owner.GetOrAddComponent<AIStrategyComponent>(HMasks.AIStrategyComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (AINPCSystem)system;			system.Owner.EntityCommandService.RemoveListener<NeedDecisionCommand>(system);			system.Owner.EntityCommandService.RemoveListener<IsDeadCommand>(system);			system.Owner.EntityCommandService.RemoveListener<SetDefaultStrategyCommand>(system);			system.Owner.EntityCommandService.RemoveListener<ChangeStrategyCommand>(system);			system.Owner.EntityCommandService.RemoveListener<ForceStopAICommand>(system);			system.Owner.EntityCommandService.RemoveListener<ForceStartAICommand>(system);			currentSystem.aIStrategyComponent = null;		}	}	public sealed class InputListenSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class SpawnViewSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (SpawnViewSystem)system;			currentSystem.viewReferenceGameObject = currentSystem.Owner.GetOrAddComponent<ViewReferenceGameObjectComponent>(HMasks.ViewReferenceGameObjectComponent);			currentSystem.unityTransform = currentSystem.Owner.GetOrAddComponent<UnityTransformComponent>(HMasks.UnityTransformComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (SpawnViewSystem)system;			currentSystem.viewReferenceGameObject = null;			currentSystem.unityTransform = null;		}	}	public sealed class StartSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class UpdateActorByTranformSystemBindContainerForSys : ISystemSetter	{		private FieldInfo transformComponentFieldBinding = typeof(UpdateActorByTranformSystem).GetField("transformComponent", BindingFlags.Instance | BindingFlags.NonPublic);		public void BindSystem(ISystem system)		{			var currentSystem = (UpdateActorByTranformSystem)system;			transformComponentFieldBinding.SetValue(currentSystem, currentSystem.Owner.GetOrAddComponent<TransformComponent>(HMasks.TransformComponent));		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (UpdateActorByTranformSystem)system;			transformComponentFieldBinding.SetValue(system, null);		}	}	public sealed class UpdateTranformFromActorSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (UpdateTranformFromActorSystem)system;			currentSystem.transformComponent = currentSystem.Owner.GetOrAddComponent<TransformComponent>(HMasks.TransformComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (UpdateTranformFromActorSystem)system;			currentSystem.transformComponent = null;		}	}	public sealed class SoundGlobalSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (SoundGlobalSystem)system;			system.Owner.World.AddGlobalReactCommand<PlaySoundCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<StopSoundCommand>(system, currentSystem);			currentSystem.volumeComponent = currentSystem.Owner.GetOrAddComponent<SoundVolumeComponent>(HMasks.SoundVolumeComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (SoundGlobalSystem)system;			system.Owner.World.RemoveGlobalReactCommand<PlaySoundCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<StopSoundCommand>(system);			currentSystem.volumeComponent = null;		}	}	public sealed class HideUISystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (HideUISystem)system;			system.Owner.EntityCommandService.AddListener<HideUICommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (HideUISystem)system;			system.Owner.EntityCommandService.RemoveListener<HideUICommand>(system);		}	}	public sealed class UISystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (UISystem)system;			system.Owner.World.AddGlobalReactCommand<ShowUICommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<ShowUIOnAdditionalCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<HideUICommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<ShowUIAndHideOthersCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<HideAllUIExceptCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<UIGroupCommand>(system, currentSystem);			system.Owner.World.AddGlobalReactCommand<CanvasReadyCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (UISystem)system;			system.Owner.World.RemoveGlobalReactCommand<ShowUICommand>(system);			system.Owner.World.RemoveGlobalReactCommand<ShowUIOnAdditionalCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<HideUICommand>(system);			system.Owner.World.RemoveGlobalReactCommand<ShowUIAndHideOthersCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<HideAllUIExceptCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<UIGroupCommand>(system);			system.Owner.World.RemoveGlobalReactCommand<CanvasReadyCommand>(system);		}	}	public sealed class CountersHolderSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (CountersHolderSystem)system;			system.Owner.EntityCommandService.AddListener<AddCounterModifierCommand<float>>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<RemoveCounterModifierCommand<float>>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<ResetCountersCommand>(system, currentSystem);			currentSystem.countersHolder = currentSystem.Owner.GetOrAddComponent<CountersHolderComponent>(HMasks.CountersHolderComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (CountersHolderSystem)system;			system.Owner.EntityCommandService.RemoveListener<AddCounterModifierCommand<float>>(system);			system.Owner.EntityCommandService.RemoveListener<RemoveCounterModifierCommand<float>>(system);			system.Owner.EntityCommandService.RemoveListener<ResetCountersCommand>(system);			currentSystem.countersHolder = null;		}	}	public sealed class StateUpdateSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class StrategiesMainServiceSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{		}		public void UnBindSystem(ISystem system)		{		}	}	public sealed class AnimationDoneCheckOutSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (AnimationDoneCheckOutSystem)system;			system.Owner.EntityCommandService.AddListener<AnimationEventCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<AnimationDoneCheckOut>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<AnimationCycleCheckout>(system, currentSystem);			currentSystem.animationCheckOutsHolder = currentSystem.Owner.GetOrAddComponent<AnimationCheckOutsHolderComponent>(HMasks.AnimationCheckOutsHolderComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (AnimationDoneCheckOutSystem)system;			system.Owner.EntityCommandService.RemoveListener<AnimationEventCommand>(system);			system.Owner.EntityCommandService.RemoveListener<AnimationDoneCheckOut>(system);			system.Owner.EntityCommandService.RemoveListener<AnimationCycleCheckout>(system);			currentSystem.animationCheckOutsHolder = null;		}	}	public sealed class AnimationSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (AnimationSystem)system;			system.Owner.EntityCommandService.AddListener<BoolAnimationCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<FloatAnimationCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<IntAnimationCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<TriggerAnimationCommand>(system, currentSystem);			system.Owner.EntityCommandService.AddListener<ViewReadyCommand>(system, currentSystem);			currentSystem.AnimatorStateComponent = currentSystem.Owner.GetOrAddComponent<AnimatorStateComponent>(HMasks.AnimatorStateComponent);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (AnimationSystem)system;			system.Owner.EntityCommandService.RemoveListener<BoolAnimationCommand>(system);			system.Owner.EntityCommandService.RemoveListener<FloatAnimationCommand>(system);			system.Owner.EntityCommandService.RemoveListener<IntAnimationCommand>(system);			system.Owner.EntityCommandService.RemoveListener<TriggerAnimationCommand>(system);			system.Owner.EntityCommandService.RemoveListener<ViewReadyCommand>(system);			currentSystem.AnimatorStateComponent = null;		}	}	public sealed class CompositeAbilitiesSystemBindContainerForSys : ISystemSetter	{		public void BindSystem(ISystem system)		{			var currentSystem = (CompositeAbilitiesSystem)system;			system.Owner.EntityCommandService.AddListener<ExecuteAbilityCommand>(system, currentSystem);		}		public void UnBindSystem(ISystem system)		{			var currentSystem = (CompositeAbilitiesSystem)system;			system.Owner.EntityCommandService.RemoveListener<ExecuteAbilityCommand>(system);		}	}	public static partial class TypesMap	{		static partial void SetSystemSetters()		{			systemsSetters = new System.Collections.Generic.Dictionary<Type, ISystemSetter>()			{				{typeof(DeathHandlerSystem), new DeathHandlerSystemBindContainerForSys()},				{typeof(EnemyAttackSystem), new EnemyAttackSystemBindContainerForSys()},				{typeof(EnemyHealthUISystem), new EnemyHealthUISystemBindContainerForSys()},				{typeof(GravitySystem), new GravitySystemBindContainerForSys()},				{typeof(HealthUISystem), new HealthUISystemBindContainerForSys()},				{typeof(InitializationCameraFollowSystem), new InitializationCameraFollowSystemBindContainerForSys()},				{typeof(JumpingSystem), new JumpingSystemBindContainerForSys()},				{typeof(MovingEnemySystem), new MovingEnemySystemBindContainerForSys()},				{typeof(MovingPlatformSystem), new MovingPlatformSystemBindContainerForSys()},				{typeof(MovingSystem), new MovingSystemBindContainerForSys()},				{typeof(PlatformCatchPlayerSystem), new PlatformCatchPlayerSystemBindContainerForSys()},				{typeof(PlayerAnimationSystem), new PlayerAnimationSystemBindContainerForSys()},				{typeof(PlayerMeleeAttackSystem), new PlayerMeleeAttackSystemBindContainerForSys()},				{typeof(SpawnEnemyHealthUISystem), new SpawnEnemyHealthUISystemBindContainerForSys()},				{typeof(SpawnHealthUISystem), new SpawnHealthUISystemBindContainerForSys()},				{typeof(SpitterAnimationSystem), new SpitterAnimationSystemBindContainerForSys()},				{typeof(TagingGroundedSystem), new TagingGroundedSystemBindContainerForSys()},				{typeof(TakeDamageSystem), new TakeDamageSystemBindContainerForSys()},				{typeof(AbilitiesSystem), new AbilitiesSystemBindContainerForSys()},				{typeof(JobUpdateSystem), new JobUpdateSystemBindContainerForSys()},				{typeof(DestroyEntityWorldSystem), new DestroyEntityWorldSystemBindContainerForSys()},				{typeof(PoolingSystem), new PoolingSystemBindContainerForSys()},				{typeof(RemoveComponentWorldSystem), new RemoveComponentWorldSystemBindContainerForSys()},				{typeof(WaitingCommandsSystems), new WaitingCommandsSystemsBindContainerForSys()},				{typeof(AINPCSystem), new AINPCSystemBindContainerForSys()},				{typeof(InputListenSystem), new InputListenSystemBindContainerForSys()},				{typeof(SpawnViewSystem), new SpawnViewSystemBindContainerForSys()},				{typeof(StartSystem), new StartSystemBindContainerForSys()},				{typeof(UpdateActorByTranformSystem), new UpdateActorByTranformSystemBindContainerForSys()},				{typeof(UpdateTranformFromActorSystem), new UpdateTranformFromActorSystemBindContainerForSys()},				{typeof(SoundGlobalSystem), new SoundGlobalSystemBindContainerForSys()},				{typeof(HideUISystem), new HideUISystemBindContainerForSys()},				{typeof(UISystem), new UISystemBindContainerForSys()},				{typeof(CountersHolderSystem), new CountersHolderSystemBindContainerForSys()},				{typeof(StateUpdateSystem), new StateUpdateSystemBindContainerForSys()},				{typeof(StrategiesMainServiceSystem), new StrategiesMainServiceSystemBindContainerForSys()},				{typeof(AnimationDoneCheckOutSystem), new AnimationDoneCheckOutSystemBindContainerForSys()},				{typeof(AnimationSystem), new AnimationSystemBindContainerForSys()},				{typeof(CompositeAbilitiesSystem), new CompositeAbilitiesSystemBindContainerForSys()},			};		}	}}